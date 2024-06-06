using System;
using System.Collections.Generic;
using UnityEngine;

public static class SaveService
{
    private const string SAVE_TITLE = "Save";

    private static SaveData _saveData;
    private static List<string> _sceneNames = new();

    public static bool MusicIsOn => _saveData.MusicIsOn;
    public static bool SoundIsOn => _saveData.SoundIsOn;
    public static float MusicVolume => _saveData.MusicVolume;
    public static float SoundVolume => _saveData.SoundVolume;
    public static List<string> UnlockedLevels => _saveData.UnlockedLevels;

    public static void Initialize(List<string> sceneNames)
    {
        _sceneNames = sceneNames;
        _saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVE_TITLE)) ?? new SaveData();
    }

    public static void Save()
    {
        PlayerPrefs.SetString(SAVE_TITLE, JsonUtility.ToJson(_saveData));
    }

    public static void SetMusicIsOn(bool isOn) => _saveData.MusicIsOn = isOn;

    public static void SetSoundIsOn(bool isOn) => _saveData.SoundIsOn = isOn;

    public static void SetMusicVolume(float value) => _saveData.MusicVolume = value;

    public static void SetSoundVolume(float value) => _saveData.SoundVolume = value;

    public static void UnlockNextLevel(string curentSceneName)
    {
        int sceneIndex = _sceneNames.FindIndex(i => i == curentSceneName);

        if (sceneIndex == _sceneNames.Count - 1)
            return;

        string sceneName = _sceneNames[sceneIndex + 1];

        if (_saveData.UnlockedLevels.Contains(sceneName) == false)
            _saveData.UnlockedLevels.Add(sceneName);

        Save();
    }

    public static bool IsUnlockedLevel(string sceneName) => _saveData.UnlockedLevels.Contains(sceneName);

    [Serializable]
    private class SaveData
    {
        public bool MusicIsOn = true;
        public bool SoundIsOn = true;
        public float MusicVolume = ConstantsData.SavaData.DEFAULT_VOLUME;
        public float SoundVolume = ConstantsData.SavaData.DEFAULT_VOLUME;
        public List<string> UnlockedLevels = new() { "Level1" };
    }
}