using System;
using System.Collections.Generic;
using System.Text;

namespace SimFarm
{
    [Serializable]
    public class Map
    {
        private List<Terrain> terrains = new List<Terrain>();

        public Map()
        {
            GenerateMap();
        }

        public List<Terrain> Terrains { get => terrains; }

        private void GenerateMap()
        {
            for (int i = 0; i < 100; i++)
            {
                Terrain terrain = new Terrain();
                terrains.Add(terrain);
            }
        }

        public void GenerateRiver()
        {
            Random r = new Random();

            int direction = r.Next(2);

            // Horizontal
            if (direction == 0)
            {
                int position = r.Next(10) * 10;

                for (int i = position; i < position + 10; i++)
                {
                    terrains[i].MakeHorizontalRiverTerrain();
                }
            }

            // Vertical
            else
            {
                int position = r.Next(10);
                int riverBlocksPostion = r.Next(2);
                for (int i = position; i < position + 100; i += 10)
                {
                    terrains[i].MakeVerticalRiverTerrain();
                }
            }
        }

        public void GenerateLake()
        {
            Random r = new Random();
            int terrainPosition;
            while (true)
            {
                terrainPosition = r.Next(0, 88);
                if (terrainPosition % 10 < 9) { break; }
            }


            terrains[terrainPosition].GenerateLake();
            terrains[terrainPosition + 10].GenerateBottomLake();
            terrains[terrainPosition + 1].GenerateSideLake();
            terrains[terrainPosition + 11].GenerateCornerLake();


        }

        public string GetTerrainPositionString(Terrain terrain)
        {
            float terrainRow = terrains.IndexOf(terrain) / 10;
            float terrainColumn = terrains.IndexOf(terrain) % 10;

            return $"Fila: {terrainRow + 1} - Columna: {terrainColumn + 1}";
        }

        public int getTerrainIndex(List<Terrain> terrains, bool userTerrains)
        {
            Console.WriteLine("\n¿Que terreno desea comprar?");
            List<int> notAvailableTerrains = new List<int>();

            for (int i = 0; i < this.terrains.Count; i++)
            {
                if (terrains.Contains(this.terrains[i]) == userTerrains)
                {
                    string terrainPosition = this.GetTerrainPositionString(this.Terrains[i]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"({i + 1 }) - {terrainPosition}");
                }
                else
                {
                    Console.WriteLine($"({i + 1 }) - Terreno no disponible.");
                    notAvailableTerrains.Add(i);
                }
                Console.ResetColor();
            }

            int userOption = Int32.Parse(Console.ReadLine());
            if (userOption < 1 || userOption > this.terrains.Count || notAvailableTerrains.Contains(userOption - 1))
            {
                return -1;
            }

            return userOption - 1;
        }
    }
}
