using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Interfaces;

namespace Users.Core.Entities
{
    public class User : IAddable
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "First Name cannot be longer than 80 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(80, ErrorMessage = "Last Name cannot be longer than 80 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Company { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string Gender { get; set; }

        public bool Active { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Role Role { get; set; }

    }
}
