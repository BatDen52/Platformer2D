using UnityEngine;

public class Key : MonoBehaviour, IItem
{
    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;

    public void Collect()
    {
        Destroy(gameObject);
    }
}
