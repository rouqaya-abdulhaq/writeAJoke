using System;
using System.ComponentModel.DataAnnotations;

namespace writeAJoke.Models 
{
    public class InputModel 
    {
        [Display(Name = "Name")]
        public string Name {get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email {get; set;}

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password {get; set;}

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}