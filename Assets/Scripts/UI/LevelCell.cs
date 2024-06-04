using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Image _lockIcon;
    [SerializeField] private TMP_Text _numberText;

    private string _sceneName;

    public event Action<string> SceneSelected;

    public void Initialize(string sceneName, int number, bool isUnlocked)
    {
        _sceneName = sceneName;

        _openButton.onClick.AddListener(Select);
        _numberText.text = number.ToString();

        _openButton.interactable = isUnlocked;
        _numberText.gameObject.SetActive(isUnlocked);
        _lockIcon.gameObject.SetActive(isUnlocked == false);
    }

    private void Select()
    {
        _openButton.onClick.RemoveListener(Select);
        SceneSelected?.Invoke(_sceneName);
    }
}
