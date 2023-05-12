using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Core.Contracts;
using NotesApp.Infrastructure.Dtos.NoteDtos;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService noteService;

        public NoteController(INoteService _noteService)
        {
            this.noteService  = _noteService;
        }

        /// <summary>
        /// Adds a note, to a given user, to the database 
        /// </summary>
        /// <param name="noteAdd"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("add/{userId}")]
        public async Task<IActionResult> Add(string userId,[FromBody] NoteAdd noteAdd)
        {
            await noteService.AddNoteAsync(userId, noteAdd);

            return Ok();

        }

        /// <summary>
        /// gets all the notes a given user has
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetAllNotes(string userId)
        {

            var notes = await noteService.GetAllUserNotesAsync(userId);

            if(notes == null)
            {
                return BadRequest("List is null");
            }

            return Ok(notes);

        }

        [HttpGet]
        [Route("{userId}/{noteId}")]
        public async Task<IActionResult> GetNoteByIdAndUserId(string userId,string noteId)
        {

            var note = await noteService.GetNoteByIdAndUserIdAsync(userId,noteId);

            if (note == null)
            {
                return BadRequest("Note not found");
            }

            return Ok(note);

        }

        /// <summary>
        /// sets the given 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("delete/{userId}/{noteId}")]
        public async Task<IActionResult> DeleteNoteByIdAndUserId(string userId, string noteId)
        {

            if (await noteService.DeleteNoteByIdAndUserIdAsync(userId, noteId))
            {
                return NoContent();
            }

            return BadRequest("Couldn't delete note");

        }

        [HttpPatch]
        [Route("edit/{userId}/{noteId}")]
        public async Task<IActionResult> EditNoteByIdAndUserId(string userId, string noteId, [FromBody] NoteUpdate noteUpdate)
        {

            if (await noteService.EditNoteByIdAndUserIdAsync(userId,noteId,noteUpdate))
            {
                return Ok(noteUpdate);
            }

            return BadRequest("Couldn't update note");

        }

    }
}
