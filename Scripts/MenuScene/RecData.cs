using UnityEngine;

public static class RecData
{
    private static readonly string stateSoundKey = "keyForSound"; //key for save
    public static readonly int[] plans = new int[50] { 
        500,  600,  700,  800,  900,  1000, 1100, 1200, 1300, 1400, 
        1500, 1600, 1700, 1800, 1900, 2000, 2100, 2200, 2300, 2400, 
        2500, 2600, 2700, 2800, 2900, 2950, 3000, 3050, 3100, 3150, 
        3200, 3250, 3300, 3350, 3400, 3450, 3500, 3550, 3600, 3650, 
        3700, 3750, 3800, 3850, 3900, 3950, 4000, 4050, 4100, 4200 };
    public static readonly int canCookBurger = 10;
    public static readonly int canCookCola = 40;
    public static readonly int canCookDrink = 2;
    public static readonly int canCookFree = 5; //and 10-14
    public static readonly int canCookSauseG = 20;
    public static readonly int canCookOnion = 30;
    public static bool youCompletedTheGame = false;
    public static readonly int Code_Onion = 4;
    public static readonly int Code_Doneness = 8;
    public static readonly int Code_Burger = 16;
    public static readonly int Code_HotDog = 32;
    public static void CangeAndSaveStateOfSound(ref string sound)
    {
        if (sound == "false") { sound = "true"; }
        else { sound = "false"; }
        PlayerPrefs.SetString(stateSoundKey, sound);
        PlayerPrefs.Save();
    }
    public static void LoadStateOfSound(ref string sound)
    {
        if (PlayerPrefs.HasKey(stateSoundKey)) { sound = PlayerPrefs.GetString(stateSoundKey); }
        else { sound = "true"; }
    }
}
