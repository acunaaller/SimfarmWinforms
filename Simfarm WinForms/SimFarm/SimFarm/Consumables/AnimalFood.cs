using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Consumables
{
    [Serializable]
    public class AnimalFood : Consumable
    {
        public AnimalFood() : base(name: "Alimento para animal", numberOfUses: 1, price: 10, quantityAdded: 100)
        {
        }
    }
}
