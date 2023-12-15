using System.ComponentModel.DataAnnotations;

namespace Db1HealthPanelBack.Models.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public required string Password { get; set; }
    }
}