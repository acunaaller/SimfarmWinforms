using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Consumables
{
    [Serializable]
    public class AnimalWater : Consumable
    {
        public AnimalWater() : base(name: "Agua para animal", numberOfUses: 1, price: 10, quantityAdded: 70)
        {
        }
    }
}