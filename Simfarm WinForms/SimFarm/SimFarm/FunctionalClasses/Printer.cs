using System;
namespace SimFarm
{
    static class Printer
    {
        public static void PrintTerrain(Terrain terrain)
        {
            string line = "";

            for (int i = 0; i < 100; i++)
            {
                if (i != 0 && i % 10 == 0)
                {
                    line += "\n";
                }

                line += terrain.Blocks[i].Name;
            }

            Console.WriteLine(line);
        }

        private static void GetMapLine(Map map, int terrainStart, int lineNumber)
        {
            string line = "";
            for (int i = terrainStart; i < terrainStart + 10; i++)
            {

                Terrain terrain = map.Terrains[i];

                if (terrain.Bought)
                {
                    line += "CCCCCCCCCC";
                }
                else
                {
                    line += terrain.Blocks[0 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[1 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[2 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[3 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[4 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[5 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[6 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[7 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[8 + (lineNumber * 10)].Name;
                    line += terrain.Blocks[9 + (lineNumber * 10)].Name;
                }
                line += "  ";
            }
            Console.WriteLine(line);
        }

        public static void PrintMap(Map map)
        {

            for (int i = 0; i < 100; i += 10)
            {
                for (int j = 0; j < 10; j++)
                {
                    GetMapLine(map, i, j);
                }
                Console.WriteLine();
            }
        }
    }
}