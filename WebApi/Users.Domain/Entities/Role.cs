using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Core.Entities
{
    public class Role
    {
        [Key]
        public long Id { get; set; }

        [StringLength(50, ErrorMessage = "Role name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}
