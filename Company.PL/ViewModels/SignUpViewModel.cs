using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="Username Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="First Name Is Required")]
        public string FName { get; set; }
        [Required(ErrorMessage ="Last Name Is Required")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(Password),ErrorMessage ="Confirm Password Does Not Match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
