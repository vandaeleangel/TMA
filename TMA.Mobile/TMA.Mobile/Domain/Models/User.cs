using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMA.Mobile.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Geen geldig email adress")]
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public ICollection<Chore> Chores { get; set; }

    }
}
