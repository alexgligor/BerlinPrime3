using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class GenericArticle: BaseDbObject
    {
        public string? Location { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public string? Currency { get; set; }

        public string Flat()
        {
            return $"{Title} {Description} {Location} {Count} {Price}";
        }
    }
}
