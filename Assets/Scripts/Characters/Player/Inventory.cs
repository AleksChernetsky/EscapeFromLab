using System.Collections.Generic;

using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Key> Keys = new List<Key>();

    public void AddItem(object item)
    {
        if (item is IPickable pickable)
        {
            pickable.PickObject(transform);
            SortInventory(pickable, true);
        }
    }
    public void GetItem(object item)
    {
        if (item is IPickable pickable)
        {
            pickable.GetObject();
            SortInventory(pickable, false);
        }
    }

    private void SortInventory(object item, bool isIncomingItem)
    {
        if (item is Key key)
        {
            if (isIncomingItem)
            {
                Keys.Add(key);
            }
            else
            {
                Keys.Remove(key);
            }
        }
    }
}
