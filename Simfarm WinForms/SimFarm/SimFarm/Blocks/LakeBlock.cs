using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm
{
    [Serializable]
    public class LakeBlock : Block
    {
        public LakeBlock() : base(name: 'L')
        {
        }
    }
}