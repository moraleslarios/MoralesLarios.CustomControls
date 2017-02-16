using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoralesLarios.Utilities.ClientTests
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Sales { get; set; }
        public string City { get; set; }



        public static IEnumerable<Customer> GetData()
        {
            return new List<Customer>()
            {
                new Customer { ID = 1, Name = "Philips"   , Sales = 2000000m, City = "Madrid"   },
                new Customer { ID = 2, Name = "Pionner"   , Sales = 1000000m, City = "Berlin"   },
                new Customer { ID = 3, Name = "Renault"   , Sales = 2000000m, City = "Paris"    },
                new Customer { ID = 4, Name = "Sony Music", Sales =  500000m, City = "London"   },
                new Customer { ID = 5, Name = "Sony SCEE" , Sales = 2000000m, City = "Tokio"    },
                new Customer { ID = 6, Name = "Pepsi"     , Sales = 9000000m, City = "New York" },
                new Customer { ID = 6, Name = "LG"        , Sales = 2000000m, City = "Rome"     }
            };
        }
    }
}
