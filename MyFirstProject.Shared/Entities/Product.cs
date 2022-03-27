﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Shared.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public double PromoPrice { get; set; }

        public Guid BrandId { get; set; }
    }
}
