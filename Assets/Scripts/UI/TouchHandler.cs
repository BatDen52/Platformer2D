using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IPointerDownHandler
{
    public event Action Down; 

    public void OnPointerDown(PointerEventData eventData)
    {
        Down?.Invoke();
    }
}
