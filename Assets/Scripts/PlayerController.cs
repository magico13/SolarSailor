using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed;
    public float ThrowForce;
    public float Rotation;
    public GameObject Wrench;
    public AudioClip ThrowSound;

    public bool DebugMode = false;

    private AudioSource _audio;
    private float _distToGround;
    private Rigidbody2D _body;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.gravity = new Vector2(0, -2f);
        _audio = GetComponent<AudioSource>();
        _distToGround = GetComponent<Collider2D>().bounds.extents.y + 0.25f;
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

            Vector3 wrenchSpawn = transform.position + new Vector3(Mathf.Sign(direction.x) * 0.75f, 0);
            Vector2 wrenchDir = worldMousePos - wrenchSpawn;
            //create wrench object
            GameObject created = Instantiate(Wrench, wrenchSpawn, Quaternion.Euler(0, 0, 0));
            created.SetActive(true);
            Rigidbody2D rigid = created.GetComponent<Rigidbody2D>();
            rigid.velocity = GetComponent<Rigidbody2D>().velocity;
            rigid.AddForce(ThrowForce * wrenchDir.normalized);
            rigid.angularVelocity = (Random.Range(-Rotation, Rotation));
            Destroy(created, 10f);

            _audio.PlayOneShot(ThrowSound);
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _body.AddForce(ThrowForce * 2 * Vector2.up);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject?.CompareTag("EnemyWeapon") == true)
        {
            //Destroy(this.gameObject);
            Debug.Log("Got hit!");
            if (!DebugMode)
            {
                GameController.Instance.RestartLoop();
            }
        }
    }

    bool IsGrounded()
    {
        bool grounded = Physics2D.IsTouchingLayers(GetComponent<Collider2D>(), LayerMask.GetMask("Default"));
        if (grounded) return true;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, Vector2.down, _distToGround, LayerMask.GetMask("Default"));
        ////Debug.DrawLine(transform.position, raycast.point);
        return raycast.collider != null && raycast.distance < _distToGround;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal") && IsGrounded())
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(_body.velocity.x) < MaxSpeed
                || (Mathf.Sign(horizontal) != Mathf.Sign(_body.velocity.x)))
            {
                _body.AddForce(ThrowForce * horizontal * Vector2.right);
            }
        }
        if (Input.GetButton("Vertical") && IsGrounded())
        {
            float vertical = Input.GetAxis("Vertical");
            // only down
            if (vertical < 0)
            {
                _body.AddForce(ThrowForce * vertical * Vector2.up);
            }
        }
    }
}
