using System;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;

public class SirenRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 20f;
    
    

    // Update is called once per frame
    void Update()
    {
        
        transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        
    }

   
}
