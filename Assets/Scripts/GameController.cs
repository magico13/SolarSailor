using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int Loops = 0;

    // seconds until loop restarts automatically
    public int MaxTime = 60;
    public float Countdown;

    void Start()
    {
        Instance = this;
        Countdown = MaxTime;
        Debug.Log($"Loops: {Loops}");

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        Countdown -= Time.deltaTime;

        if (Countdown < 0)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Loops++;
        SceneManager.LoadScene(0);
    }
}
