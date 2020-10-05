using UnityEngine;

public class WrapperController : MonoBehaviour
{
    //public float TargetX;
    public GameObject LinkedWrapper;
    public bool CloneItems = true;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //teleport the object to the target location, keep velocity and relative position
        Rigidbody2D them = otherCollider.GetComponent<Rigidbody2D>();
        if (them != null)
        {
            if (!them.gameObject.CompareTag("Clone")
                && otherCollider.GetComponent<DynamicCloneController>() == null //can't be a clone or have been cloned
                && otherCollider.GetComponent<HasBeenClonedComponent>()?.Cloned != true)
            {
                //create a copy of them to show, unless it's the player
                if (CloneItems && !them.gameObject.CompareTag("Player"))
                {
                    GameObject copy = Instantiate(them.gameObject);
                    DynamicCloneController clone = copy.AddComponent<DynamicCloneController>();
                    clone.Original = them.gameObject;
                    clone.transform.position = them.position;
                    otherCollider.gameObject.AddComponent<HasBeenClonedComponent>().Clone = copy;
                }

                var linkedCollider = LinkedWrapper.GetComponent<Collider2D>();

                float newX = linkedCollider.bounds.center.x;
                if (newX < transform.position.x)
                {
                    newX += linkedCollider.bounds.extents.x + 1;
                }
                else
                {
                    newX -= linkedCollider.bounds.extents.x + 1;
                }

                them.position = new Vector2(newX, them.transform.position.y);
            }
        }
        else
        {
            Debug.LogWarning("Wrapper had collision with something without a rigidbody.");
        }
    }
}
