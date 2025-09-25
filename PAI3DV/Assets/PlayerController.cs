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
        DrivingEffects();
        GroundedCheck();
        ApplyGravity();
        MovingCheck();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        
    }

    private void MovingCheck()
    {
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
    private void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //Falling down
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
    }

    private void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
    
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }


    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward;

        float turnSpeed = Mathf.Lerp(1f, 0.5f, Time.deltaTime * velocity.magnitude * 10f);
        
            
        if (x != 0)
        {
            if (z < 0)
            {
                direction = Vector3.Lerp(direction, transform.right * -x, Time.deltaTime * turnSpeed);
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

    private void DrivingEffects()
    {
        
        foreach (var dustParticle in dustParticles)
        {
            if(!dustParticle.isPlaying && isGrounded) dustParticle.Play();
            else if (dustParticle.isPlaying && !isGrounded) dustParticle.Stop();
        }

        foreach (var wheelTrail in wheelTrails)
        {
            if(!wheelTrail.emitting && isGrounded) wheelTrail.emitting = true;
            else if (wheelTrail.emitting && !isGrounded) wheelTrail.emitting = false;
        }
        
    }
}
