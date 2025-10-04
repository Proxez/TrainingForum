using Entities;

namespace Application.Service.Interface;
public interface IMediaService
{
    Task CreateMediaAsync(Media media);
    Task DeleteMediaAsync(int id);
    Task<List<Media>> GetAllMediaAsync();
    Task<Media> GetMediaByIdAsync(int id);
    Task UpdateMediaAsync(int id, Media updatedMedia);
}