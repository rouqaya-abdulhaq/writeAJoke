using System;
using System.ComponentModel.DataAnnotations;

namespace writeAJoke.Models 
{
    public class Joke 
    {
        public int ID {get; set;}
        public string Title {get; set;}
        public string Body {get; set;}
    }
}