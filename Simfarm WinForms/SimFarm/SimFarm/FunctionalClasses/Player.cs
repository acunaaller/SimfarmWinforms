using System;
using System.Collections.Generic;
using SimFarm.Buildings;
using SimFarm.Consumables;

namespace SimFarm
{
    [Serializable]
    public class Player
    {
        private string name;
        private List<Terrain> terrains = new List<Terrain>();
        public List<Building> buildings = new List<Building>();
        public List<Consumable> consumables = new List<Consumable>();
        private int money;

        public Player(string name, int money)
        {
            this.name = name;
            this.money = money;
        }

        public string Name { get => name; }
        public int Money { get => money; set => money = value; }
        public List<Terrain> Terrains { get => terrains; }
        public List<Building> Buildings { get => buildings; }
        public List<Consumable> Consumables { get => Consumables; }

        public void BuyTerrain(Terrain terrain)
        {
            terrains.Add(terrain);
            terrain.Buy();
        }

        public void ChangeMoney(int changeMoney)
        {
            money = money + changeMoney;
        }

        public void AddBuilding(Building building)
        {
            buildings.Add(building);
        }

        public void AddConsumable(Consumable consumable)
        {
            consumables.Add(consumable);
        }

        public List<Consumable> GetAvailableConsumables(List<string> consumableTypes)
        {
            List<Consumable> availableConsumables = new List<Consumable>();
            foreach (string consumableType in consumableTypes)
            {
                availableConsumables.AddRange(getConsumamblesOfType(consumableType));

            }
            return availableConsumables;
        }

        private List<Consumable> getConsumamblesOfType(string consumableType)
        {
            List<Consumable> availableConsumables = new List<Consumable>();
            foreach (Consumable consumable in consumables)
            {
                if (consumable.GetType().Name == consumableType)
                {
                    availableConsumables.Add(consumable);
                }
            }

            return availableConsumables;
        }
    }
}
