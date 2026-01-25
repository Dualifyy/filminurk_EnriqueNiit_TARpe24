using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Accounts
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sisesta oma parool: ")]
        public string Password { get; set; }
        [Display(Name = "Sisesta oma parool uuesti: ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Paroolid ei kattu. Proovi uuesti.")]
        public string ConfirmPassword { get; set; }
        public string? DisplayName { get; set; }
        public bool ProfileType { get; set; }
    }
}
