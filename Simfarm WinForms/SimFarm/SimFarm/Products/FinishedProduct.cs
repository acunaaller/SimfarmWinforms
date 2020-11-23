using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Products
{
    [Serializable]
    public class FinishedProduct : Product
    {

        private int quality;

        public FinishedProduct(string name, int price, int basePrice, int waterConsume, int minimumWaterLevel, int penaltyForLackOfWater, int productionTime, int probabilityOfIllness, int penaltyForIllness, int quality) : base(name, price, basePrice, waterConsume, minimumWaterLevel, penaltyForLackOfWater, productionTime, probabilityOfIllness, penaltyForIllness)
        {

            this.quality = quality;
        }


        public int Quality { get => quality; }

        public void DicreaseQuality()
        {
            quality -= 1;
            Console.WriteLine($"La calidad del producto {Name} bajo a {quality}");
        }
    }
}