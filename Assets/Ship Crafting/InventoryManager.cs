using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager2 : MonoBehaviour
{
    [SerializeField] private List<ResourceQuantity> inventory;
    [SerializeField] public Dictionary<Item, ResourceQuantity> Inventory;

    public static InventoryManager2 Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        Inventory = inventory
        .GroupBy(rq => rq.Ressorce)
        .ToDictionary(i => i.Key, i => new ResourceQuantity { Ressorce = i.Key, Quantity = i.First().Quantity });
    }

    public void ReduceQuantity(Item item, int quantity)
    {
        if (Inventory.TryGetValue(item, out ResourceQuantity value))
        {
            int currentQuantity = value.Quantity;
            ResourceQuantity newValue = new() { Ressorce = value.Ressorce, Quantity = Math.Max(currentQuantity - quantity, 0) };
            Inventory[item] = newValue;
        }
    }

    public void IncreaseQuantity(Item item, int quantity)
    {
        if (Inventory.TryGetValue(item, out ResourceQuantity value))
        {
            int currentQuantity = value.Quantity;
            ResourceQuantity newValue = new ResourceQuantity { Ressorce = value.Ressorce, Quantity = currentQuantity + quantity };
            Inventory[item] = newValue;
        }
    }
}
