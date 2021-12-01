using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public GameObject stairsZone;
    public bool stairsEnable = false;
    //public Rigidbody2D cam;

    private float horizontalMovement;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            horizontalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        }
        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        //cam.velocity = Vector3.SmoothDamp(cam.velocity, targetVelocity, ref velocity, .05f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        stairsEnable = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        stairsEnable = false;
    }

}
