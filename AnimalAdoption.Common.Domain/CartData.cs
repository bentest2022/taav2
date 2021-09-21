using System.Collections.Generic;

namespace AnimalAdoption.Common.Domain
{
    public class CartData
    {
        public Dictionary<int, int> CartContents { get; set; } = new Dictionary<int, int>();
    }
}
