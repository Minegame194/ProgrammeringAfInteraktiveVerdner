using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CompassScript : MonoBehaviour
{
    public Transform Player;

    public Transform Target;

    public Transform CompassPointer;
    
    
    void Update()
    {
        Vector3 targetDirection = Target.position - Player.position;
        
        float angle = Vector3.SignedAngle(Player.forward, targetDirection, Vector3.up);
        
        if(angle > -15f && angle < 15f) angle = 0f;
        
        CompassPointer.rotation = Quaternion.Euler(0f, 0f, -angle);
    }

    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }
}
