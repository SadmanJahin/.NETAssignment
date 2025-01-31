using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Enums;

namespace Users.Core.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(80, ErrorMessage = "First Name cannot be longer than 80 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(80, ErrorMessage = "Last Name cannot be longer than 80 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company is required.")]
        [StringLength(150, ErrorMessage = "Company name cannot be longer than 150 characters.")]
        public string Company { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        public bool Active { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Role Role { get; set; }

    }
}
