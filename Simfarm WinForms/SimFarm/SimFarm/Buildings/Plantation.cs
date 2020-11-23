using System;
using System.Collections.Generic;
using System.Text;

using SimFarm.Products;

namespace SimFarm.Buildings
{
    [Serializable]
    public class Plantation : Building
    {
        private Seed seed;
        private int nutrients;
        private bool hasWorms;
        private bool hasUndergrowth;
        private int health;
        private int water;
        private int maturity;
        private int finalProduction;
        private bool sick;
        private bool hasWormsPast;
        private bool hasUndergrowthPast;
        private bool sickPast;

        public Plantation(Seed seed, string name, int purchasePrice, int salePrice, int health, int finalProduction) : base(name, purchasePrice, salePrice)
        {
            this.seed = seed;
            this.nutrients = 100;
            this.hasWorms = false;
            this.hasUndergrowth = false;
            this.health = health;
            this.water = 100;
            this.maturity = 0;
            this.finalProduction = finalProduction;
            this.sick = false;
            this.sickPast = false;
            this.hasWormsPast = false;
            this.hasUndergrowthPast = false;
        }

        public Seed Seed { get => seed; }
        public int Nutrients { get => nutrients; set => nutrients = value; }
        public bool HasWorms { get => hasWorms; set => hasWorms = value; }
        public bool HasUndergrowth { get => hasUndergrowth; set => hasUndergrowth = value; }
        public int Health { get => health; set => health = value; }
        public int Water { get => water; set => water = value; }
        public int Maturity { get => maturity; set => maturity = value; }
        public int FinalProduction { get => finalProduction; }
        public bool Sick { get => sick; set => sick = value; }
        public bool SickPast { get => sickPast; set => sickPast = value; }
        public bool HasWormsPast { get => hasWormsPast; set => hasWormsPast = value; }
        public bool HasUndergrowthPast { get => hasUndergrowthPast; set => hasUndergrowthPast = value; }

        public void GetWorms()
        {
            int worms = seed.ProbabilityOfWorms;
            Random r = new Random();
            int probability = r.Next(100);
            if (probability <= worms)
            {
                hasWorms = true;
            }

        }

        public void GetUndergrowth()
        {
            int worms = seed.WeedProbability;
            Random r = new Random();
            int probability = r.Next(100);
            if (probability <= worms)
            {
                hasUndergrowth = true;
            }
        }
        public void GetSick()
        {
            int worms = seed.ProbabilityOfIllness;
            Random r = new Random();
            int probability = r.Next(100);
            if (probability <= worms)
            {
                sick = true;
            }
        }
    }
}

