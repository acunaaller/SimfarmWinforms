using System;
using System.Collections.Generic;
using System.Text;
using SimFarm.Interfaces;

namespace SimFarm.Consumables
{
    [Serializable]
    public class Fungicide : Consumable, ICheckSuccess
    {
        private int probabilityOfSuccess;
        public Fungicide(int probabilityOfSuccess) : base(name: "Fungicida", numberOfUses: 1, price: 10, quantityAdded: 0)
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