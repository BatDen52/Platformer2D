using System.Linq;
using UnityEngine;

public class Finish : MonoBehaviour, IInteractable
{
    [SerializeField] public Switch[] _switches;

    public void Interact()
    {
        if (_switches.All(i => i.IsActive))
        {
            gameObject.SetActive(false);
        }
    }
}
