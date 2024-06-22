﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Categories.Responses
{
    public class CategWithSubCategDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; } = [];   
    }
}
