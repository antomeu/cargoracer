public static class Globals
{
    //Variables
    public static int Lives;
    public static float Speed;
    public static bool[] PackageSlotIsUsed = new bool[3];

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

    public static int TotalLevels = 3;
    public static int[] TrafficSpeed = new int[3] {6,6,6 };
    public static int[] SpeedIncrease = new int[3] { 0, 10, 25 };

    public static int LevelTreshold = 10;
    
    
    public static void Reset()
    {
        
        Lives = MaximumLives;
        for (int i = 0; i <= PackageSlotIsUsed.Length - 1; i++)
        { PackageSlotIsUsed[i] = false; }

        GameState = GameState.Playing;
        Distance = 0;
        PackagesDelivered = 0;

        Level = 1;
    }
}

