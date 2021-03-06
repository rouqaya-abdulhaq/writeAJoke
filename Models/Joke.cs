using System;
using System.ComponentModel.DataAnnotations;

namespace writeAJoke.Models 
{
    public class Joke 
    {
        public int ID {get; set;}
        [StringLength(60,MinimumLength = 2)]
        [Required]
        public string Title {get; set;}
        [Required]
        public string Body {get; set;}

        public string Genre {get; set;}
        
        public string UserId {get; set;}

        public string UserName {get; set;}

        public int  laughs {get; set;}

        public int smiles {get; set;}

        public int eyeRolls {get; set;}
    }
}