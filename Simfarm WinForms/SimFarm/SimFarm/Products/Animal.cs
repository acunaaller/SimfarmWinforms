using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm.Products
{
    [Serializable]
    public class Animal : Product
    {
        private int foodConsumption;
        private int minimumFoodLevel;
        private int penaltyForLackOfFood;
        private int units;
        private int escapeProbability;
        private int initialRangeOfUnitsEscaping;
        private int finalRangeOfEscapingUnits;
        private int probabilityOfSuddenDeath;
        private int initialRangeOfDyingSuddenly;
        private int finalRangeOfDyingSuddenly;

        public Animal(string name, int price, int basePrice, int foodConsumption, int waterConsume, int minimumFoodLevel, int minimumWaterLevel, int penaltyForLackOfFood, int penaltyForLackOfWater, int productionTime, int probabilityOfIllness, int penaltyForIllness, int units, int escapeProbability, int initialRangeOfUnitsEscaping, int finalRangeOfEscapingUnits, int probabilityOfSuddenDeath, int initialRangeOfDyingSuddenly, int finalRangeOfDyingSuddenly) : base(name, price, basePrice, waterConsume, minimumWaterLevel, penaltyForLackOfWater, productionTime, probabilityOfIllness, penaltyForIllness)
        {
            this.foodConsumption = foodConsumption;
            this.minimumFoodLevel = minimumFoodLevel;
            this.penaltyForLackOfFood = penaltyForLackOfFood;
            this.units = units;
            this.escapeProbability = escapeProbability;
            this.initialRangeOfUnitsEscaping = initialRangeOfUnitsEscaping;
            this.finalRangeOfEscapingUnits = finalRangeOfEscapingUnits;
            this.probabilityOfSuddenDeath = probabilityOfSuddenDeath;
            this.initialRangeOfDyingSuddenly = initialRangeOfDyingSuddenly;
            this.finalRangeOfDyingSuddenly = finalRangeOfDyingSuddenly;
        }

        public int FoodConsumption { get => foodConsumption; }
        public int MinimumFoodLevel { get => minimumFoodLevel; }
        public int PenaltyForLackOfFood { get => penaltyForLackOfFood; }
        public int Units { get => units; }
        public int EscapeProbability { get => escapeProbability; }
        public int InitialRangeOfUnitsEscaping { get => initialRangeOfUnitsEscaping; }
        public int FinalRangeOfEscapingUnits { get => finalRangeOfEscapingUnits; }
        public int ProbabilityOfSuddenDeath { get => probabilityOfSuddenDeath; }
        public int InitialRangeOfDyingSuddenly { get => initialRangeOfDyingSuddenly; }
        public int FinalRangeOfDyingSuddenly { get => finalRangeOfDyingSuddenly; }
    }
}