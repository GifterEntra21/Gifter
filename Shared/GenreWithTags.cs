﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class GenreWithTags
    {
        public string GenreName { get; set; }
        public List<string> Tags { get; set; }


        public GenreWithTags()
        {
            this.Tags = new List<string>();
        }
    }
}
