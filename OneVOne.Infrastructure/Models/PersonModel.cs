﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVOne.GameService.Infrastructure.Models
{
    public class PersonModel : Model
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
