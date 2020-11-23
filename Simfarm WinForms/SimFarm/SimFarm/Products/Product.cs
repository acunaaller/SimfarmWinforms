using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Products
{
    [Serializable]
    abstract public class Product
    {
        private string name;
        private int price;
        private int basePrice;
        private int waterConsume;
        private int minimumWaterLevel;
        private int penaltyForLackOfWater;
        private int productionTime;
        private int probabilityOfIllness;
        private int penaltyForIllness;


        public Product(string name, int price, int basePrice, int waterConsume, int minimumWaterLevel, int penaltyForLackOfWater, int productionTime, int probabilityOfIllness, int penaltyForIllness)
        {
            this.name = name;
            this.price = price;
            this.basePrice = basePrice;
            this.waterConsume = waterConsume;
            this.minimumWaterLevel = minimumWaterLevel;
            this.penaltyForLackOfWater = penaltyForLackOfWater;
            this.productionTime = productionTime;
            this.probabilityOfIllness = probabilityOfIllness;
            this.penaltyForIllness = penaltyForIllness;
        }

        public string Name { get => name; }
        public int Price { get => price; set => price = value; }
        public int BasePrice { get => basePrice; }
        public int WaterConsume { get => waterConsume; }
        public int MinimumWaterLevel { get => minimumWaterLevel; }
        public int PenaltyForLackOfWater { get => penaltyForLackOfWater; }
        public int ProductionTime { get => productionTime; }
        public int ProbabilityOfIllness { get => probabilityOfIllness; }
        public int PenaltyForIllness { get => penaltyForIllness; }
    }
}
