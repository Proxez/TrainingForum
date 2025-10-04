using Application.Service.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class MediaRepository : IMediaRepository
{
    private readonly MyDbContext _context;

    public MediaRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<Media>> GetAllMediaAsync()
    {
        return await _context.Media.ToListAsync();
    }
    public async Task<Media?> GetMediaByIdAsync(int id)
    {
        return await _context.Media.FindAsync(id);
    }
    public async Task AddMediaAsync(Media media)
    {
        await _context.Media.AddAsync(media);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateMediaAsync(int id, Media updatedMedia)
    {
        var media = await _context.Media.FirstOrDefaultAsync(c => c.Id == id);
        if (media != null)
        {
            media.Url = updatedMedia.Url;
            _context.Media.Update(updatedMedia);
            await _context.SaveChangesAsync();
        }
    }
    public async Task DeleteMediaAsync(Media media)
    {
        if (media != null)
        {
            _context.Media.Remove(media);
            await _context.SaveChangesAsync();
        }
    }
}
