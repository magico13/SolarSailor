using UnityEngine;

public class WrapperController : MonoBehaviour
{
    public float TargetX;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //teleport the object to the target location, keep velocity and relative position
        Rigidbody2D them = otherCollider.GetComponent<Rigidbody2D>();
        if (them != null)
        {
            //update their position
            them.position = new Vector2(TargetX, them.transform.position.y);
        }
        else
        {
            Debug.LogWarning("Wrapper had collision with something without a rigidbody.");
        }
    }
}
