using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Products
{
    [Serializable]
    public class Seed : Product
    {
        private int priceVariation;
        private int nutrientConsumption;
        private int minimumLevelOfNutrients;
        private int penaltyForLackOfNutrients;
        private int probabilityOfWorms;
        private int wormPenalty;
        private int weedProbability;
        private int weedPenalty;
        public List<int> priceHistory = new List<int>();

        public Seed(string name, int price, int basePrice, int priceVariation, int nutrientConsumption, int waterConsume, int minimumLevelOfNutrients, int minimumWaterLevel, int penaltyForLackOfNutrients, int penaltyForLackOfWater, int productionTime, int probabilityOfIllness, int penaltyForIllness, int probabilityOfWorms, int wormPenalty, int weedProbability, int weedPenalty) : base(name, price, basePrice, waterConsume, minimumWaterLevel, penaltyForLackOfWater, productionTime, probabilityOfIllness, penaltyForIllness)
        {
            this.priceVariation = priceVariation;
            this.nutrientConsumption = nutrientConsumption;
            this.minimumLevelOfNutrients = minimumLevelOfNutrients;
            this.penaltyForLackOfNutrients = penaltyForLackOfNutrients;
            this.probabilityOfWorms = probabilityOfWorms;
            this.wormPenalty = wormPenalty;
            this.weedProbability = weedProbability;
            this.weedPenalty = weedPenalty;
        }


        public int PriceVariation { get => priceVariation; }
        public int NutrientConsumption { get => nutrientConsumption; }
        public int MinimumLevelOfNutrients { get => minimumLevelOfNutrients; }
        public int PenaltyForLackOfNutrients { get => penaltyForLackOfNutrients; }
        public int ProbabilityOfWorms { get => probabilityOfWorms; }
        public int WormPenalty { get => wormPenalty; }
        public int WeedProbability { get => weedProbability; }
        public int WeedPenalty { get => weedPenalty; }

        public void InitiatePriceHistory()
        {
            int xprice = BasePrice;
            for (int i = 0; i < 30; i++)
            {
                Random r = new Random();

                int var = r.Next(-1, 2);
                xprice = xprice + priceVariation * var;
                if (Math.Abs(xprice - BasePrice) > (BasePrice * 0.4))
                {
                    xprice = BasePrice;
                }
                priceHistory.Add(xprice);
            }
            Price = xprice;
        }

        public void AddPriceHistory()
        {
            int xprice = priceHistory[29];
            Random r = new Random();

            int var = r.Next(-1, 2);
            xprice = xprice + priceVariation * var;
            if (Math.Abs(xprice - BasePrice) > (BasePrice * 0.4))
            {
                xprice = BasePrice;
            }
            priceHistory.Add(xprice);
            priceHistory.RemoveAt(0);
            Price = xprice;
        }

        public void ShowPriceHistory()
        {
            int counter = 29;
            Console.WriteLine("Dias transcurridos-Precio");
            foreach (int priceHistoryValue in priceHistory)
            {
                Console.WriteLine($"Precio hace {counter} dias: ${priceHistoryValue}");
                counter -= 1;
            }
        }
    }
}