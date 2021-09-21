using System.Collections.Generic;

namespace AnimalAdoption.Common.Domain
{
    public class Cart
    {
        public string Id { get; set; }
        public IEnumerable<CartContent> CartContents { get; set; }
    }
}
