using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;

    public float speed = 5f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isActive = true;
    
    Vector3 velocity;
    
    bool isGrounded;
    bool isMoving;

    public ParticleSystem[] dustParticles;
    public TrailRenderer[] wheelTrails;
    
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        // Locking cursor
        Cursor.lockState = CursorLockMode.Locked;
        
        controller = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < dustParticles.Length; i++)
        {
            if(!dustParticles[i].isPlaying && isGrounded) dustParticles[i].Play();
            else if (dustParticles[i].isPlaying && !isGrounded) dustParticles[i].Stop();
        }
        
        for (int i = 0; i < wheelTrails.Length; i++)
        {
            if(!wheelTrails[i].emitting && isGrounded) wheelTrails[i].emitting = true;
            else if (wheelTrails[i].emitting && !isGrounded) wheelTrails[i].emitting = false;
        }
            
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            if (isActive)
            {
                xRotation -= mouseY;
                yRotation += mouseX;
            }

            

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            

            transform.localRotation = Quaternion.Euler(0, yRotation, 0f);

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            float upSpeed = speed * Time.deltaTime;
            
            
            
            
            if (isActive)
            {
               controller.Move(move * upSpeed);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            //Falling down
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (lastPosition != gameObject.transform.position && isGrounded == true)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            lastPosition = gameObject.transform.position;
        
        
    }
}
