using System;
using UnityEngine;

public class GateOpeningScript : MonoBehaviour
{
    public GameObject GateLeft;
    public GameObject GateRight;

    public float openRange = 90f;  
    public float openSpeed = 1f;    

    private Quaternion leftClosedRot;
    private Quaternion rightClosedRot;
    private Quaternion leftOpenRot;
    private Quaternion rightOpenRot;

    private float leftT = 0f;
    private float rightT = 0f;

    private enum GateState { Open, Closed, Opening, Closing }
    [SerializeField] private GateState state = GateState.Closed;

    private void Start()
    {
        leftClosedRot = GateLeft.transform.rotation;
        rightClosedRot = GateRight.transform.rotation;
        
        leftOpenRot = leftClosedRot * Quaternion.Euler(0f, openRange, 0f);
        rightOpenRot = rightClosedRot * Quaternion.Euler(0f, -openRange, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (state == GateState.Closed) state = GateState.Opening;
            else if (state == GateState.Open) state = GateState.Closing;
        }

        switch (state)
        {
            case GateState.Opening:
                leftT += openSpeed * Time.deltaTime;
                rightT += openSpeed * Time.deltaTime;

                GateLeft.transform.rotation = Quaternion.Lerp(leftClosedRot, leftOpenRot, leftT);
                GateRight.transform.rotation = Quaternion.Lerp(rightClosedRot, rightOpenRot, rightT);

                if (Quaternion.Angle(GateLeft.transform.rotation, leftOpenRot) < 0.5f &&
                    Quaternion.Angle(GateRight.transform.rotation, rightOpenRot) < 0.5f)
                {
                    state = GateState.Open;
                }
                break;

            case GateState.Closing:
                leftT -= openSpeed * Time.deltaTime;
                rightT -= openSpeed * Time.deltaTime;

                GateLeft.transform.rotation = Quaternion.Lerp(leftClosedRot, leftOpenRot, leftT);
                GateRight.transform.rotation = Quaternion.Lerp(rightClosedRot, rightOpenRot, rightT);

                if (Quaternion.Angle(GateLeft.transform.rotation, leftClosedRot) < 0.5f &&
                    Quaternion.Angle(GateRight.transform.rotation, rightClosedRot) < 0.5f)
                {
                    state = GateState.Closed;
                }
                break;
        }
        
        leftT = Mathf.Clamp01(leftT);
        rightT = Mathf.Clamp01(rightT);
    }
}