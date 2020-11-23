using System;
using System.Collections.Generic;
using SimFarm.Buildings;

namespace SimFarm
{
    [Serializable]
    public class Terrain
    {
        private List<Block> blocks = new List<Block>();
        private bool bought = false;

        private Building building;

        public Terrain()
        {
            GenerateTerrain();
        }

        public List<Block> Blocks { get => blocks; }
        public bool Bought { get => bought; }
        public Building Building { get => building; set => building = value; }

        private void GenerateTerrain()
        {
            for (int i = 0; i < 100; i++)
            {
                FieldBlock fieldBlock = new FieldBlock();
                blocks.Add(fieldBlock);
            }
        }

        public void Buy()
        {
            this.bought = true;
        }

        // River methods

        public void MakeHorizontalRiverTerrain()
        {
            blocks.Clear();


            for (int i = 0; i < 50; i++)
            {
                RiverBlock riverBlock = new RiverBlock();
                blocks.Add(riverBlock);
            }

            for (int i = 50; i < 100; i++)
            {
                FieldBlock fieldBlock = new FieldBlock();
                blocks.Add(fieldBlock);
            }


        }

        public void MakeVerticalRiverTerrain()
        {
            blocks.Clear();
            for (int i = 0; i < 10; i++)
            {


                for (int j = 0; j < 5; j++)
                {
                    RiverBlock riverBlock = new RiverBlock();
                    blocks.Add(riverBlock);
                }

                for (int k = 5; k < 10; k++)
                {
                    FieldBlock fieldBlock = new FieldBlock();
                    blocks.Add(fieldBlock);
                }



            }
        }

        // Lake methods
        public void GenerateLake()
        {
            blocks.Clear();

            for (int i = 0; i < 100; i++)
            {
                LakeBlock lakeBlock = new LakeBlock();
                blocks.Add(lakeBlock);
            }
        }

        public void GenerateBottomLake()
        {
            blocks.Clear();
            for (int i = 0; i < 50; i++)
            {
                LakeBlock lakeBlock = new LakeBlock();
                blocks.Add(lakeBlock);
            }
            for (int i = 50; i < 100; i++)
            {

                FieldBlock fieldBlock = new FieldBlock();
                blocks.Add(fieldBlock);
            }


        }

        public void GenerateSideLake()
        {
            blocks.Clear();
            for (int i = 0; i < 100; i++)
            {
                if (i % 10 < 5)
                {
                    LakeBlock lakeBlock = new LakeBlock();
                    blocks.Add(lakeBlock);
                }
                else
                {
                    FieldBlock fieldBlock = new FieldBlock();
                    blocks.Add(fieldBlock);
                }



            }
        }

        public void GenerateCornerLake()
        {
            blocks.Clear();
            for (int i = 0; i < 50; i++)
            {
                if (i % 10 < 5)
                {
                    LakeBlock lakeBlock = new LakeBlock();
                    blocks.Add(lakeBlock);
                }
                else
                {
                    FieldBlock fieldBlock = new FieldBlock();
                    blocks.Add(fieldBlock);
                }



            }
            for (int i = 50; i < 100; i++)
            {

                FieldBlock fieldBlock = new FieldBlock();
                blocks.Add(fieldBlock);
            }
        }

        public bool HasBuilding()
        {
            return building != null;
        }
    }
}