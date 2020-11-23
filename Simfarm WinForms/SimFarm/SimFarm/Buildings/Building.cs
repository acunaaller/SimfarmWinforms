using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Buildings
{
    [Serializable]
    abstract public class Building
    {
        private string name;
        private int purchasePrice;
        private int salePrice;


        public Building(string name, int purchasePrice, int salePrice)
        {
            this.name = name;
            this.purchasePrice = purchasePrice;
            this.salePrice = salePrice;

        }

        public string Name { get => name; }
        public int PurchasePrice { get => purchasePrice; set => purchasePrice = value; }
        public int SalePrice { get => salePrice; set => salePrice = value; }



    }
}