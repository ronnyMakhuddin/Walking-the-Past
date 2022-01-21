using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VibrationTypes
{
    public static void OnTapVibrate(bool success)
    {
        Success();
    }

    public static void OnSwipeVibrate(bool success)
    {

    }

    public static void OnDestructionVibrate()
    {

    }

    private static void Success()
    {
        CustomVibrate.Vibrate(250);
    }

    private static void Failure()
    {
        long[] pattern = { 100, 100 };
        CustomVibrate.VibratePattern(pattern);
    }
}
