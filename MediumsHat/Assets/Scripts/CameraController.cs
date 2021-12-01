using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera deplacement speed
    public float panSpeed = 20f;   
    public float panBorderThickness = 10f;
    public GameObject player;

    // Limits of the camera
    private float leftLimit;
    private float rightLimit;
    private float upLimit;
    private float downLimit;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        leftLimit = player.transform.position.x;
        rightLimit = player.transform.position.x + 5;
        downLimit = player.transform.position.y;
        upLimit = player.transform.position.y + 3;

        // Up
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        // Down
        if (Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        // Right
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        // Left
        if (Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);
        pos.y = Mathf.Clamp(pos.y, downLimit, upLimit);

        transform.position = pos;
    }
}
