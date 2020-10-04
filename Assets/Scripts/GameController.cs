using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int Loops = 0;

    // seconds until loop restarts automatically
    public int MaxTime = 60;
    public float Countdown;
    public TextMeshProUGUI CountdownText;
    public TextMeshProUGUI LoopsText;

    void Start()
    {
        Instance = this;
        Countdown = MaxTime;
        Debug.Log($"Loops: {Loops}");

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (Loops > 0)
        {
            LoopsText.gameObject.SetActive(true);
            LoopsText.SetText($"Loops: {Loops}");
        }
    }

    void Update()
    {
        Countdown -= Time.deltaTime;

        CountdownText.SetText(Mathf.RoundToInt(Countdown).ToString());

        if (Countdown < 0)
        {
            RestartLoop();
        }
    }

    public void RestartLoop()
    {
        Loops++;
        SceneManager.LoadScene(0);
    }
}
