using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password Does Not Match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        //public string Token { get; set; }
        //public string Email { get; set; }
    }
}
