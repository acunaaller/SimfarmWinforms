using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm
{
    [Serializable]
    abstract public class Block
    {
        private char name;

        public Block(char name)
        {
            this.name = name;
        }

        public char Name { get => name; set => name = value; }
    }
}