using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    public Vector3 offset;
    
    public GameObject camera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera.transform.position = offset + gameObject.transform.position;
        camera.transform.LookAt(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        yRotation += mouseX;
        
        xRotation = Mathf.Clamp(xRotation, -10f, 45f);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
