using System;
using Unity.Mathematics;
using UnityEngine;

public class GateOpeningScript : MonoBehaviour
{
    public GameObject GateLeft;
    public GameObject GateRight;
    public float openRange = 0;

    public float openSpeed = 0.1f;

    private Vector3 GateLeftOriginRot;
    private Vector3 GateRightOriginRot;
    private Vector3 GateLeftOpenPos;
    private Vector3 GateRightOpenPos;

    private float gateFloat = 0f;

    public float OpenOffset = 5f;

    
   private enum GateState
    {
        Open, Closed, Opening, Closing
    }

    private void Start()
    {
        GateLeftOriginRot = GateLeft.GetComponent<Transform>().eulerAngles;
        GateRightOriginRot = GateRight.GetComponent<Transform>().eulerAngles;
        
        GateRightOpenPos = GateRightOriginRot + new Vector3(0f, -openRange, 0f);
        GateLeftOpenPos = GateLeftOriginRot + new Vector3(0f, openRange, 0f);
    }

    [SerializeField] private GateState state = GateState.Closed;
    [SerializeField] private GateState gateRightstate = GateState.Closed;
    [SerializeField] private GateState gateLeftstate = GateState.Closed;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (gateRightstate == GateState.Open && gateLeftstate == GateState.Open)
            {
                state = GateState.Closing;
                gateLeftstate = GateState.Closing;
                gateRightstate = GateState.Closing;
            }
            else if (gateRightstate == GateState.Closed && gateLeftstate == GateState.Closed)
            {
                state = GateState.Opening;
                gateLeftstate = GateState.Opening;
                gateRightstate = GateState.Opening;
            }
        }

        if (state == GateState.Closing || state == GateState.Opening)
        {
            gateRightstate = MoveGate(GateRight, gateRightstate, GateRightOriginRot, GateRightOpenPos);
            gateLeftstate = MoveGate(GateLeft, gateLeftstate, GateLeftOriginRot, GateLeftOpenPos);
            
            if(gateLeftstate == GateState.Open && gateRightstate == GateState.Open) state = GateState.Open;
            if(gateLeftstate == GateState.Closed && gateRightstate == GateState.Closed) state = GateState.Closed;

        }
        
    }

    bool gateCheck(Vector3 currentPosition, Vector3 desiredPosition)
    {
        return currentPosition.magnitude >= desiredPosition.magnitude - OpenOffset && currentPosition.magnitude <= desiredPosition.magnitude + OpenOffset;
    }
    

    GateState MoveGate(GameObject gate, GateState gateState, Vector3 closedPosition, Vector3 openPosition)
    {
        switch (gateState)
        {
            case GateState.Opening:
                
                
                gate.transform.eulerAngles = math.lerp(closedPosition, openPosition, gateFloat);
                gateFloat += openSpeed * Time.deltaTime;
                
                
                //float rotation = Mathf.Lerp(closedPosition.y, openPosition.y, Time.deltaTime);
                
                //gate.transform.eulerAngles = new Vector3(0f, gate.transform.eulerAngles.y + rotation, 0f);
                Debug.Log("lerp: " + openSpeed * Time.deltaTime);
                if(gateCheck(gate.transform.eulerAngles, openPosition )) gateState = GateState.Open;
                break;
                
            case GateState.Closing:
                gate.transform.eulerAngles = math.lerp(openPosition, closedPosition, openSpeed * Time.deltaTime);
                if(gateCheck(gate.transform.eulerAngles, closedPosition)) gateState = GateState.Closed;
                break;
        }
        
        return gateState;
        
        
        
        
    }
}
