using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines.Interpolators;

// Base written by Jenna

[RequireComponent(typeof(BoxCollider))]
public class CameraRotationArea : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private float goalCamAngleInArea = 90;
    [SerializeField] private float goalCamAngleOutArea = 0;

    [SerializeField, Tooltip("How close can the camera be to the goalAngle to stop moving")] //needed since using deltatime which can miss exact goal value
    private float thresholdForCameraLockToAngle;
    [SerializeField] private float speed;


    private CameraController camControl;
    private float goalAngle;
    private float currentYRotation;
    private float direction;

    private void Start()
    {
        currentYRotation = cam.transform.rotation.eulerAngles.y;
        direction = speed;
        goalAngle = goalCamAngleOutArea;
        camControl = cam.GetComponent<CameraController>();
    }

    void OnTriggerEnter(Collider other)
    {
        goalAngle = goalCamAngleInArea;
        direction = speed; //rotate positively (right)
        if (!camControl.IsRotating) camControl.IsRotating = true;
    }

    void OnTriggerExit(Collider other)
    {
        goalAngle = goalCamAngleOutArea;
        direction = -speed; //rotate negatively (left)
        if (!camControl.IsRotating) camControl.IsRotating = true;
    }

    void LateUpdate()
    {
        // not within the threshold of the goal angle
        if ((goalAngle == goalCamAngleInArea && currentYRotation < goalCamAngleInArea - thresholdForCameraLockToAngle) ||
        (goalAngle == goalCamAngleOutArea && currentYRotation > goalCamAngleOutArea + thresholdForCameraLockToAngle))
        {
            //player.transform.Rotate(Vector3.up * direction * Time.deltaTime);
            camControl.RotateAroundPlayer(direction * Time.deltaTime);
            currentYRotation = cam.transform.rotation.eulerAngles.y;
        }
        else if (currentYRotation != goalAngle) //within threshold, but not at goal
        {
            camControl.SetGlobalYRotation(goalAngle);
        }
        else //is at goal
        {
            if (camControl.IsRotating) camControl.IsRotating = false;
        }
    }
}
