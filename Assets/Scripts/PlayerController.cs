using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float ThrowForce;
    public float Rotation;
    public GameObject Wrench;
    public AudioClip ThrowSound;

    private AudioSource _audio;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2(0, -2f);
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player clicks, figure out the direction, apply force in the opposite direction

        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = worldMousePos - transform.position;
            direction.Normalize();

            gameObject.GetComponent<Rigidbody2D>().AddForce(-ThrowForce*direction);

            //create wrench object
            GameObject created = Instantiate(Wrench, transform.position + new Vector3(Mathf.Sign(direction.x)*0.5f, 0), Quaternion.identity);
            created.SetActive(true);
            Rigidbody2D rigid = created.GetComponent<Rigidbody2D>();
            rigid.velocity = GetComponent<Rigidbody2D>().velocity;
            rigid.AddForce(ThrowForce * direction);
            rigid.AddTorque(Random.Range(-Rotation, Rotation));

            _audio.PlayOneShot(ThrowSound);
        }
    }

    void FixedUpdate()
    {
        
    }
}
