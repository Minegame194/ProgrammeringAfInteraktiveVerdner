using System;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    private Material crystalMat;
    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        crystalMat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        Debug.Log(Vector3.Distance(player.position, transform.position));
        if (Vector3.Distance(player.position, transform.position) <= 20f)
        {
            crystalMat.color = Color.yellow;
            crystalMat.SetColor("_EmissionColor", Color.yellow);
        }
        else
        {
            crystalMat.color = Color.magenta;
            crystalMat.SetColor("_EmissionColor", Color.magenta);
        }
    }
}
