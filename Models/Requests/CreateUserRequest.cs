using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class CreateUserRequest
    {

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

    }
}