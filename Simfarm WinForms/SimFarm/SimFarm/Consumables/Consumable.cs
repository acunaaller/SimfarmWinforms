using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Consumables
{
    [Serializable]
    abstract public class Consumable
    {
        private string name;
        private int numberOfUses;
        private int price;
        private int quantityAdded;

        public Consumable(string name, int numberOfUses, int price, int quantityAdded)
        {
            this.name = name;
            this.numberOfUses = numberOfUses;
            this.price = price;
            this.quantityAdded = quantityAdded;
        }

        public string Name { get => name; }
        public int NumberOfUses { get => numberOfUses; }
        public int Price { get => price; }
        public int QuantityAdded { get => quantityAdded; }

    }
}
