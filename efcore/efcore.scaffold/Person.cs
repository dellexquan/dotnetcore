using System;
using System.Collections.Generic;

namespace efcore.scaffold
{
    public partial class Person
    {
        public long Id { get; set; }
        public long Age { get; set; }
        public string BirthPlace { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
