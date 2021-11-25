using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityUtils;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }

    public SaveMode Mode = SaveMode.PlayerPrefs;
    public long Score { get; private set; } = 0;
    public long HighScore { get; private set; }
    public Vector2 StartPoint;
    public Transform Player;
    public TMPro.TextMeshProUGUI ScoreText;

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
        //TODO: change method of setting score to something else or keep
        Score = Mathf.FloorToInt(Vector2.Distance(StartPoint, Player.position));
        ScoreText.text = Score.ToString();
        if (Score > HighScore) HighScore = Score;
    }

    /// <summary>
    /// Save High Score to the FileSystem
    /// </summary>
    private void SaveScoreIO()
    {
        //Check if file exists, if not create it
        if (!File.Exists(Application.persistentDataPath + "\\saveData.bin")) File.WriteAllText(Application.persistentDataPath + "\\saveData.bin", string.Empty);
        
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

    /// <summary>
    /// Load High Score from the FileSystem
    /// </summary>
    private void LoadScoreIO()
    {
        if (!File.Exists(Application.persistentDataPath + "\\saveData.bin")) return;

        //Open a stream to the file
        FileStream fs = new FileStream(Application.persistentDataPath + "\\saveData.bin", FileMode.Open);
        //Open a Binary Reader to read data
        BinaryReader br = new BinaryReader(fs);

        //Read data
        HighScore = br.ReadInt64();

        //Close to avoid memory leaks
        br.Close();
        fs.Close();
    }

    /// <summary>
    /// Save the score using the current SaveMode
    /// </summary>
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

    /// <summary>
    /// Deletes HighScores regardless of SaveMode
    /// </summary>
    /// <returns>Result of deletion IO</returns>
    public bool DeleteScore()
    {
        PlayerPrefsExt.DeleteLong("game.scores.HighScore");
        if (!File.Exists(Application.persistentDataPath + "\\saveData.bin"))
        {
            return true;
        }
        try
        {
            File.Delete(Application.persistentDataPath + "\\saveData.bin");
            if (File.Exists(Application.persistentDataPath + "\\saveData.bin"))
            {
                return false;
            }

        } catch
        {
            return false;
        }
        return true;
    }    
    
    /// <summary>
    /// Changes the score
    /// </summary>
    /// <param name="amount">Amount to influence score</param>
    /// <param name="adju">Wheter the Adjustment should add remove or set the score</param>
    public void AdjustScore(int amount, AdjustMode adju)
    {
        switch (adju)
        {
            case AdjustMode.Addictive:
                Score += amount;
                break;
            case AdjustMode.Absolute:
                Score = amount;
                break;
            case AdjustMode.Reduce:
                Score -= amount;
                break;
            default:
                break;
        }
    }
}
public enum SaveMode
{
    IO = 0x0,
    PlayerPrefs = 0x1
}
public enum AdjustMode
{
    Addictive,
    Absolute,
    Reduce
}