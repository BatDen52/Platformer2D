using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MoveTutorial : MonoBehaviour
{
    [SerializeField] private bool _isMobile;

    private void Awake()
    {
        if (Application.isEditor && _isMobile || YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
            gameObject.SetActive(false);
    }
}
