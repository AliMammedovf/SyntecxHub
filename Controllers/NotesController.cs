using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyntecxhubUserApi.Business.DTOs;
using SyntecxhubUserApi.Interfaces;
using SyntecxhubUserApi.Models;
using System.Security.Claims;



namespace SyntecxhubUserApi.Controllers
{
    [ApiController]
    [Route("api/note")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _repo;

        public NotesController(INoteRepository repo)
        {
            _repo = repo;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotes()
        {
            var notes = await _repo.GetUserNotes(GetUserId());

            var result = notes.Select(n => new NoteDTO
            {
                Id = n.Id,
                Title = n.Title
            });

            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateNoteDTO dto)
        {
            var note = new Note
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = GetUserId()
            };

            await _repo.Add(note);
            return Ok(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _repo.GetById(id);

            if (note == null || note.UserId != GetUserId())
                return NotFound();

            await _repo.Delete(note);
            return Ok(StatusCodes.Status204NoContent);
        }



    }
}
