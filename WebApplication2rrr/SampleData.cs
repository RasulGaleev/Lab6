using System.Linq;
using WebApplication2rrr.Models;

namespace WebApplication2rrr
{
    public class SampleData
    {
        public static void Initialize(MobileContext context, IWebHostEnvironment env)
        {
            if (!context.Phones.Any())
            {
                context.Phones.AddRange(
                    new Phone
                    {
                        Name = "iPhone X",
                        Company = "Apple",
                        Price = 600
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy Edge",
                        Company = "Samsung",
                        Price = 550
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy S10",
                        Company = "Samsung",
                        Price = 850
                    },
                    new Phone
                    {
                        Name = "Pixel 3",
                        Company = "Google",
                        Price = 500
                    },
                    new Phone
                    {
                        Name = "iPhone 11",
                        Company = "Apple",
                        Price = 600
                    },
                    new Phone
                    {
                        Name = "iPhone 12",
                        Company = "Apple",
                        Price = 800
                    },
                    new Phone
                    {
                        Name = "iPhone 13",
                        Company = "Apple",
                        Price = 1200
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
