using System.Threading.Tasks;

using UnityEngine;
public enum ObjectColor
{
    gold,
    red,
    green,
    blue
}

public class Key : MonoBehaviour, IPickable
{
    [SerializeField] private float _rotationSpeed = 50f;
    [SerializeField] private GameObject _hint;

    [field: SerializeField] public ObjectColor Color { get; private set; }

    private void Start()
    {
        QuestHandler.instance.RegisterNewKey();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
    public void PickObject(Transform parent)
    {
        GlobalSoundHandler.instance.OnPickUpItem();
        QuestHandler.instance.OnKeyCollect();
        transform.SetParent(parent);
        _hint.SetActive(false);
        gameObject.SetActive(false);

    }
    public void GetObject()
    {
        gameObject.SetActive(true);
        GlobalSoundHandler.instance.OnPutDownItem();
    }
}
