using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityUtils;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }

    public SaveMode Mode = SaveMode.PlayerPrefs;
    public long Score = 0;
    public long HighScore { get; set; }
    public Vector2 StartPoint;
    public Transform Player;


    private void Awake()
    { 
        if (Instance != null)
        {
            enabled = false;
            return;
        }
        Instance = this;

        switch (Mode)
        {
            case SaveMode.IO:
                Debug.LogWarning("File system loading not implemented");
                LoadScoreIO();
                break;
            case SaveMode.PlayerPrefs:
                HighScore = PlayerPrefsExt.GetLong("game.scores.HighScore", 0);
                break;
            default:
                break;
        }
    }


    private void Update()
    {
        //TODO: change method of setting score
        Score = Mathf.FloorToInt(Vector2.Distance(StartPoint, Player.position));
        if (Score > HighScore) HighScore = Score;
    }

    public void SaveScore()
    {
        switch (Mode)
        {
            case SaveMode.IO:
                SaveScoreIO();
                break;
            case SaveMode.PlayerPrefs:
                PlayerPrefsExt.SetLong("game.scores.HighScore", HighScore);
                break;
            default:
                break;
        }
    }

    private void SaveScoreIO()
    {
        //Check if file exists, if not create it
        if (File.Exists(Application.persistentDataPath + "\\saveData.bin")) File.WriteAllText(Application.persistentDataPath + "\\saveData.bin", string.Empty);
        
        //Open a stream to the file
        FileStream fs = new FileStream(Application.persistentDataPath + "\\saveData.bin", FileMode.Create);
        //Open a Binary Writer to write data
        BinaryWriter bw = new BinaryWriter(fs);

        //Write any data to be saved
        bw.Write(HighScore);

        //Close to avoid memory leaks
        bw.Close();
        fs.Close();
        //C.free(ref bw);
        //C.free(ref fs);
        //Debug: open folder containing file
        //System.Diagnostics.Process.Start(Application.persistentDataPath);
    }

    private void LoadScoreIO()
    {
        throw new NotImplementedException();
    }
}

public enum SaveMode
{
    IO = 0x0,
    PlayerPrefs = 0x1
}