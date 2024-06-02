using UnityEngine;

public class MedKit : MonoBehaviour, IItem
{
    [SerializeField] private int _value;
    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;
    public int Value => _value;

    public void Collect()
    {
        Destroy(gameObject);
    }
}
