using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Library")]
public class PrefabLibrary : ScriptableObject
{
    public List<Entry> library = new List<Entry>();
    public List<Combination> combinations = new List<Combination>();

    public GameObject GetPrefab(ItemName name)
    {
        return library.First(item => item.name == name).prefab;
    }

    public Sprite GetSprite(ItemName name)
    {
        return library.First(item => item.name == name).icon;
    }

    public List<ItemName> GetCombinationsContaining(ItemName name)
    {
        List<ItemName> results = new List<ItemName>();

        foreach (var item in combinations)
        {
            if(item.inputNames.Contains(name))
            {
                foreach (var x in item.inputNames)
                {
                    if(x != name && !results.Contains(name))
                    {
                        results.Add(x);
                    }
                }
            }
        }

        return results;
    }

    public ItemName FindCombinationResult(List<ItemName> items)
    {
        for (int i = 0; i < combinations.Count; i++)
        {
            bool isMatch = items.All(combinations[i].inputNames.Contains) && items.Count == combinations[i].inputNames.Count;
            if (isMatch)
            {
                return combinations[i].resultName;
            }
        }

        return ItemName.Unset;
        
    }
}
[System.Serializable]
public class Entry
{
    public ItemName name;
    public GameObject prefab;
    public Sprite icon;
}

[System.Serializable]
public class Combination
{
    public List<ItemName> inputNames = new List<ItemName>();
    public ItemName resultName;
}
