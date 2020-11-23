using System;
using System.Collections.Generic;
using System.Text;

using SimFarm.Interfaces;

namespace SimFarm.Consumables
{
    [Serializable]
    public class Vaccine : Consumable, ICheckSuccess
    {
        private int probabilityOfSuccess;
        public Vaccine(int probabilityOfSuccess) : base(name: "Vacuna", numberOfUses: 1, price: 10, quantityAdded: 0)
        {
            this.probabilityOfSuccess = probabilityOfSuccess;
        }

        public int ProbabilityOfSuccess { get => probabilityOfSuccess; }

        public bool CheckSuccess()
        {
            Random r = new Random();
            int firstNumber = r.Next(0, 101); // Numero del 0...100

            return probabilityOfSuccess >= firstNumber;
        }
    }
}