using System;
using System.Collections.Generic;
using SimFarm.Products;
using SimFarm.Buildings;
using SimFarm.Consumables;

namespace SimFarm
{
    [Serializable]
    public class Market
    {
        private List<Seed> seeds = new List<Seed>();
        private List<Animal> animals = new List<Animal>();
        private List<Consumable> consumables = new List<Consumable>();

        public List<Seed> Seeds { get => seeds; }
        public List<Animal> Animals { get => animals; }
        public List<Consumable> Consumables { get => consumables; }

        public void AddSeed(Seed seed)
        {
            seeds.Add(seed);
        }

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public void AddConsumable(Consumable consumable)
        {
            consumables.Add(consumable);
        }

        public void BuyPlantation(Player player, Map map)
        {
            int selectedTerrainIndex = map.getTerrainIndex(player.Terrains, true);

            if (selectedTerrainIndex == -1)
            {
                Console.WriteLine("Opción no valida...");
            }
            else
            {
                Terrain selectedTerrain = map.Terrains[selectedTerrainIndex];
                int playerTerrainIndex = player.Terrains.IndexOf(selectedTerrain);
                Terrain terrain = player.Terrains[playerTerrainIndex];
                int finalPrice = 0;

                // Preguntamos que semilla quiere comprar.
                Console.WriteLine("¿Que semilla desea plantar?");
                for (int i = 0; i < seeds.Count; i++)
                {
                    Console.WriteLine($"({i + 1}) - {seeds[i].Name} - ${seeds[i].Price}");
                }

                // Conseguimos la semilla
                int userOption = Int32.Parse(Console.ReadLine());

                if (userOption < 1 || userOption > seeds.Count)
                {
                    Console.WriteLine("Opción no valida...");
                    return;
                }

                Seed userSeed = seeds[userOption - 1];

                // Vemos si el terreno tiene un building
                if (terrain.HasBuilding())
                {
                    //Temp prueba de concepto
                    finalPrice += 30;
                }

                finalPrice += userSeed.Price;
                Plantation plantation = new Plantation(userSeed, $"{userSeed.Name} en {map.GetTerrainPositionString(terrain)}", finalPrice, finalPrice, 100, 100);

                finalPrice += plantation.PurchasePrice;

                Console.WriteLine($"El precio final es de {finalPrice}.");
                Console.WriteLine("¿Desea comprarlo? y/n");
                string userWillBuy = Console.ReadLine();

                if (userWillBuy == "y")
                {
                    if (player.Money >= finalPrice)
                    {
                        // Comprar
                        terrain.Building = plantation;
                        player.Money = player.Money - finalPrice;
                        player.AddBuilding(plantation);
                        Console.WriteLine("Se compró la plantación exitosamente.");
                        Console.WriteLine($"Tu dinero restante es de {player.Money}.");
                    }
                    else
                    {
                        Console.WriteLine("No tienes el suficiente dinero para comprar esta plantación.");
                    }
                }
                else
                {
                    Console.WriteLine("No se compro nada.");
                }
            }
        }

        public void BuyLivestock(Player player, Map map)
        {
            int selectedTerrainIndex = map.getTerrainIndex(player.Terrains, true);

            if (selectedTerrainIndex == -1)
            {
                Console.WriteLine("Opción no valida...");
            }
            else
            {
                Terrain selectedTerrain = map.Terrains[selectedTerrainIndex];
                int playerTerrainIndex = player.Terrains.IndexOf(selectedTerrain);
                Terrain terrain = player.Terrains[playerTerrainIndex];
                int finalPrice = 0;

                // Preguntamos que animal quiere comprar.
                Console.WriteLine("¿Que animal desea comprar?");
                for (int i = 0; i < animals.Count; i++)
                {
                    Console.WriteLine($"({i + 1}) - {animals[i].Name} - ${animals[i].Price}");
                }

                // Conseguimos el animal
                int userOption = Int32.Parse(Console.ReadLine());

                if (userOption < 1 || userOption > animals.Count)
                {
                    Console.WriteLine("Opción no valida...");
                    return;
                }

                Animal userAnimal = animals[userOption - 1];

                // Vemos si el terreno tiene un building
                if (terrain.HasBuilding())
                {
                    //Temp prueba de concepto
                    finalPrice += 30;
                }

                finalPrice += userAnimal.Price;

                Livestock livestock = new Livestock(userAnimal, 10, 10, $"{userAnimal.Name} en {map.GetTerrainPositionString(terrain)}", 100, 100, 100, 100);

                finalPrice += livestock.PurchasePrice;

                Console.WriteLine($"El precio final es de {finalPrice}.");
                Console.WriteLine("¿Desea comprarlo? y/n");
                string userWillBuy = Console.ReadLine();

                if (userWillBuy == "y")
                {
                    if (player.Money >= finalPrice)
                    {
                        // Comprar
                        terrain.Building = livestock;
                        player.Money = player.Money - finalPrice;
                        player.AddBuilding(livestock);
                        Console.WriteLine("Se compró el animal exitosamente.");
                        Console.WriteLine($"Tu dinero restante es de {player.Money}.");
                    }
                    else
                    {
                        Console.WriteLine("No tienes el suficiente dinero para comprar este ganado.");
                    }

                }
                else
                {
                    Console.WriteLine("No se compro nada.");
                }
            }
        }

        public void BuyStorageBuilding(Player player, Map map)
        {
            int finalPrice = 0;
            int selectedTerrainIndex = map.getTerrainIndex(player.Terrains, true);

            if (selectedTerrainIndex == -1)
            {
                Console.WriteLine("Opción no valida...");
            }
            else
            {
                Terrain selectedTerrain = map.Terrains[selectedTerrainIndex];
                int playerTerrainIndex = player.Terrains.IndexOf(selectedTerrain);
                Terrain terrain = player.Terrains[playerTerrainIndex];

                if (terrain.HasBuilding())
                {
                    // Temp value
                    finalPrice += 30;
                }

                Console.WriteLine("¿Que nombre le quieres poner a tu Edificio de Almacenamiento?");
                string storageBuildingName = Console.ReadLine();

                bool canBuy = true;
                foreach (Building userBuilding in player.Buildings)
                {
                    if (userBuilding.Name == storageBuildingName)
                    {
                        canBuy = false;
                        break;
                    }
                }

                if (canBuy)
                {
                    StorageBuilding storageBuilding = new StorageBuilding($"{storageBuildingName} en {map.GetTerrainPositionString(terrain)}", 100, 100, 3);
                    finalPrice += storageBuilding.PurchasePrice;

                    Console.WriteLine($"El precio final es de {finalPrice}.");
                    Console.WriteLine("¿Desea comprarlo? y/n");
                    string userWillBuy = Console.ReadLine();

                    if (userWillBuy == "y")
                    {
                        if (player.Money >= finalPrice)
                        {
                            // Comprar
                            terrain.Building = storageBuilding;
                            player.Money = player.Money - finalPrice;
                            player.AddBuilding(storageBuilding);
                            Console.WriteLine("Se compró el edificio de almacenamiento exitosamente.");
                            Console.WriteLine($"Tu dinero restante es de {player.Money}.");
                        }
                        else
                        {
                            Console.WriteLine("No tienes el suficiente dinero para comprar este edificio de almacenamiento.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("No se compro nada.");
                    }
                }
                else
                {
                    Console.WriteLine("No puedes tener 2 edificios de almacenamiento con el mismo nombre.");
                }
            }
        }

        public void SellOrDestroyBuilding(Player player, Map map)
        {
            int selectedTerrainIndex = map.getTerrainIndex(player.Terrains, true);

            if (selectedTerrainIndex == -1)
            {
                Console.WriteLine("Opción no valida...");
            }
            else
            {
                Terrain selectedTerrain = map.Terrains[selectedTerrainIndex];
                int playerTerrainIndex = player.Terrains.IndexOf(selectedTerrain);
                Terrain terrain = player.Terrains[playerTerrainIndex];

                if (terrain.HasBuilding())
                {
                    if (terrain.Building.GetType() == typeof(Plantation) || terrain.Building.GetType() == typeof(Livestock))
                    {
                        Console.WriteLine($"Te costara {terrain.Building.SalePrice} vender este terreno.");
                        Console.WriteLine("¿Desea venderlo? y/n");
                        string userWillSell = Console.ReadLine();

                        if (userWillSell == "y")
                        {
                            if (player.Money >= terrain.Building.SalePrice)
                            {
                                player.Money = player.Money - terrain.Building.SalePrice;
                                terrain.Building = null;
                                Console.WriteLine("Se vendio el edificio exitosamente.");
                                Console.WriteLine($"Tu dinero restante es de {player.Money}.");
                            }
                            else
                            {
                                Console.WriteLine("No tienes el suficiente dinero para vender este terreno.");

                            }
                        }
                        else
                        {
                            Console.WriteLine("No se vendio nada.");
                        }
                    }
                    else
                    {
                        // Si es bodega, suma el precio de venta mas remate de productos dentro

                        int finalPrice = 0;

                        StorageBuilding userStorageBuilding = terrain.Building as StorageBuilding;

                        foreach (Product product in userStorageBuilding.Products)
                        {
                            finalPrice += product.Price;
                        }

                        finalPrice += terrain.Building.SalePrice;

                        Console.WriteLine($"Ganaras {terrain.Building.SalePrice} por vender este terreno.");
                        Console.WriteLine("¿Desea venderlo? y/n");
                        string userWillSell = Console.ReadLine();

                        if (userWillSell == "y")
                        {
                            player.Money = player.Money + finalPrice;
                            terrain.Building = null;
                            Console.WriteLine("Se vendio el edificio exitosamente.");
                            Console.WriteLine($"Tu dinero restante es de {player.Money}.");
                        }
                        else
                        {
                            Console.WriteLine("No se vendio nada.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No tienes un edificio para vender en este terreno.");
                }
            }
        }

        public void ConsumablesMarket(Player player)
        {
            Console.WriteLine("\n¿Que producto desea comprar?");
            for (int i = 0; i < consumables.Count; i++)
            {
                Console.WriteLine($"({i + 1}) - {consumables[i].Name} - ${consumables[i].Price}");
            }

            int userOption = Int32.Parse(Console.ReadLine());

            if (userOption < 1 || userOption > consumables.Count)
            {
                Console.WriteLine("Opción no valida...");
            }
            else
            {
                Consumable userConsumable = consumables[userOption - 1];

                if (player.Money >= userConsumable.Price)
                {
                    player.Money = player.Money - userConsumable.Price;
                    player.AddConsumable(userConsumable);

                    Console.WriteLine("Se compro el producto exitosamente.");
                    Console.WriteLine($"Tu dinero restante es de {player.Money}.");
                }
                else
                {
                    Console.WriteLine("No tienes el suficiente dinero para comprar este producto.");
                }
            }


        }

        public void TerrainMarket(Player player, Map map)
        {
            // Precio base * calidad del terreno * proporcion de tierra
            int selectedTerrainIndex = map.getTerrainIndex(player.Terrains, false);

            if (selectedTerrainIndex == -1)
            {
                Console.WriteLine("Opción no valida...");
            }
            else
            {
                Terrain terrain = map.Terrains[selectedTerrainIndex];

                double fieldProportion = 0;
                double fieldQuality = 0;

                foreach (Block block in terrain.Blocks)
                {
                    if (block.GetType() == typeof(FieldBlock))
                    {
                        fieldProportion += 1;

                        FieldBlock fieldBlock = block as FieldBlock;
                        fieldQuality += fieldBlock.Quality;
                    }
                }

                fieldProportion = fieldProportion / 100;
                fieldQuality = fieldQuality / 100;

                int finalPrice = Convert.ToInt32(100 * fieldQuality * fieldProportion);

                Console.WriteLine($"El precio del terreno es de ${finalPrice}");
                Console.WriteLine("¿Desea comprarlo? y/n");
                string userWillBuy = Console.ReadLine();

                if (userWillBuy == "y")
                {
                    if (player.Money >= finalPrice)
                    {
                        player.Money = player.Money - finalPrice;
                        player.BuyTerrain(terrain);
                        Console.WriteLine("Se compro el terreno exitosamente.");
                        Console.WriteLine($"Tu dinero restante es de {player.Money}.");
                    }
                    else
                    {
                        Console.WriteLine("No tienes el suficiente dinero para comprar este terreno.");

                    }
                }
                else
                {
                    Console.WriteLine("No se compro nada.");
                }
            }
        }

        public void SeedsPriceHistory()
        {
            Console.WriteLine("\n¿De que semilla quiere ver el historial de precio?");
            for (int i = 0; i < seeds.Count; i++)
            {
                Console.WriteLine($"({i + 1}) - {seeds[i].Name}");
            }

            int userOption = Int32.Parse(Console.ReadLine());

            if (userOption < 1 || userOption > seeds.Count)
            {
                Console.WriteLine("Opción no valida...");
            }
            else
            {
                Seed userSeed = seeds[userOption];
                userSeed.ShowPriceHistory();
            }
        }

        public int GetPriceOfSeed(Seed userSeed)
        {
            foreach (Seed seed in seeds)
            {
                if (seed.GetType() == userSeed.GetType())
                {
                    return seed.Price;
                }
            }

            return 0;
        }

        public int GetPriceOfAnimal(Animal userAnimal)
        {
            foreach (Animal animal in animals)
            {
                if (animal.GetType() == userAnimal.GetType())
                {
                    return animal.Price;
                }
            }

            return 0;
        }
    }
}