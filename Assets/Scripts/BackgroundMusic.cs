using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    public static GameObject Instance;
    public static bool RestartAudio = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (RestartAudio)
        {
            Instance = gameObject;
            audioSource.Play();
            DontDestroyOnLoad(gameObject);
            RestartAudio = false;
        }
    }
}
