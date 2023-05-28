using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Infrastructure.Dtos.NoteDtos
{
    public class NoteUpdate
    {
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; } = null!;

    }
}
