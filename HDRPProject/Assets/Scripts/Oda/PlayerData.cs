using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    int Fragment;
    bool InBox;
    int PlayerWorld;
    bool FlashlightActive;
    float Time;
    int UnlockStage;

    public PlayerData()
    {
        Fragment1 = 0;
        InBox1 = false;
        PlayerWorld1 = 0;
        FlashlightActive1 = false;
        Time1 = 0;
        UnlockStage1 = 0;
    }

    public PlayerData(int fragment, bool inBox, int playerWorld, bool flashlightActive, float time, int unlockStage)
    {
        Fragment1 = fragment;
        InBox1 = inBox;
        PlayerWorld1 = playerWorld;
        FlashlightActive1 = flashlightActive;
        Time1 = time;
        UnlockStage1 = unlockStage;
    }

    public int Fragment1 { get => Fragment; set => Fragment = value; }
    public bool InBox1 { get => InBox; set => InBox = value; }
    public int PlayerWorld1 { get => PlayerWorld; set => PlayerWorld = value; }
    public bool FlashlightActive1 { get => FlashlightActive; set => FlashlightActive = value; }
    public float Time1 { get => Time; set => Time = value; }
    public int UnlockStage1 { get => UnlockStage; set => UnlockStage = value; }
}
