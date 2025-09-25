using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    

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
        Move();
        
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

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

        

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            //Falling down
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (lastPosition != gameObject.transform.position && isGrounded)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            lastPosition = gameObject.transform.position;
        
        
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward;

        float turnSpeed = Mathf.Lerp(1f, 0.5f, Time.deltaTime * velocity.magnitude * 10f);
        Debug.Log(velocity.magnitude);
            
        if (x != 0)
        {
            if (z < 0)
            {
                direction = Vector3.Lerp(direction, transform.right * x * -1, Time.deltaTime * turnSpeed);
            }
            else
            {
                direction = Vector3.Lerp(direction, transform.right * x, Time.deltaTime * turnSpeed);
            }
            
        }
        
        Vector3 move = direction * z;

        float upSpeed = speed * Time.deltaTime;

        controller.Move(move * upSpeed);
        if (z < 0)
        {
            transform.localRotation = Quaternion.LookRotation(-move);
        }
        else if(z > 0)
        {
            transform.localRotation = Quaternion.LookRotation(move);
        }
        
        
    }
}
