using NotesApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Infrastructure.Dtos.NoteDtos
{
    public class NoteAdd
    {

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;


    }
}
