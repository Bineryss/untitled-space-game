using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{

    public List<Item> SelectedResources = new();

    public static CraftingManager Instance { get; private set; }

    [SerializeField] private int maxLenght;
    [SerializeField] private List<CraftingRecipe> recieps;
    [SerializeField] private InventoryItem scrap;

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
    }

    public bool AddResource(Item resource)
    {
        if (SelectedResources.Count + 1 > maxLenght)
        {
            return false;
        }

        SelectedResources.Add(resource);

        return true;
    }

    public bool RemoveResource(Item resource)
    {
        if (!SelectedResources.Contains(resource))
        {
            return false;
        }

        SelectedResources.Remove(resource);
        return true;
    }

    public InventoryItem Craft()
    {
        //TODO item specific recipie check

        List<Characteristic> characteristics = SelectedResources.Select(el => el.CraftItem).ToList();

        if (characteristics.Where(c => c != Characteristic.NOTHING).Distinct().ToList().Count > 1)
        {
            //todo here check for combine
            return scrap;
        }

        CraftingRecipe match = recieps
        .FirstOrDefault(reciep => reciep.Required
            .OrderBy(el => el)
            .SequenceEqual(characteristics.OrderBy(el => el))
            );

        if (match == null)
        {
            return scrap;
        }
        return match.Result;
        //pseudo code
        /*
        1. check if item specific combination can be found
        2. check if two items have characteristics and no "combine" characteristic is present
        except if one items rarity is higerh than the others! the the higher rarity wins
            => true "failure"
        3. check characteristic and ammount of nothing items against reciepe book

        */
    }
}

[System.Serializable]
public class SpecificReciep
{
    public List<Item> Required;

    public string Result;
}
