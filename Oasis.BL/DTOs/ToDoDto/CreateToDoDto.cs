﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.BL.DTOs.ToDoDto
{
    public class CreateToDoDto
    {
        public string? Title { get; set; }
        public bool Completed { get; set; }
    }
}