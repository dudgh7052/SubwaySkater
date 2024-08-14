using System;

[System.Serializable]
public class SaveState
{
    [NonSerialized] const int HAT_COUNT = 12;

    public int Highscore { get; set; }
    public int Fish { get; set; }
    public DateTime LastSaveTime { get; set; }
    public int CurrentHatIndex { get; set; }

    // 모자 해금 확인 배열
    public byte[] UnlockedHatFlag { get; set; } // 01010101

    public SaveState()
    {
        Highscore = 0;
        Fish = 0;
        LastSaveTime = DateTime.Now;
        CurrentHatIndex = 0;
        UnlockedHatFlag = new byte[HAT_COUNT];
        UnlockedHatFlag[0] = 1;
    }
}