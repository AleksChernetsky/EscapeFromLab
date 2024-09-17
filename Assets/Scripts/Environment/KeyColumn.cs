using UnityEngine;

public class KeyColumn : MonoBehaviour, IPutable
{
    [field: SerializeField] public ObjectColor Color { get; private set; }
    [SerializeField] private Transform _keyHolder;

    public void PutItem(Inventory inventory)
    {
        foreach (Key key in inventory.Keys)
        {
            if (key.Color == Color)
            {
                key.transform.SetParent(_keyHolder);
                key.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                inventory.GetItem(key);
                QuestHandler.instance.OnColumnReceiveKey();
                break;
            }
        }
    }
}
