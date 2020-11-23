using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Consumables
{
    [Serializable]
    public class Irrigation : Consumable
    {
        public Irrigation() : base(name: "Riego", numberOfUses: 1, price: 10, quantityAdded: 40)
        {
        }
    }
}
