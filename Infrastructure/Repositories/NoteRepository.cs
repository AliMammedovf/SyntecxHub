using Microsoft.EntityFrameworkCore;
using SyntecxhubUserApi.Data;
using SyntecxhubUserApi.Interfaces;
using SyntecxhubUserApi.Models;

namespace SyntecxhubUserApi.Infrastructure.Repositories
{
    public class NoteRepository: INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetUserNotes(int userId)
        {
            return await _context.Notes
                .Where(n => n.UserId == userId && !n.IsDeleted)
                .ToListAsync();
        }

        public async Task<Note> GetById(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task Add(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Note note)
        {
            note.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
