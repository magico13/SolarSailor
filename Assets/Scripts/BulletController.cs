using UnityEngine;

public class BulletController : MonoBehaviour
{

    void Start()
    {
        //get audio player, play unless is clone
        var audio = GetComponent<AudioSource>();
        if (audio && GetComponent<DynamicCloneController>() == null)
        {
            audio.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
        {
            return;
        }
        Destroy(gameObject);
    }
}
