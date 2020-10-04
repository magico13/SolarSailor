using UnityEngine;

public class TurretController : MonoBehaviour
{

    GameObject ThePlayer;
    GameObject Bullet;

    float shootTimeout = 1f;
    float shootCounter = 0f;
    bool canShoot = false;

    bool onCeiling;

    // Start is called before the first frame update
    void Start()
    {
        ThePlayer = FindObjectOfType<PlayerController>().gameObject;
        Bullet = FindObjectOfType<TurretHolder>().Bullet;

        onCeiling = GetComponent<SpriteRenderer>().sprite.name == "turret_c";
    }

    // Update is called once per frame
    void Update()
    {
        // look for player, try to shoot them
        //Debug.DrawLine(transform.position, ThePlayer.transform.position);
        if (!canShoot)
        {
            shootCounter += Time.deltaTime;
            canShoot = shootCounter > shootTimeout;
            return;
        }
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, ThePlayer.transform.position - transform.position);
        Debug.DrawLine(transform.position, raycast.point);
        if (raycast.collider?.gameObject?.CompareTag("Player") == true)
        {
            shootCounter = 0;
            canShoot = false;

            GameObject player = raycast.collider.gameObject;
            //is player, try to shoot
            Vector3 shotPosition = transform.position + new Vector3(0, (onCeiling ? -1 : 1) * 0.9f);
            Vector2 direction = (player.transform.position - shotPosition).normalized;
            
            GameObject created = Instantiate(Bullet, shotPosition, Quaternion.identity);
            created.SetActive(true);
            Rigidbody2D rigid = created.GetComponent<Rigidbody2D>();
            rigid.AddForce(150 * direction);
            Destroy(created, 10f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider?.gameObject?.CompareTag("Weapon") == true)
        {
            //_audio.PlayOneShot(HitWallSound, collision.relativeVelocity.magnitude / 10f);
            Destroy(gameObject);
            Destroy(collision.collider.gameObject);
        }
    }
}
