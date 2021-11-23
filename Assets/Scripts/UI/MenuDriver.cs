using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuDriver : MonoBehaviour
{
    [SerializeField]
    private int menuScenes;
    [SerializeField]
    private int creditsLoc;
    public GameObject BeerImage;
    public GameObject JulianImage;

    public void PlayButton()
    {
        PlayerPrefs.SetInt("hasPlayed", 1);
        //Global.playAmount += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + menuScenes);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ToCredits()
    {
        SceneManager.LoadScene(1);
    }
    public void ToMain()
    {
        //if (GameManager.Instance != null) GameManager.Instance.HandlePause();
        SceneManager.LoadScene(0);
    }

    public void ToStats()
    {
        SceneManager.LoadScene(2);
    }

    public void ToSettings()
    {
        SceneManager.LoadScene(3);
    }

    public void PopImage(GameObject popup)
    {
        popup.SetActive(true);
    }

    public void PauseGame()
    {
        //if (GameManager.Instance.IsPaused) UIManager.Instance.isHovering = false;
        //GameManager.Instance.HandlePause();
        //UIManager.Instance.ShowPause();
    }
}
