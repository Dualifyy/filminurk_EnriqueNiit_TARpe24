using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Accounts
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Kinnita oma parool uuesti: ")]
        [Compare("Password", ErrorMessage = "Paroolid ei kattu. Proovi uuesti.")]
        public string ConfirmNewPassword { get; set; }
        public string Token { get; set; }

    }
}
