using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed;
    public float ThrowForce;
    public float Rotation;
    public GameObject Wrench;
    public AudioClip ThrowSound;

    private AudioSource _audio;
    private float _distToGround;
    private Rigidbody2D _body;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.gravity = new Vector2(0, -2f);
        _audio = GetComponent<AudioSource>();
        _distToGround = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
        _body = GetComponent<Rigidbody2D>();
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

            _body.AddForce(-ThrowForce*direction);

            //create wrench object
            GameObject created = Instantiate(Wrench, transform.position + new Vector3(Mathf.Sign(direction.x)*0.5f, 0), Quaternion.identity);
            created.SetActive(true);
            Rigidbody2D rigid = created.GetComponent<Rigidbody2D>();
            rigid.velocity = GetComponent<Rigidbody2D>().velocity;
            rigid.AddForce(ThrowForce * direction);
            rigid.AddTorque(Random.Range(-Rotation, Rotation));
            Destroy(created, 10f);

            _audio.PlayOneShot(ThrowSound);
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _body.AddForce(ThrowForce * 3 * Vector2.up);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.down, _distToGround, LayerMask.GetMask("Default"));
        //Debug.DrawLine(transform.position, raycast.point);
        return raycast.collider != null && raycast.distance < _distToGround;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal") && IsGrounded())
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(_body.velocity.x) < MaxSpeed)
            {
                _body.AddForce(ThrowForce * horizontal * Vector2.right);
            }
        }
    }
}
