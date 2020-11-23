using System;
using System.Collections.Generic;
using System.Text;
using SimFarm.Interfaces;

namespace SimFarm
{
    [Serializable]
    public class FieldBlock : Block, IFarmable
    {
        private double quality;
        public FieldBlock() : base(name: 'T')
        {
            Random r = new Random();
            this.quality = r.Next(25, 101);
        }

        public double Quality { get => quality; }

        void IFarmable.Farm()
        {
            throw new NotImplementedException();
        }
    }
}