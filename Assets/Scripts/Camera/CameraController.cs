using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private PlayerController playerControl;
    private bool isRotating;
    public bool IsRotating
    {
        get => isRotating;
        set => isRotating = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = GetNewOffset();
        playerControl = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.position - offset;


        //pause player movement while camera spin
        if (isRotating)
        {
            if (playerControl.enabled) playerControl.enabled = false;

            if (offset != GetNewOffset())
            {
                offset = GetNewOffset();
            }
        }
        else
        {
            if (!playerControl.enabled) playerControl.enabled = true;
        }

        transform.position = player.position - offset; //lock camera to follow player
    }

    public void RotateAroundPlayer(float distance)
    {
        transform.RotateAround(player.transform.position, Vector3.up, distance);
    }

    public void SetGlobalYRotation(float degree)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, degree, transform.rotation.z);
        IsRotating = false;
        offset = GetNewOffset();
    }

    private Vector3 GetNewOffset()
    {
        return player.position - transform.position;
    }
}
