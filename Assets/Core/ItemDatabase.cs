using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ItemDatabase
{
    static Dictionary<string, InventoryItem> map;

    public static void BuildIndex()
    {
        // if items live in a Resources folder:
        var loaded = Resources.LoadAll<InventoryItem>("");
        Debug.Log($"loaded {loaded.Length}");
        map = loaded.ToDictionary(el => el.Id, el => el);
    }

    public static InventoryItem Get(string id) {
        Debug.Log($"accesing item with id {id}");
        return map.TryGetValue(id, out var so) ? so : null;
    }
}
