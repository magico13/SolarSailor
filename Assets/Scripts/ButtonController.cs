using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject Door;
    public Sprite AlternateSprite;

    private bool pressed = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!pressed && collision.collider?.gameObject?.CompareTag("Player") == true)
        {
            OnPress();
        }
    }

    void OnPress()
    {
        Destroy(Door);
        GetComponent<SpriteRenderer>().sprite = AlternateSprite;
        pressed = true;
        GetComponent<Collider2D>().enabled = false;
    }
}
