using Microsoft.EntityFrameworkCore;
using NotesApp.Core.Contracts;
using NotesApp.Infrastructure.Common;
using NotesApp.Infrastructure.Dtos.NoteDtos;
using NotesApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Core.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository repository;
        private readonly IUserService userService;

        public NoteService(IRepository _repository,
            IUserService _userService)
        {
            this.repository = _repository;
            this.userService = _userService;

        }

        /// <summary>
        /// adds a note to the database
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        public async Task AddNoteAsync(string userId, NoteAdd noteAdd)
        {
            var note = new Note { Title = noteAdd.Title, Description = noteAdd.Description, UserId = Guid.Parse(userId) };

            await repository.AddAsync(note);
            await repository.SaveChangesAsync();    
        }

        /// <summary>
        /// if the note is found sets its IsActive property to false and returns true, if note is not found returns false
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public async Task<bool> DeleteNoteByIdAndUserIdAsync(string userId, string noteId)
        {
            var guidUserId = Guid.Parse(userId);
            var guidNoteId = Guid.Parse(noteId);

            var note = await repository.All<Note>()
                .FirstOrDefaultAsync(n => n.Id == guidNoteId && n.UserId == guidUserId);

            if (note == null)
            {
                return false;

            }

            note.IsActive = false;
            await repository.SaveChangesAsync();

            return true;

        }

        /// <summary>
        /// gets all notes for the user with given id, if user is nt found returns null
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public async Task<List<Note>?> GetAllUserNotesAsync(string userId)
        {

            var user = await userService.GetUserByIdAsync(Guid.Parse(userId));

            if(user == null)
            {
                return null!;
            }

            return user.Notes
                .Where(n=>n.IsActive==true)
                .ToList();
            
        }


        /// <summary>
        /// gets the note that matches userId and noteId, otherwise returns null
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        /// 
        public async Task<Note?> GetNoteByIdAndUserIdAsync(string userId, string noteId)
        {
            var guidUserId = Guid.Parse(userId);
            var guidNoteId = Guid.Parse(noteId);

            var note = await repository.All<Note>()
                .FirstOrDefaultAsync(n => n.Id == guidNoteId && n.UserId == guidUserId && n.IsActive == true);

            if(note == null)
            {
                return null!;
            }

            return note;
        }

        /// <summary>
        /// Returns true is note has been found and updated, if not returns false
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <param name="noteUpdate"></param>
        /// <returns></returns>
        /// 
        public async Task<bool> EditNoteByIdAndUserIdAsync(string userId, string noteId, NoteUpdate noteUpdate)
        {

            var note = await GetNoteByIdAndUserIdAsync(userId, noteId);

            if(note == null)
            {
                return false;
            }

            note.Title  = noteUpdate.Title;
            note.Description = noteUpdate.Description;
            await repository.SaveChangesAsync();

            return true;
        }
    }
}
