using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    //public Rigidbody2D cam;

    private float horizontalMovement;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        //cam.velocity = Vector3.SmoothDamp(cam.velocity, targetVelocity, ref velocity, .05f);
    }


}
