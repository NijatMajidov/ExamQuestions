using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MambaASPNet.Core.Models
{
    public class Team : BaseEntity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(3)]
        public string Surname { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Position { get; set; } = null!;
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; } = null!;
        
    }
}
