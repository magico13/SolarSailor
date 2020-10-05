using UnityEngine;

public class WrenchController : MonoBehaviour
{
    public float DestroyDelay = 0.5f;
    public AudioClip HitWallSound;

    private AudioSource _audio;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        Destroy(gameObject, DestroyDelay);
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().IsSleeping())
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider?.gameObject?.CompareTag("Player") != true)
        {
            _audio.PlayOneShot(HitWallSound, collision.relativeVelocity.magnitude/10f);
            //Destroy(gameObject, DestroyDelay);
        }
    }
}
