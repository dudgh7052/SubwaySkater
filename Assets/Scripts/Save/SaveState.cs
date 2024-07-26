using System;

[System.Serializable]
public class SaveState
{
    public int Highscore { get; set; }
    public int Fish { get; set; }
    public DateTime LastSaveTime { get; set; }

    public SaveState()
    {
        Highscore = 0;
        Fish = 0;
        LastSaveTime = DateTime.Now;
    }
}