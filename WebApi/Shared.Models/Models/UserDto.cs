using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace WebApi.Shared.Models
{
    public class UserDto
    {
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
        [StringLength(15)]
        [DefaultValue("M")]
        public string Gender { get; set; }

        public bool Active { get; set; }

        public virtual ContactDto Contact { get; set; }

        public virtual RoleDto Role { get; set; }
    }

    public class ContactDto
    {
        public long Id { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, ErrorMessage = "Phone cannot be longer than 20 characters.")]
        public string Phone { get; set; }

        [StringLength(80, ErrorMessage = "City name cannot be longer than 80 characters.")]
        public string? City { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot be longer than 250 characters.")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot be longer than 100 characters.")]
        public string? Country { get; set; }
    }

    public class RoleDto
    {
        public long Id { get; set; }

        [StringLength(50, ErrorMessage = "Role name cannot be longer than 50 characters.")]
        public string Name { get; set; }
    }
}
