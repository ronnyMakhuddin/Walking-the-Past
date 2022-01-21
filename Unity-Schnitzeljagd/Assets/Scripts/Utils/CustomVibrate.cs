using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomVibrate
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityplayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityplayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate(long millisecond = 100)
    {
        if (android())
        {
            vibrator.Call("vibrate", millisecond);
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    public static void VibratePattern(long[] pattern)
    {
        if (android())
        {
            vibrator.Call("vibrate", pattern);
        }
        else
        {
            Handheld.Vibrate();
        }
    }


    public static void Cancel()
    {
        if (android())
        {
            vibrator.Call("cancel");
        }
    }

    public static bool android()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif       
    }
}
