using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWrapperController : MonoBehaviour
{
    public GameObject LinkedWrapper;
    public int ManualOffset = 0;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //teleport the object to the target location, keep velocity and relative position
        GameObject them = otherCollider.gameObject;
        if (them != null)
        {
            if (!them.CompareTag("Clone") && !them.CompareTag("Player")
                && otherCollider.GetComponent<DynamicCloneController>() == null //can't be a clone or have been cloned
                && otherCollider.GetComponent<HasBeenClonedComponent>()?.Cloned != true
                && otherCollider.GetComponent<VisiblyCloned>()?.Cloned != true)
            {
                var ourCollider = GetComponent<Collider2D>();
                var linkedCollider = LinkedWrapper.GetComponent<Collider2D>();

                //create a copy of them to show, unless it's the player
                GameObject copy = Instantiate(them);
                DynamicCloneController clone = copy.AddComponent<DynamicCloneController>();
                clone.Original = them;

                float newX = linkedCollider.bounds.center.x;
                if (linkedCollider.transform.position.x < transform.position.x)
                {
                    newX -= linkedCollider.bounds.size.x + 2;
                }
                else
                {
                    newX += linkedCollider.bounds.size.x - 2;
                }

                newX += ManualOffset;

                clone.transform.position = (them.transform.position - ourCollider.bounds.center) 
                    + new Vector3(newX, linkedCollider.bounds.center.y); //get relative position, put at same relative position on other side
                clone.tag = "Clone";
                them.AddComponent<VisiblyCloned>().Clone = copy;
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        VisiblyCloned cloned = otherCollider.GetComponent<VisiblyCloned>();
        if (cloned != null)
        {
            cloned.Cloned = false;
            Destroy(cloned.Clone);
        }
    }
}
