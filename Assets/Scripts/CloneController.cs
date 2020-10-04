using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    public GameObject Original;

    SpriteRenderer originalSpriteRenderer;
    SpriteRenderer ourSpriteRenderer;

    void Start()
    {
        originalSpriteRenderer = Original.GetComponent<SpriteRenderer>();
        if (originalSpriteRenderer != null)
        {
            ourSpriteRenderer = GetComponent<SpriteRenderer>();
            ourSpriteRenderer.sprite = originalSpriteRenderer.sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Original == null)
        {
            Destroy(gameObject);
            return;
        }
        if (Original.activeSelf != isActiveAndEnabled)
        {
            gameObject.SetActive(Original.activeSelf);
        }
        if (originalSpriteRenderer != null && originalSpriteRenderer.sprite != ourSpriteRenderer.sprite)
        {
            ourSpriteRenderer.sprite = originalSpriteRenderer.sprite;
        }
    }
}
