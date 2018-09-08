using BPM.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Production
{
    public class Product : Material
    {
        public ICollection<Requirement> Requirements { get; set; }

        public Product()
        {
        }
    }
}
