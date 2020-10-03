using UnityEngine;

public class BulletController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
        {
            return;
        }
        Destroy(gameObject);
    }
}
