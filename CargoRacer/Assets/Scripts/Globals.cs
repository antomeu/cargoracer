﻿public static class Globals
{
    //Variables
    public static int Lives;
    public static float Speed;
    public static bool[] PackageSlotIsUsed = new bool[3];

    public static GameState GameState;
    public static float Distance;
    public static int PackagesDelivered;


    //Constants
    public static int MaximumLives = 3;
    public static float NominalSpeed = 40f;
    public static float ClippingDistance = 400f;
    
    
    public static void Reset()
    {
        Lives = MaximumLives;
        for (int i = 0; i <= PackageSlotIsUsed.Length - 1; i++)
        { PackageSlotIsUsed[i] = false; }

        GameState = GameState.Playing;
        Distance = 0;
        PackagesDelivered = 0;
        
        
    }
}
