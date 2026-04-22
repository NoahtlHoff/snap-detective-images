using SnapDetective.Images.Contracts;
using SnapDetective.Images.Contracts.Events;
using SnapDetective.Images.Interfaces;
using SnapDetective.Images.Models;

namespace SnapDetective.Images.Services;

public class ImageSetService(
    IImageSetRepository repository,
    IMessagePublisher publisher,
    IBlobStorageService blobStorage) : IImageSetService
{
    public async Task<List<ImageSetResult>> GetAllAsync()
    {
        var sets = await repository.GetAllAsync();
        return sets.Select(ToResult).ToList();
    }

    public async Task<ImageSetResult?> GetByIdAsync(int id)
    {
        var set = await repository.GetByIdAsync(id);
        return set is null ? null : ToResult(set);
    }

    public async Task<ImageSetResult> CreateAsync(CreateImageSetRequest request)
    {
        var imageSet = new ImageSet
        {
            Name = request.Name,
            Description = request.Description
        };
        var created = await repository.CreateAsync(imageSet);
        return ToResult(created);
    }

    public async Task AddImageAsync(int imageSetId, AddImageRequest request)
    {
        var url = await blobStorage.UploadAsync(
            request.FileStream,
            request.FileName,
            request.ContentType);

        var image = new Image
        {
            Url = url,
            FileName = request.FileName,
            Answer = request.Answer
        };

        await repository.AddImageAsync(imageSetId, image);

        var set = await repository.GetByIdAsync(imageSetId);
        if (set is null) return;

        await publisher.PublishAsync(
            new ImageSetPublishedEvent(
                set.Id,
                set.Name,
                set.Images.Select(i => new ImageEventItem(i.Id, i.Url, i.Answer)).ToList()),
            routingKey: "image-set.published");
    }

    private static ImageSetResult ToResult(ImageSet set) =>
        new(set.Id, set.Name, set.Description,
            set.Images.Select(i => new ImageResult(i.Id, i.Url, i.Answer)).ToList());
}