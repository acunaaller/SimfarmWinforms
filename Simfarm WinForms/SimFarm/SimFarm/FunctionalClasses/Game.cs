using System;
using SimFarm.Products;
using SimFarm.Consumables;
using SimFarm.Buildings;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using SimFarm.Controllers;
namespace SimFarm
{
    [Serializable]
    public class Game
    {

        private Player player;
        private Map map;
        private Market market = new Market();
        private int turn = 0;
        private bool playing = true;
        private string[] loadOptions = { "Nuevo juego", "Cargar juego" };
        private string[] mapOptions = { "Crear solo terreno", "Crear terreno con lago", "Crear terreno con rio", "Crear terreno con lago y rio" };
        private string[] gameOptions = { "Administrar la granja", "Ir al mercado", "Pasar turno", "Grabar la partida", "Salir del juego" };
        private string[] administrationOptions = { "Administrar produccion", "Administrar almacenamiento", "Volver al menu principal" };
        private string[] productsAdministrationOptions = { "Agregar agua o comida", "Aplicar cura", "Obtener producto terminado", "Volver al menu principal" };
        private string[] buildingMarketOptions = { "Comprar una plantacion", "Comprar ganado", "Comprar almacenamiento", "Vender/Destruir edificio", "Volver al menu principal" };
        private string[] marketOptions = { "Mercado edficaciones", "Mercado consumibles", "Mercado propiedades", "Precios hisotricos por semilla", "Volver al menu principal" };

        public void Start()
        {
            // Load Options
            int selectedLoadOption = getUserOption(loadOptions, false);
            if (selectedLoadOption == 1)
            {
                // Nuevo Juego
                Console.Clear();
                Console.WriteLine("¿Como te llamas?");
                string userName = Console.ReadLine();

                // Creamos al jugador
                Console.WriteLine($"\n¡Hola {userName} empecemos!\n");
                player = new Player(userName, 50000);

                // Hacemos el mapa
                bool creatingMap = true;

                while (creatingMap)
                {
                    int selectedMapMenuOption = getUserOption(mapOptions, false);

                    if (CreateMap(selectedMapMenuOption))
                    {
                        creatingMap = false;
                    }

                    Console.Clear();
                }

                GiveFarmToPlayer();
                InitialiteSeeds();
                InitialiteAnimals();
                InitialiteConsumables();
            }
            else
            {
                // Cargar Juego
                LoadGame();
            }


            // Se Comienza el juego
            while (playing)
            {
                int selectedGameOption = getUserOption(gameOptions);
                switch (selectedGameOption)
                {
                    case 1:
                        AdministrateFarm(getUserOption(administrationOptions));
                        break;
                    case 2:
                        GoToTheMarket(getUserOption(marketOptions));
                        break;
                    case 3:
                        NextTurn();
                        break;
                    case 4:
                        SaveGame();
                        break;
                    case 5:
                        CloseGame();
                        break;
                }
            }
        }

        // Menu Options
        private int getUserOption(string[] menuOptions, bool showSummary = true)
        {
            bool selectingOption = true;
            int userOption = 0;

            while (selectingOption)
            {
                if (showSummary)
                {
                    ShowSummary();
                }
                Console.WriteLine("¿Que desea hacer?");
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    Console.WriteLine($"({i + 1}) - {menuOptions[i]}");
                }

                userOption = Int32.Parse(Console.ReadLine());

                if (userOption < 1 || userOption > menuOptions.Length + 1)
                {
                    Console.WriteLine("Opción no valida...");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    if (!showSummary)
                    {
                        Console.Clear();
                    }
                }
                else
                {
                    selectingOption = false;
                }
            }

            return userOption;
        }

        // Map Options
        private bool CreateMap(int type)
        {
            Map newMap = new Map();

            switch (type)
            {
                case 1:
                    // Crear mapa normal
                    Console.WriteLine("Creando mapa con solo terreno...");
                    break;
                case 2:
                    // Crear mapa con lago
                    Console.WriteLine("Creando mapa con lago...");
                    newMap.GenerateLake();
                    break;
                case 3:
                    // Crear mapa con rio
                    Console.WriteLine("Creando mapa con rio...");
                    newMap.GenerateRiver();
                    break;
                case 4:
                    // Crear mapa con lago y rio
                    Console.WriteLine("Creando mapa con lago y rio...");
                    newMap.GenerateLake();
                    newMap.GenerateRiver();
                    break;
            }

            bool selectingMap = true;

            while (selectingMap)
            {
                Printer.PrintMap(newMap);
                Console.WriteLine("¿Te gusta este mapa? (y/n)");
                string userOption = Console.ReadLine();

                if (userOption != "y" && userOption != "n")
                {
                    Console.WriteLine("Opción no valida");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    if (userOption == "y")
                    {
                        map = newMap;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        // Initializers
        private void InitialiteSeeds()
        {

            Seed tomate = new Seed("Tomate", 70, 100, 5, 1, 1, 5, 10, 10, 10, 5, 10, 10, 10, 10, 10, 10);
            Seed trigo = new Seed("Trigo", 70, 100, 5, 1, 1, 5, 10, 10, 10, 5, 10, 10, 10, 10, 10, 10);
            Seed palta = new Seed("Palta", 70, 100, 5, 1, 1, 5, 10, 10, 10, 5, 10, 10, 10, 10, 10, 10);

            tomate.InitiatePriceHistory();
            trigo.InitiatePriceHistory();
            palta.InitiatePriceHistory();

            market.AddSeed(tomate);
            market.AddSeed(trigo);
            market.AddSeed(palta);

        }

        private void InitialiteAnimals()
        {
            Animal vaca = new Animal("Vaca", 100, 50, 25, 20, 15, 20, 5, 10, 15, 10, 20, 7, 5, 3, 8, 20, 1, 4);
            Animal cerdo = new Animal("Cerdo", 100, 50, 25, 20, 15, 20, 5, 10, 15, 10, 20, 7, 5, 3, 8, 20, 1, 4);
            Animal pollo = new Animal("Pollo", 100, 50, 25, 20, 15, 20, 5, 10, 15, 10, 20, 7, 5, 3, 8, 20, 1, 4);

            market.AddAnimal(vaca);
            market.AddAnimal(cerdo);
            market.AddAnimal(pollo);
        }

        private void InitialiteConsumables()
        {
            AnimalFood animalFood = new AnimalFood();
            AnimalWater animalWater = new AnimalWater();
            Fertilizer fertilizer = new Fertilizer();
            Fungicide fungicide = new Fungicide(50);
            Herbicide herbicide = new Herbicide(50);
            Irrigation irrigation = new Irrigation();
            Pesticide pesticide = new Pesticide(50);
            Vaccine vaccine = new Vaccine(50);

            market.AddConsumable(animalFood);
            market.AddConsumable(animalWater);
            market.AddConsumable(fertilizer);
            market.AddConsumable(fungicide);
            market.AddConsumable(herbicide);
            market.AddConsumable(irrigation);
            market.AddConsumable(pesticide);
            market.AddConsumable(vaccine);

        }

        // Game methods

        private void ShowSummary()
        {
            Console.Clear();
            Printer.PrintMap(map);
            Console.WriteLine($"¡Hola {player.Name}!");
            Console.WriteLine($"Turno: {turn}");
            Console.WriteLine($"Dinero: {player.Money}");
            Console.WriteLine($"Terrenos (C): {player.Terrains.Count}\n");
        }

        private void AdministrateFarm(int option)
        {
            switch (option)
            {
                case 1:
                    AdministrateProducts(getUserOption(productsAdministrationOptions));
                    break;
                case 2:
                    // Administrar almacenamiento
                    AdministrateStorage();
                    break;
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void AdministrateStorage()
        {
            foreach (Building building in player.Buildings)
            {
                Console.WriteLine($"Almacenamiento: {building.Name}");
                if (building.GetType() == typeof(StorageBuilding))
                {
                    StorageBuilding storageBuilding = building as StorageBuilding;
                    int n = 1;
                    foreach (FinishedProduct finishedProduct in storageBuilding.Products)
                    {
                        Console.WriteLine($"{n} - {finishedProduct.Name}");
                        n++;
                    }
                    Console.WriteLine($"Seleccione el elemento a vender o 0 para continuar");
                    int userOption = Int32.Parse(Console.ReadLine());
                    if (userOption != 0 && userOption < storageBuilding.Products.Count)
                    {
                        FinishedProduct selectedProduct = storageBuilding.Products[userOption - 1];
                        int indexOfProduct = storageBuilding.Products.IndexOf(selectedProduct);
                        player.Money += selectedProduct.Price * selectedProduct.Quality;
                        storageBuilding.Products.RemoveAt(indexOfProduct);

                    }

                }
            }
        }
        private void AdministrateProducts(int option)
        {
            switch (option)
            {
                case 1:
                    AddWaterOrFood();
                    break;
                case 2:
                    ApplyCure();
                    break;
                case 3:
                    GetFinishedProduct();
                    break;
            }
        }

        private void AdministrateBuildingMarket(int option)
        {
            switch (option)
            {
                case 1:
                    market.BuyPlantation(player, map);
                    break;
                case 2:
                    market.BuyLivestock(player, map);
                    break;
                case 3:
                    market.BuyStorageBuilding(player, map);
                    break;
                case 4:
                    market.SellOrDestroyBuilding(player, map);
                    break;
            }
        }

        private void GoToTheMarket(int option)
        {
            switch (option)
            {
                case 1:
                    AdministrateBuildingMarket(getUserOption(buildingMarketOptions));
                    break;
                case 2:
                    market.ConsumablesMarket(player);
                    break;
                case 3:
                    market.TerrainMarket(player, map);
                    break;
                case 4:
                    market.SeedsPriceHistory();
                    break;
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void AddWaterOrFood()
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

                if (!terrain.HasBuilding())
                {
                    Console.WriteLine("No tienes un edificio en este terreno...");
                }
                else
                {
                    Building terrainBuilding = terrain.Building;
                    if (terrainBuilding.GetType() == typeof(Plantation))
                    {
                        Plantation plantationBuilding = terrainBuilding as Plantation;
                        List<Consumable> availableConsumables = new List<Consumable>();

                        bool hasAvailableConsumables = false;
                        while (!hasAvailableConsumables)
                        {
                            availableConsumables = player.GetAvailableConsumables(new List<string> { "Irrigation", "Fertilizer" });
                            string option = CheckIfCanAddConsumableToTerrain(availableConsumables);

                            if (option == "out")
                            {
                                return;
                            }
                            else if (option == "continue")
                            {
                                hasAvailableConsumables = true;
                            }
                        }

                        Console.WriteLine("Elige uno de los productos disponibles:");
                        for (int i = 0; i < availableConsumables.Count; i++)
                        {
                            Console.WriteLine($"({i + 1}) - {availableConsumables[i].Name}");
                        }

                        int userOption = Int32.Parse(Console.ReadLine());

                        if (userOption < 1 || userOption > availableConsumables.Count)
                        {
                            Console.WriteLine("Opción no valida...");
                            return;
                        }

                        Consumable selectedConsumable = availableConsumables[userOption - 1];

                        if (selectedConsumable.GetType() == typeof(Irrigation))
                        {
                            Irrigation irrigationConsumable = selectedConsumable as Irrigation;
                            plantationBuilding.Water += irrigationConsumable.QuantityAdded;
                            Console.WriteLine($"Se agrego riego correctamente. Nuevo nivel de agua: {plantationBuilding.Water}");
                        }
                        else if (selectedConsumable.GetType() == typeof(Fertilizer))
                        {
                            Fertilizer fertilizerConsumable = selectedConsumable as Fertilizer;
                            plantationBuilding.Nutrients += fertilizerConsumable.QuantityAdded;
                            Console.WriteLine($"Se agrego fertilizante correctamente. Nuevo nivel de nutrientes: {plantationBuilding.Nutrients}");
                        }
                        else
                        {
                            return;
                        }

                        int indexOfConsumable = player.consumables.IndexOf(selectedConsumable);
                        player.consumables.RemoveAt(indexOfConsumable);

                    }
                    else if (terrainBuilding.GetType() == typeof(Livestock))
                    {
                        Livestock liveStockBuilding = terrainBuilding as Livestock;
                        List<Consumable> availableConsumables = new List<Consumable>();

                        bool hasAvailableConsumables = false;
                        while (!hasAvailableConsumables)
                        {
                            availableConsumables = player.GetAvailableConsumables(new List<string> { "AnimalFood", "AnimalWater" });
                            string option = CheckIfCanAddConsumableToTerrain(availableConsumables);

                            if (option == "out")
                            {
                                return;
                            }
                            else if (option == "continue")
                            {
                                hasAvailableConsumables = true;
                            }
                        }

                        Console.WriteLine("Elige uno de los productos disponibles:");
                        for (int i = 0; i < availableConsumables.Count; i++)
                        {
                            Console.WriteLine($"({i + 1}) - {availableConsumables[i].Name}");
                        }

                        int userOption = Int32.Parse(Console.ReadLine());

                        if (userOption < 1 || userOption > availableConsumables.Count)
                        {
                            Console.WriteLine("Opción no valida...");
                            return;
                        }

                        Consumable selectedConsumable = availableConsumables[userOption - 1];

                        if (selectedConsumable.GetType() == typeof(AnimalWater))
                        {
                            AnimalWater animalWaterConsumable = selectedConsumable as AnimalWater;
                            liveStockBuilding.Water += animalWaterConsumable.QuantityAdded;
                            Console.WriteLine($"Se agrego agua correctamente. Nuevo nivel de agua: {liveStockBuilding.Water}");
                        }
                        else if (selectedConsumable.GetType() == typeof(AnimalFood))
                        {
                            AnimalFood animalFoodConsumable = selectedConsumable as AnimalFood;
                            liveStockBuilding.Food += animalFoodConsumable.QuantityAdded;
                            Console.WriteLine($"Se agrego comida correctamente. Nuevo nivel de nutrientes: {liveStockBuilding.Food}");
                        }
                        else
                        {
                            return;
                        }

                        int indexOfConsumable = player.consumables.IndexOf(selectedConsumable);
                        player.consumables.RemoveAt(indexOfConsumable);
                    }
                    else
                    {
                        return;
                    }
                }

            }
        }

        public void ApplyCure()
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

                if (!terrain.HasBuilding())
                {
                    Console.WriteLine("No tienes un edificio en este terreno...");
                }
                else
                {
                    Building terrainBuilding = terrain.Building;
                    if (terrainBuilding.GetType() == typeof(Plantation))
                    {
                        Plantation plantationBuilding = terrainBuilding as Plantation;
                        List<Consumable> availableConsumables = new List<Consumable>();

                        bool hasAvailableConsumables = false;
                        while (!hasAvailableConsumables)
                        {
                            availableConsumables = player.GetAvailableConsumables(new List<string> { "Herbicide", "Pesticide", "Fungicide" });
                            string option = CheckIfCanAddConsumableToTerrain(availableConsumables);

                            if (option == "out")
                            {
                                return;
                            }
                            else if (option == "continue")
                            {
                                hasAvailableConsumables = true;
                            }
                        }

                        Console.WriteLine("Elige uno de los productos disponibles:");
                        for (int i = 0; i < availableConsumables.Count; i++)
                        {
                            Console.WriteLine($"({i + 1}) - {availableConsumables[i].Name}");
                        }

                        int userOption = Int32.Parse(Console.ReadLine());

                        if (userOption < 1 || userOption > availableConsumables.Count)
                        {
                            Console.WriteLine("Opción no valida...");
                            return;
                        }

                        Consumable selectedConsumable = availableConsumables[userOption - 1];

                        if (selectedConsumable.GetType() == typeof(Herbicide))
                        {
                            Herbicide herbicideConsumable = selectedConsumable as Herbicide;
                            if (herbicideConsumable.CheckSuccess())
                            {
                                plantationBuilding.HasUndergrowth = false;
                                Console.WriteLine($"Herbicida funciono.");
                            }
                            else
                            {
                                Console.WriteLine($"Herbicida no funciono.");
                            }
                        }
                        else if (selectedConsumable.GetType() == typeof(Pesticide))
                        {
                            Pesticide pesticideConsumable = selectedConsumable as Pesticide;
                            if (pesticideConsumable.CheckSuccess())
                            {
                                plantationBuilding.HasWorms = false;
                                Console.WriteLine($"Pesticida funciono.");
                            }
                            else
                            {
                                Console.WriteLine($"Pesticida no funciono.");
                            }
                        }
                        else if (selectedConsumable.GetType() == typeof(Fungicide))
                        {
                            Fungicide fungicideConsumable = selectedConsumable as Fungicide;
                            if (fungicideConsumable.CheckSuccess())
                            {
                                plantationBuilding.Sick = false;
                                Console.WriteLine($"Fungicida funciono.");
                            }
                            else
                            {
                                Console.WriteLine($"Fungicida no funciono.");
                            }
                        }
                        else
                        {
                            return;
                        }

                        int indexOfConsumable = player.consumables.IndexOf(selectedConsumable);
                        player.consumables.RemoveAt(indexOfConsumable);

                    }
                    else if (terrainBuilding.GetType() == typeof(Livestock))
                    {
                        Livestock liveStockBuilding = terrainBuilding as Livestock;
                        List<Consumable> availableConsumables = new List<Consumable>();

                        bool hasAvailableConsumables = false;
                        while (!hasAvailableConsumables)
                        {
                            availableConsumables = player.GetAvailableConsumables(new List<string> { "Vaccine" });
                            string option = CheckIfCanAddConsumableToTerrain(availableConsumables);

                            if (option == "out")
                            {
                                return;
                            }
                            else if (option == "continue")
                            {
                                hasAvailableConsumables = true;
                            }
                        }

                        Console.WriteLine("Elige uno de los productos disponibles:");
                        for (int i = 0; i < availableConsumables.Count; i++)
                        {
                            Console.WriteLine($"({i + 1}) - {availableConsumables[i].Name}");
                        }

                        int userOption = Int32.Parse(Console.ReadLine());

                        if (userOption < 1 || userOption > availableConsumables.Count)
                        {
                            Console.WriteLine("Opción no valida...");
                            return;
                        }

                        Consumable selectedConsumable = availableConsumables[userOption - 1];

                        if (selectedConsumable.GetType() == typeof(Vaccine))
                        {
                            Vaccine vaccineConsumable = selectedConsumable as Vaccine;
                            if (vaccineConsumable.CheckSuccess())
                            {
                                liveStockBuilding.Sick = false;
                                Console.WriteLine($"Vacuna funciono.");
                            }
                            else
                            {
                                Console.WriteLine($"Vacuna no funciono.");
                            }
                        }
                        else
                        {
                            return;
                        }

                        int indexOfConsumable = player.consumables.IndexOf(selectedConsumable);
                        player.consumables.RemoveAt(indexOfConsumable);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private string CheckIfCanAddConsumableToTerrain(List<Consumable> consumables)
        {
            if (consumables.Count == 0)
            {
                Console.WriteLine("No tienes productos disponibles para utilizar...");
                Console.WriteLine("¿Desea comprar? (y/n)");
                string userOption = Console.ReadLine();

                if (userOption == "y")
                {
                    market.ConsumablesMarket(player);
                    return "again";
                }
                else
                {
                    return "out";
                }
            }

            return "continue";
        }

        private void GetFinishedProduct()
        {
            List<Building> finishedBuildings = new List<Building>();
            List<StorageBuilding> storageBuildings = new List<StorageBuilding>();

            foreach (Building building in player.buildings)
            {
                if (building.GetType() == typeof(Plantation))
                {
                    Plantation plantationBuilding = building as Plantation;
                    if (plantationBuilding.Maturity >= plantationBuilding.Seed.ProductionTime)
                    {
                        finishedBuildings.Add(plantationBuilding);
                    }
                }
                else if (building.GetType() == typeof(Livestock))
                {
                    Livestock livestockBuilding = building as Livestock;

                    if (livestockBuilding.Maturity >= livestockBuilding.Animal.ProductionTime)
                    {
                        finishedBuildings.Add(livestockBuilding);
                    }
                }
                else if (building.GetType() == typeof(StorageBuilding))
                {
                    StorageBuilding storageBuilding = building as StorageBuilding;
                    if (!storageBuilding.IsFull())
                    {
                        storageBuildings.Add(storageBuilding);
                    }
                }
            }

            if (finishedBuildings.Count == 0)
            {
                Console.WriteLine("No tienes ningun producto terminado.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Elige uno de los productos disponibles:");
                for (int i = 0; i < finishedBuildings.Count; i++)
                {
                    Console.WriteLine($"({i + 1}) - {finishedBuildings[i].Name}");
                }

                Console.ResetColor();

                int userOption = Int32.Parse(Console.ReadLine());

                if (userOption < 1 || userOption > finishedBuildings.Count)
                {
                    Console.WriteLine("Opción no valida...");
                    return;
                }

                Building finishedBuilding = finishedBuildings[userOption - 1];


                if (finishedBuilding.GetType() == typeof(Plantation))
                {
                    Plantation plantationBuilding = finishedBuilding as Plantation;
                    double terrainQuality = 0;
                    int fieldBlocks = 0;
                    foreach (Terrain terrain in map.Terrains)
                    {
                        if (terrain.Building == plantationBuilding)
                        {
                            foreach (Block block in terrain.Blocks)
                            {
                                if (block.GetType() == typeof(FieldBlock))
                                {
                                    FieldBlock fieldBlock = block as FieldBlock;
                                    fieldBlocks += 1;
                                    terrainQuality += fieldBlock.Quality;
                                }
                            }
                            break;
                        }
                    }

                    int seedPrice = market.GetPriceOfSeed(plantationBuilding.Seed);

                    terrainQuality = terrainQuality / fieldBlocks;
                    double terrainProportion = fieldBlocks / 100;

                    double price = plantationBuilding.Health * terrainQuality * terrainProportion;
                    int finalPrice = Convert.ToInt32(price) * seedPrice;

                    if (storageBuildings.Count == 0)
                    {
                        player.Money += finalPrice;
                        Console.WriteLine($"Plantación vendida a {finalPrice}");
                    }
                    else
                    {
                        Console.WriteLine("Elige uno de las bodegas disponibles:");
                        for (int i = 0; i < storageBuildings.Count; i++)
                        {
                            Console.WriteLine($"({i + 1}) - {storageBuildings[i].Name}");
                        }

                        int storageUserOption = Int32.Parse(Console.ReadLine());

                        if (storageUserOption < 1 || storageUserOption > storageBuildings.Count)
                        {
                            Console.WriteLine("Opción no valida...");
                            return;
                        }

                        Building storageBuilding = storageBuildings[userOption - 1];
                        int storageBuildingIndex = player.Buildings.IndexOf(storageBuilding);
                        StorageBuilding userStorageBuilding = player.Buildings[storageBuildingIndex] as StorageBuilding;
                        FinishedProduct finishedProduct = new FinishedProduct(plantationBuilding.Seed.Name, finalPrice, seedPrice, plantationBuilding.Seed.WaterConsume, plantationBuilding.Seed.MinimumWaterLevel, plantationBuilding.Seed.PenaltyForLackOfWater, plantationBuilding.Seed.ProductionTime, plantationBuilding.Seed.ProbabilityOfIllness, plantationBuilding.Seed.PenaltyForIllness, Convert.ToInt32(terrainQuality));

                        userStorageBuilding.AddFinishedProduct(finishedProduct);
                        Console.WriteLine($"Plantación guardada en bodega {userStorageBuilding.Name}");
                    }

                    int plantationBuildingIndex = player.Buildings.IndexOf(plantationBuilding);
                    player.Buildings.RemoveAt(plantationBuildingIndex);
                }
                else if (finishedBuilding.GetType() == typeof(Livestock))
                {
                    Livestock livestockBuilding = finishedBuilding as Livestock;

                    int fieldBlocks = 0;
                    double terrainQuality = 0;
                    foreach (Terrain terrain in map.Terrains)
                    {
                        if (terrain.Building == livestockBuilding)
                        {
                            foreach (Block block in terrain.Blocks)
                            {
                                if (block.GetType() == typeof(FieldBlock))
                                {
                                    FieldBlock fieldBlock = block as FieldBlock;
                                    fieldBlocks += 1;
                                    terrainQuality += fieldBlock.Quality;
                                }
                            }
                            break;
                        }
                    }

                    terrainQuality = terrainQuality / fieldBlocks;

                    int animalPrice = market.GetPriceOfAnimal(livestockBuilding.Animal);
                    double terrainProportion = fieldBlocks / 100;
                    double originalUnits = 10 * terrainProportion;

                    double price = (livestockBuilding.Health * livestockBuilding.Units) / originalUnits;
                    int finalPrice = Convert.ToInt32(price) * animalPrice;

                    if (storageBuildings.Count == 0)
                    {
                        player.Money += finalPrice;
                        Console.WriteLine($"Ganado vendido a {finalPrice}");
                    }
                    else
                    {
                        Console.WriteLine("Elige uno de las bodegas disponibles:");
                        for (int i = 0; i < storageBuildings.Count; i++)
                        {
                            Console.WriteLine($"({i + 1}) - {storageBuildings[i].Name}");
                        }

                        int storageUserOption = Int32.Parse(Console.ReadLine());

                        if (storageUserOption < 1 || storageUserOption > storageBuildings.Count)
                        {
                            Console.WriteLine("Opción no valida...");
                            return;
                        }

                        Building storageBuilding = storageBuildings[userOption - 1];
                        int storageBuildingIndex = player.Buildings.IndexOf(storageBuilding);
                        StorageBuilding userStorageBuilding = player.Buildings[storageBuildingIndex] as StorageBuilding;
                        FinishedProduct finishedProduct = new FinishedProduct(livestockBuilding.Animal.Name, finalPrice, animalPrice, livestockBuilding.Animal.WaterConsume, livestockBuilding.Animal.MinimumWaterLevel, livestockBuilding.Animal.PenaltyForLackOfWater, livestockBuilding.Animal.ProductionTime, livestockBuilding.Animal.ProbabilityOfIllness, livestockBuilding.Animal.PenaltyForIllness, Convert.ToInt32(terrainQuality));

                        userStorageBuilding.AddFinishedProduct(finishedProduct);
                        Console.WriteLine($"Ganado guardado en bodega {userStorageBuilding.Name}");
                    }

                    int liveStockBuildingIndex = player.Buildings.IndexOf(livestockBuilding);
                    player.Buildings.RemoveAt(liveStockBuildingIndex);
                }
                else
                {
                    return;
                }

            }
        }

        private void GiveFarmToPlayer()
        {

            Random r = new Random();
            int farmPosition;
            while (true)
            {
                farmPosition = r.Next(0, 88);
                if (farmPosition % 10 < 8) { break; }
            }

            if (map.Terrains[farmPosition].Blocks[0].Name != 'T')
            {
                GiveFarmToPlayer();
            }
            else
            {

                player.BuyTerrain(map.Terrains[farmPosition]);
                player.BuyTerrain(map.Terrains[farmPosition + 1]);
                player.BuyTerrain(map.Terrains[farmPosition + 2]);
                player.BuyTerrain(map.Terrains[farmPosition + 10]);
                player.BuyTerrain(map.Terrains[farmPosition + 11]);
                player.BuyTerrain(map.Terrains[farmPosition + 12]);
            }
        }

        private void NextTurn()
        {
            Console.WriteLine("Pasando de turno...");
            Console.ForegroundColor = ConsoleColor.Red;

            // Se actuliza el precio de las semillas en el mercado
            foreach (Seed seed in market.Seeds)
            {
                seed.AddPriceHistory();
            }

            // Se actulizan los datos de los edificios del usuario
            foreach (Building building in player.buildings)
            {
                if (building.GetType() == typeof(Plantation))
                {
                    // Se actulizan datos de las plantaciones
                    Plantation plantationBuilding = building as Plantation;
                    Seed plantationSeed = plantationBuilding.Seed;

                    plantationBuilding.Maturity += 1;
                    if (plantationBuilding.Sick == false) { plantationBuilding.SickPast = false; }
                    if (plantationBuilding.HasUndergrowth == false) { plantationBuilding.HasUndergrowthPast = false; }
                    if (plantationBuilding.HasWorms == false) { plantationBuilding.HasWormsPast = false; }
                    plantationBuilding.GetSick();
                    plantationBuilding.GetUndergrowth();
                    plantationBuilding.GetWorms();

                    // Se revisan las proiedades de la plantacion
                    if (plantationBuilding.Health == 0)
                    {
                        Console.WriteLine($"-La salud de la plantacion {plantationBuilding.Name} llego a 0.");
                    }

                    if (plantationBuilding.Water == 0)
                    {
                        Console.WriteLine($"-El nivel de agua de la plantacion {plantationBuilding.Name} llego a 0.");
                    }

                    if (plantationBuilding.Nutrients == 0)
                    {
                        Console.WriteLine($"-El nivel de nutrientes de la plantacion {plantationBuilding.Name} llego a 0.");
                    }

                    if (plantationBuilding.Nutrients < plantationSeed.MinimumLevelOfNutrients && plantationBuilding.Health != 0)
                    {
                        plantationBuilding.Health -= plantationSeed.PenaltyForLackOfNutrients;
                        Console.WriteLine($"-La salud de la plantacion {plantationBuilding.Name} disminuyo a {plantationBuilding.Health} debido a falta de nutrientes.");
                    }

                    if (plantationBuilding.Water < plantationSeed.MinimumWaterLevel && plantationBuilding.Health != 0)
                    {
                        plantationBuilding.Health -= plantationSeed.PenaltyForLackOfWater;
                        Console.WriteLine($"-La salud de la plantacion {plantationBuilding.Name} disminuyo a {plantationBuilding.Health} debido a falta de Agua.");
                    }

                    if (plantationBuilding.Water != 0)
                    {
                        plantationBuilding.Water -= plantationSeed.WaterConsume;
                    }

                    if (plantationBuilding.Nutrients != 0)
                    {
                        plantationBuilding.Nutrients -= plantationSeed.NutrientConsumption;
                    }

                    if (plantationBuilding.Sick && plantationBuilding.SickPast && plantationBuilding.Health != 0)
                    {
                        plantationBuilding.Health -= plantationSeed.PenaltyForIllness;
                        Console.WriteLine($"-La plantacion {plantationBuilding.Name} se encuentra enferma y disminuyo la Salud a {plantationBuilding.Health}.");
                    }
                    else if (plantationBuilding.Sick)
                    {
                        Console.WriteLine($"-La plantacion {plantationBuilding.Name} se encuentra enferma.");
                    }

                    if (plantationBuilding.HasWorms && plantationBuilding.HasWormsPast && plantationBuilding.Health != 0)
                    {
                        plantationBuilding.Health -= plantationSeed.WormPenalty;
                        Console.WriteLine($"-La plantacion {plantationBuilding.Name} se encuentra con gusanos y disminuyo la Salud a {plantationBuilding.Health}.");
                    }
                    else if (plantationBuilding.HasWorms)
                    {
                        Console.WriteLine($"-La plantacion {plantationBuilding.Name} se encuentra con gusanos.");
                    }

                    if (plantationBuilding.HasUndergrowth && plantationBuilding.HasUndergrowthPast && plantationBuilding.Health != 0)
                    {
                        plantationBuilding.Health -= plantationSeed.WeedPenalty;
                        Console.WriteLine($"-La plantacion {plantationBuilding.Name} se encuentra con maleza y disminuyo la Salud a {plantationBuilding.Health}.");
                    }
                    else if (plantationBuilding.HasUndergrowth)
                    {
                        Console.WriteLine($"-La plantacion {plantationBuilding.Name} se encuentra con maleza.");
                    }

                    plantationBuilding.HasUndergrowthPast = plantationBuilding.HasUndergrowth;
                    plantationBuilding.HasWormsPast = plantationBuilding.HasWorms;
                    plantationBuilding.SickPast = plantationBuilding.Sick;
                }
                else if (building.GetType() == typeof(Livestock))
                {
                    // Se actulizan datos de los ganados
                    Livestock livestockBuilding = building as Livestock;
                    Animal livestockAnimal = livestockBuilding.Animal;

                    livestockBuilding.Maturity += 1;

                    if (livestockBuilding.Health == 0)
                    {
                        Console.WriteLine($"-La salud del  ganado {livestockBuilding.Name} llego a 0.");
                    }
                    //Agua
                    if (livestockBuilding.Water == 0)
                    {
                        Console.WriteLine($"-El nivel de agua del ganado {livestockBuilding.Name} llego a 0.");
                    }
                    else if (livestockBuilding.Water < livestockAnimal.MinimumWaterLevel && livestockBuilding.Health != 0)
                    {
                        livestockBuilding.Health -= livestockAnimal.PenaltyForLackOfWater;
                        Console.WriteLine($"-La salud del ganado  {livestockBuilding.Name} disminuyo a {livestockBuilding.Health} debido a falta de Agua.");
                    }


                    //Comida
                    if (livestockBuilding.Food == 0)
                    {
                        Console.WriteLine($"-El nivel de alimentos del ganado {livestockBuilding.Name} llego a 0.");
                    }

                    if (livestockBuilding.Food < livestockAnimal.MinimumFoodLevel && livestockBuilding.Health != 0)
                    {
                        livestockBuilding.Health -= livestockAnimal.PenaltyForLackOfFood;
                        Console.WriteLine($"-La salud del ganado {livestockBuilding.Name} disminuyo a {livestockBuilding.Health} debido a falta de alimentos.");
                    }
                    if (livestockBuilding.Sick == false) { livestockBuilding.SickPast = false; }
                    livestockBuilding.Escape();
                    livestockBuilding.SuddenDeath();
                    livestockBuilding.GetSick();

                    if (livestockBuilding.Sick && livestockBuilding.SickPast && livestockBuilding.Health != 0)
                    {
                        livestockBuilding.Health -= livestockAnimal.PenaltyForIllness;
                        Console.WriteLine($"-El ganado {livestockBuilding.Name} se encuentra enfermo y disminuyo la Salud a {livestockBuilding.Health}.");
                    }
                    else if (livestockBuilding.Sick)
                    {
                        Console.WriteLine($"-El ganado {livestockBuilding.Name} se encuentra enfermo.");
                    }
                    livestockBuilding.SickPast = livestockBuilding.Sick;
                    //consumos
                    if (livestockBuilding.Food != 0)
                    {
                        livestockBuilding.Food -= livestockAnimal.FoodConsumption;
                    }
                    if (livestockBuilding.Water != 0)
                    {
                        livestockBuilding.Water -= livestockAnimal.WaterConsume;
                    }
                }
                else if (building.GetType() == typeof(StorageBuilding))
                {
                    //Se disminute la calidad de los productos almacenados
                    StorageBuilding storageBuilding = building as StorageBuilding;
                    storageBuilding.ChangeQuality();
                }
            }

            turn += 1;

            Console.ResetColor();
            Console.WriteLine("Turno pasado...");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }


        private void SaveGame()
        {
            try
            {
                Console.WriteLine("Guardando partida...");
                string path = AppDomain.CurrentDomain.BaseDirectory + "SavedGame.txt";
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, this);
                stream.Close();
                Console.WriteLine("Partida guardada...");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Hubo un error guardando el juego...");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        private void LoadGame()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "SavedGame.txt";
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                Game objnew = (Game)formatter.Deserialize(stream);

                this.player = objnew.player;
                this.map = objnew.map;
                this.market = objnew.market;
                this.turn = objnew.turn;
            }
            catch
            {
                Console.WriteLine("Hubo un error cargando el juego...");
            }
        }

        private void CloseGame()
        {
            Console.WriteLine("Perdera el progreso no guardado. ¿Desea continuar? (y/n)");
            string userOption = Console.ReadLine();

            if (userOption != "y" && userOption != "n")
            {
                Console.WriteLine("Opción no valida");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                if (userOption == "y")
                {
                    playing = false;
                }
            }
        }
    }
}
