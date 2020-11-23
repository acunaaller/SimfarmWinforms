using System;
using System.Collections.Generic;
using System.Text;
using SimFarm.Products;

namespace SimFarm.Buildings
{
    [Serializable]
    public class StorageBuilding : Building
    {
        private List<FinishedProduct> products = new List<FinishedProduct>();
        private int maximumCapacity;

        public StorageBuilding(string name, int purchasePrice, int salePrice, int maximumCapacity) : base(name, purchasePrice, salePrice)
        {

            this.maximumCapacity = maximumCapacity;
        }

        public List<FinishedProduct> Products { get => products; }
        public int MaximumCapacity { get => maximumCapacity; }

        public void ChangeQuality()
        {
            foreach (var product in products)
            {
                if (product.GetType() == typeof(FinishedProduct))
                    product.DicreaseQuality();
            }
        }

        public void AddFinishedProduct(FinishedProduct finishedProduct)
        {
            products.Add(finishedProduct);
        }

        public bool IsFull()
        {
            return products.Count == maximumCapacity;
        }
    }

}

