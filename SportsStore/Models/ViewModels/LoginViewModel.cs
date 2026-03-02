using System.ComponentModel.DataAnnotations;

namespace GamerStore.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }

        public Uri ReturnUrl { get; set; } = new Uri("/", UriKind.Relative);
    }
}
