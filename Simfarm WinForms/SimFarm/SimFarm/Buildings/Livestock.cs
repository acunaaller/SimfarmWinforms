using System;
using System.Collections.Generic;
using System.Text;
using SimFarm.Products;

namespace SimFarm.Buildings
{
    [Serializable]
    public class Livestock : Building
    {
        private Animal animal;
        private int food;
        private int units;
        private int health;
        private int water;
        private int maturity;
        private int finalProduction;
        private bool sick;
        private bool sickPast;

        public Livestock(Animal animal, int food, int units, string name, int purchasePrice, int salePrice, int health, int finalProduction) : base(name, purchasePrice, salePrice)
        {
            this.animal = animal;
            this.food = 100;
            this.units = units;
            this.health = health;
            this.water = 100;
            this.maturity = 0;
            this.finalProduction = finalProduction;
            this.sick = false;
            this.sickPast = false;
        }

        public Animal Animal { get => animal; }
        public int Food { get => food; set => food = value; }
        public int Units { get => units; set => units = value; }
        public int Health { get => health; set => health = value; }
        public int Water { get => water; set => water = value; }
        public int Maturity { get => maturity; set => maturity = value; }
        public int FinalProduction { get => finalProduction; }
        public bool Sick { get => sick; set => sick = value; }
        public bool SickPast { get => sickPast; set => sickPast = value; }
        public void GetSick()
        {
            int illnes = animal.ProbabilityOfIllness;
            Random r = new Random();
            int probability = r.Next(100);
            if (probability <= illnes)
            {
                Sick = true;
            }
        }
        public void SuddenDeath()
        {
            Random r = new Random();
            int probability = r.Next(100);
            if (probability < animal.ProbabilityOfSuddenDeath)
            {
                int aux = r.Next(Animal.InitialRangeOfDyingSuddenly, Animal.FinalRangeOfDyingSuddenly + 1);
                if (aux <= Units)
                {
                    Units -= aux;
                    Console.WriteLine($"-Sufrieron muerte subita {aux} animales de {Name}, quedan {Units} unidades.");

                }
                else
                {
                    Console.WriteLine($"-Sufrieron muerte subita todos los animales de {Name}, quedan 0 unidades.");
                    Units = 0;
                }

            }
        }

        public void Escape()
        {

            Random r = new Random();
            int probability = r.Next(100);
            if (probability < animal.EscapeProbability)
            {
                int aux = r.Next(Animal.InitialRangeOfUnitsEscaping, Animal.FinalRangeOfEscapingUnits + 1);
                if (aux <= Units)
                {
                    Units -= aux;
                    Console.WriteLine($"-Escaparon {aux} animales de {Name}, quedan {Units} unidades.");

                }
                else
                {
                    Console.WriteLine($"-Escaparon todos los animales de {Name}, quedan 0 unidades.");
                    Units = 0;
                }

            }
        }
    }
}