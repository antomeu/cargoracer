﻿public static class Globals
{
    //Variables
    public static int Lives;
    public static float Speed;
    public static bool[] PackageSlotIsUsed = new bool[3];

    public static bool EndAnimationIsFinished = false;

    public static GameState GameState;
    public static float Distance;
    public static int PackagesDelivered;

    public static string PlayerName;

    public static int Level = 1;

    public static int MusicIndex = 0;

    //Constants
    public static int MaximumLives = 4;
    public static float NominalSpeed = 40f;
    public static float BoostAddition = 40f;
    public static float PenaltySpeed = 25f;
    public static float ClippingDistance = 400f;

    public static int TotalLevels = 4;
    public static int[] TrafficSpeed = new int[4] {6,6,6,6 };
    public static int[] SpeedIncrease = new int[4] { 0, 10, 25, 35 };

    public static int LevelTreshold = 10;
    public static int StartingLevel;
    
    public static void Reset()
    {
        
        Lives = MaximumLives;
        for (int i = 0; i <= PackageSlotIsUsed.Length - 1; i++)
        { PackageSlotIsUsed[i] = false; }

        GameState = GameState.Start;
        Distance = 0f;
        PackagesDelivered = 0;

        Level = 1;
    }
}

