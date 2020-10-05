using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int Loops = 0;
    public static int WrenchesThrown = 0;
    public static bool Playing = true;

    // seconds until loop restarts automatically
    public int MaxTime = 60;
    public float Countdown;
    public TextMeshProUGUI ReactorText;
    public TextMeshProUGUI CountdownText;
    public TextMeshProUGUI LoopsText;

    public TextMeshProUGUI WinText;
    public TextMeshProUGUI WinSupplemental;

    

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
        if (Playing)
        {
            Countdown -= Time.deltaTime;

            CountdownText.SetText(Mathf.RoundToInt(Countdown).ToString());

            if (Countdown < 0)
            {
                RestartLoop();
            }
        }
    }

    public void RestartLoop()
    {
        Loops++;
        SceneManager.LoadScene(0);
    }

    public void DoWin()
    {
        Playing = false;
        ReactorText.gameObject.SetActive(false);
        CountdownText.gameObject.SetActive(false);
        LoopsText.gameObject.SetActive(false);

        GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 255);
        

        WinText.gameObject.SetActive(true);
        WinSupplemental.gameObject.SetActive(true);
        WinSupplemental.SetText(string.Format(WinSupplemental.text, Mathf.RoundToInt(Countdown), Loops, WrenchesThrown));

        //deactivate player
        //FindObjectOfType<PlayerController>().gameObject.SetActive(false);
    }
}
