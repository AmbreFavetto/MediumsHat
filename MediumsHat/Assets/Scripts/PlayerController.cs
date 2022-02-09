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
    Vector2 lastClickedPos;
    bool moving;


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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }


        if (moving && transform.position.x.ToString("0.00") != lastClickedPos.x.ToString("0.00"))
        {
            print(transform.position.x.ToString("0.00"));
            print(lastClickedPos.x.ToString("0.00"));
            float step = moveSpeed * 0.03f * Time.deltaTime;
            // MovePlayer(moveSpeed * 5 * Time.deltaTime);
            lastClickedPos.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
        }
        else
        {
            moving = false;
        }
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
