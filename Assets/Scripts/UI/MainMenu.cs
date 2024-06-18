using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private const int DEFAULT_LEVEL_INDEX = 1;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _selectLevelButton;

    [SerializeField] private SettingstWindow _settingstWindow;
    [SerializeField] private SelectLevelWindow _selectLevelWindow;

    [SerializeField] private float _waitingInAlphaTime = 0.5f;
    [SerializeField] private float _showingTime;
    [SerializeField] private CanvasGroup _menuPanel;

    private void Awake()
    {
        ShowMenu();
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(LoadScene);
        _settingsButton.onClick.AddListener(_settingstWindow.Open);
        _selectLevelButton.onClick.AddListener(_selectLevelWindow.Open);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadScene);
        _settingsButton.onClick.RemoveListener(_settingstWindow.Open);
        _selectLevelButton.onClick.RemoveListener(_selectLevelWindow.Open);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(DEFAULT_LEVEL_INDEX);
    }

    private void ShowMenu()
    {
        try
        {
            StartCoroutine(Showing());
        }
        catch (Exception) { }
    }

    private IEnumerator Showing()
    {
        yield return LocalizationSettings.InitializationOperation;
        yield return new WaitForSeconds(_waitingInAlphaTime);

        float time = 0;
        float startAlpha = 0;
        float finishAlpha = 1;

        while (time < _showingTime)
        {
            time += Time.deltaTime;

            _menuPanel.alpha = Mathf.Lerp(startAlpha, finishAlpha, time / _showingTime);
            yield return null;
        }

        _menuPanel.alpha = finishAlpha;
    }
}
