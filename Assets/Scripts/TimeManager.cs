using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    public static bool IsPaused { get; private set; }

    public static void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }

    public static void Run()
    {
        Time.timeScale = 1;
        IsPaused = false;
    }
}
