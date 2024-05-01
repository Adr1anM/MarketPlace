using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Authors.Responses
{
    public class AuthorDto
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public string Biography { get; set; } 
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public string SocialMediaLinks { get; set; }
        public int NumberOfPosts { get; set; }
    }
}
