using NotesApp.Infrastructure.Dtos.NoteDtos;
using NotesApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Core.Contracts
{
    public interface INoteService
    {
        Task AddNoteAsync(string userId, NoteAdd noteAdd);

        Task<List<Note>?> GetAllUserNotesAsync(string userId);

        Task<Note?> GetNoteByIdAndUserIdAsync(string userId,string noteId);

        Task<bool> DeleteNoteByIdAndUserIdAsync(string userId, string noteId);

        Task<bool> EditNoteByIdAndUserIdAsync(string userId, string noteId,NoteUpdate noteUpdate);
    }
}
