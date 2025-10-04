using Application.Service.Interface;
using Entities;

namespace Application.Service;
public class MediaService : IMediaService
{
    private readonly IMediaRepository _repo;

    public MediaService(IMediaRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Media>> GetAllMediaAsync()
    {
        return await _repo.GetAllMediaAsync();
    }
    public async Task<Media> GetMediaByIdAsync(int id)
    {
        return await _repo.GetMediaByIdAsync(id);
    }
    public async Task CreateMediaAsync(Media media)
    {
        await _repo.AddMediaAsync(media);
    }
    public async Task UpdateMediaAsync(int id, Media media)
    {
        await _repo.UpdateMediaAsync(id, media);
    }
    public async Task DeleteMediaAsync(int id)
    {
        var media = await _repo.GetMediaByIdAsync(id);
        await _repo.DeleteMediaAsync(media);
    }
}
