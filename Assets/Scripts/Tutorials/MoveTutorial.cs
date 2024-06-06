using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorial : MonoBehaviour
{
    [SerializeField] private bool _isMobile;

    private void Awake()
    {
        if(_isMobile)
            gameObject.SetActive(false);
    }
}
