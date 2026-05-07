using SyntecxhubUserApi.Models;

namespace SyntecxhubUserApi.Interfaces
{
    public interface INoteRepository
    {
        Task<List<Note>> GetUserNotes(int userId);
        Task<Note> GetById(int id);
        Task Add(Note note);
        Task Delete(Note note);
    }
}
