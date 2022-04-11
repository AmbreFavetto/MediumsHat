using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 50f;

    public GameObject stairsZone;
    public bool stairsEnable = false;

    public Inventory inventory;

    private float horizontalMovement;

    private Vector3 velocity = Vector3.zero;
    Vector2 lastClickedPos;

    bool moving;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public static PlayerController instance;

    public AudioSource audioSource;
    public AudioClip sound;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("Il y a plus d'une instance de PlayerController dans la scène");
            return;
        }
        instance = this;
    }

    private void FixedUpdate()
    {
        KeyboardMovement();
    }

    private void Update()
    {
        MouseMovement();
    }

    private void MouseMovement()
    {
        //AudioSource.PlayClipAtPoint(sound, player.transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }

        if (moving && transform.position.x.ToString("0.00") != lastClickedPos.x.ToString("0.00"))
        {
            float step = moveSpeed * 0.03f * Time.deltaTime;
            lastClickedPos.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
        }
        else
        {
            moving = false;
        }

        Flip(_mousePosition: lastClickedPos.x);
    }

    private void KeyboardMovement()
    {
        //AudioSource.PlayClipAtPoint(sound, player.transform.position);
        if (Input.GetAxis("Horizontal") != 0)
        {
            lastClickedPos = transform.position;
            //print(Input.GetAxis("Horizontal"));
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            lastClickedPos = transform.position;
            horizontalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        }

        MovePlayer(horizontalMovement);
        Flip(rb.velocity.x, transform.position.x);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        stairsEnable = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        stairsEnable = false;
    }

    void Flip(float _velocity = 0, float _mousePosition = 0)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else if (_mousePosition > transform.position.x && moving == true)
        {
            spriteRenderer.flipX = false;
        }
        else if (_mousePosition < transform.position.x && moving == true)
        {
            spriteRenderer.flipX = true;
        }
    }

}
