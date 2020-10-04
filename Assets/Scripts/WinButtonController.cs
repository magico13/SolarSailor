using UnityEngine;

public class WinButtonController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider?.gameObject?.CompareTag("Player") == true
            || otherCollider?.gameObject?.CompareTag("Weapon") == true)
        {
            Debug.Log("Win!");
        }
    }
}
