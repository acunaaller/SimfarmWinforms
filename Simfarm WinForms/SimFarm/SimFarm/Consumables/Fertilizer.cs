using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Consumables
{
    [Serializable]
    public class Fertilizer : Consumable
    {
        public Fertilizer() : base(name: "Fertilizante", numberOfUses: 1, price: 10, quantityAdded: 70)
        {
        }
    }
}