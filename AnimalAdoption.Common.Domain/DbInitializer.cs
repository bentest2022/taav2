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

            if (context.Animals.Any())
            {
                return; // DB has been seeded
            }

            var animalList = new Animal[] {
                new Animal { Name = "Kaya", Age = 5, Description = "Funny, kind, friendly, adventurous, dreamer, impulsive, optimistic, caring, loyal, cool, graceful, fun-loving, heroic, sweet" },
                new Animal { Name = "Khumba", Age = 2, Description = "Happy, determined, dicouraged, outcasted, kind, caring, smart, social, nice" },
                new Animal { Name = "Marty", Age = 7, Description = "Funny, kind, friendly, adventurous, a dreamer, optimistic, impulsive, caring, loyal, humorous, cool, brainy, mischievous, obstinate, fun-loving, heroic" },
            };

            foreach (var a in animalList)
            {
                context.Animals.Add(a);
            }
            //context.AddRange(animalList);
            context.SaveChanges();
        }
    }
}
