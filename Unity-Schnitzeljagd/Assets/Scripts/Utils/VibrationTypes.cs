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
        Success();

    }

    public static void OnMaxburgCracksVibrate()
    {
        RepeatingTicks();
    }

    public static void OnDestructionVibrate()
    {

    }

    private static void Success()
    {
        CustomVibrate.Vibrate(100);
    }

    private static void Failure()
    {
        long[] pattern = { 100, 100 };
        CustomVibrate.VibratePattern(pattern);
    }

    private static void RepeatingTicks()
    {
        long[] pattern = { 1000, 500, 250, 100, 50 };
        CustomVibrate.VibratePattern(pattern);
    }
}
