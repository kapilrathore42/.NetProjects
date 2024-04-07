using System.ComponentModel.DataAnnotations;

public class SignUpViewModel
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required.")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
