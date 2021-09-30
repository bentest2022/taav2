using AnimalAdoption.Common.Domain;
using System.Linq;

namespace AnimalAdoption.Common.Logic
{
    public class AnimalService
    {
        private readonly AnimalAdoptionContext _context;
        public AnimalService(AnimalAdoptionContext context)
        {
            _context = context;
        }

        public Animal[] ListAnimals()
        {
            return _context.Animals.Select(x => new Animal()
            {
                Id = x.Id,
                Age = x.Age,
                Description = x.Description,
                Name = x.Name,
                Owner = x.Owner
            })
            .ToArray();
        }
    }
}
