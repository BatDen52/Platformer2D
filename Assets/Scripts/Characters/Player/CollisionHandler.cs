using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> InteractableFounded;
    public event Action<MedKit> MedKitFounded;
    public event Action<Key> KeyFounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable finish))
        {
            InteractableFounded?.Invoke(finish);
        }

        if (collision.TryGetComponent(out MedKit medKit))
            MedKitFounded?.Invoke(medKit);

        if (collision.TryGetComponent(out Key key))
            KeyFounded?.Invoke(key);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable _))
        {
            InteractableFounded?.Invoke(null);
        }
    }
}
