using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace Core
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [SerializeField] private string fileName = "inventory.json";

        public readonly Dictionary<Item, int> resources = new();
        public readonly Dictionary<ShipData, int> ships = new();
        public readonly Dictionary<WeaponData, int> weapons = new();

        void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this);
                Load();
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        public void ChangeQuantity(Item item, int quantity = 1)
        {
            ChangeQuantityGeneric(resources, item, quantity);
        }
        public void ChangeQuantity(ShipData item, int quantity = 1)
        {
            ChangeQuantityGeneric(ships, item, quantity);

        }
        public void ChangeQuantity(WeaponData item, int quantity = 1)
        {
            ChangeQuantityGeneric(weapons, item, quantity);
        }
        private void ChangeQuantityGeneric<T>(Dictionary<T, int> dict, T item, int quantity) where T : InventoryItem
        {
            Debug.Log($"changing quantity for {item.name}");
            if (dict.ContainsKey(item))
            {
                int count = dict[item] + quantity;
                dict[item] = Math.Max(0, count);
            }
            else
            {
                dict.Add(item, quantity);
            }
        }
        public bool HasEnough(Item item, int required)
        {
            return HasEnoughGeneric(resources, item, required);
        }
        public bool HasEnough(ShipData item, int required)
        {
            return HasEnoughGeneric(ships, item, required);
        }
        public bool HasEnough(WeaponData item, int required)
        {
            return HasEnoughGeneric(weapons, item, required);
        }
        private bool HasEnoughGeneric<T>(Dictionary<T, int> dict, T item, int rquired)
        {
            return dict.TryGetValue(item, out int count) && count >= rquired;
        }

        public void Save()
        {
            InventorySaveData saveData = new()
            {
                resources = resources.Select(el => new InventorySlot { Id = el.Key.Id, Quantity = el.Value }).ToList(),
                ships = ships.Select(el => new InventorySlot { Id = el.Key.Id, Quantity = el.Value }).ToList(),
                weapons = weapons.Select(el => new InventorySlot { Id = el.Key.Id, Quantity = el.Value }).ToList()
            };

            string saveDataJson = JsonUtility.ToJson(saveData);
            File.WriteAllTextAsync(Path.Combine(Application.persistentDataPath, fileName), saveDataJson);
        }

        private void Load()
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            if (!File.Exists(path)) return;

            string jsonData = File.ReadAllText(path);
            InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(jsonData);

            resources.Clear();
            ships.Clear();
            weapons.Clear();

            saveData.resources.ForEach(el =>
            {
                Item item = ItemDatabase.Get(el.Id) as Item;
                if (item != null)
                {
                    Debug.Log($"Adding item {item.name} with quantity: {el.Quantity}");
                    resources.Add(item, el.Quantity);
                }
                else
                {
                    Debug.Log($"Item id {el.Id} missing.");
                }
            });
            saveData.ships.ForEach(el =>
            {
                ShipData item = ItemDatabase.Get(el.Id) as ShipData;
                if (item != null)
                {

                    ships.Add(item, el.Quantity);
                }
                else
                {
                    Debug.Log($"Item id {el.Id} missing.");
                }
            });
            saveData.weapons.ForEach(el =>
            {
                WeaponData item = ItemDatabase.Get(el.Id) as WeaponData;
                if (item != null)
                {

                    weapons.Add(item, el.Quantity);
                }
                else
                {
                    Debug.Log($"Item id {el.Id} missing.");
                }
            });

        }
    }

    [System.Serializable]
    class InventorySlot
    {
        public string Id;
        public int Quantity;
    }

    [System.Serializable]
    class InventorySaveData
    {
        public List<InventorySlot> resources;
        public List<InventorySlot> ships;
        public List<InventorySlot> weapons;
    }
}
