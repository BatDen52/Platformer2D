using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    protected const int NO_KEY_COUNT = 0;
    protected const int NEEDED_KEY_COUNT = 1;

    [SerializeField] private bool _isLock;
    [SerializeField] private Lock _lock;
    [SerializeField] private MessageBox _messageBox;

    public bool IsLock => _isLock;
    public Key Key => _lock.Key;

    protected virtual void Awake()
    {
        if (_lock == null || _lock.Key == null)
            _isLock = false;

        _lock.gameObject.SetActive(_isLock);
    }

    public virtual void Interact()
    {
        if (IsLock)
            ShowMessage(NO_KEY_COUNT, NEEDED_KEY_COUNT, Key.Icon);
    }

    public void Unlock(Key key)
    {
        if (key == Key)
        {
            _isLock = false;
            _lock.gameObject.SetActive(false);
        }
    }

    protected void ShowMessage(int count, int neededCount, Sprite sprite)
    {
        _messageBox.Show(count, neededCount, sprite);
    }
}