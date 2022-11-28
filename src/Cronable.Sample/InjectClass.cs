using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronable.Sample
{
    public interface IInjectClass
    {
        public List<Customer> Customers { get; set; }
    }
    public record Customer
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
    }

    public class InjectClass : IInjectClass
    {
        public List<Customer> Customers { get; set; } = new()
        {
            new(){Name="Trendyol"},
            new(){Name="Hepsiburada"},
            new(){Name="n11"},
        };

    }
}
