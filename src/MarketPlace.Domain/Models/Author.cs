﻿using MarketPlace.Domain.Models.Auth;
using System.ComponentModel.DataAnnotations;


namespace MarketPlace.Domain.Models
{
    public class Author : Entity
    {
        public int UserId { get; set; }  
        public User User { get; set; }

        [MaxLength(400)]
        public string Biography { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }
        public DateTime BirthDate{ get; set; }

        [MaxLength(200)]
        public string SocialMediaLinks { get; set; }
        public int NumberOfPosts { get; set; }
        public string PhoneNumber { get; set; } 
        public byte[]? ProfileImage { get; set; }
        public List<Product> Products { get; } = [];
        public List<AuthorAuthorCategory> AuthorAuthorCategories { get; } = [];

    }
}
