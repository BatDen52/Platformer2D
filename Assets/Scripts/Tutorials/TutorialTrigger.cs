using System;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public event Action PlayerFounded;
    public event Action PlayerLosted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            PlayerFounded?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            PlayerLosted?.Invoke();
    }
}
