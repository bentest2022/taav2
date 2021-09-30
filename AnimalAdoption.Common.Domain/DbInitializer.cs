using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoption.Common.Domain
{
    public static class DbInitializer
    {
        public static void Initialize(AnimalAdoptionContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Owners.Any() && !context.Animals.Any())
            {
                var ownerOne = new Owner { Name = "Bob Smith", PhoneNumber = "021578526", Address = "34 Silverdale Road" };
                context.Add(ownerOne);

                var ownerTwo = new Owner { Name = "Wayne Johnson", PhoneNumber = "027859645", Address = "158 Hamilton Road" };
                context.Add(ownerTwo);

                var ownerThree = new Owner { Name = "Jane Doe", PhoneNumber = "0214570876", Address = "2A River Road" };
                context.Add(ownerThree);

                var animalList = new Animal[] {
                    new Animal { Name = "Lucy", Age = 1, Description = "Golden retriever. Female", Owner = ownerOne },
                    new Animal { Name = "Gizmo", Age = 4, Description = "Pug. Male" , Owner = ownerTwo },
                    new Animal { Name = "Freddy", Age = 7, Description = "German Shepard. Male", Owner = ownerThree},
                };

                context.AddRange(animalList);
                context.SaveChanges();
            }
        }
    }
}
