using System;
using System.Collections.Generic;

namespace efcore.scaffold
{
    public partial class Book
    {
        public long Id { get; set; }
        public string AuthorName { get; set; } = null!;
        public double? Price { get; set; }
        public string? PubTime { get; set; }
        public string Title { get; set; } = null!;
    }
}
