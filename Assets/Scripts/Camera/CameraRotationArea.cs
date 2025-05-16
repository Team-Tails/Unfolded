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

    private float goalAngle;
    private float currentYRotation;
    private float direction;

    private void Start()
    {
        currentYRotation = cam.transform.rotation.eulerAngles.y;
        direction = speed;
        goalAngle = goalCamAngleOutArea;
    }

    void OnTriggerEnter(Collider other)
    {
        goalAngle = goalCamAngleInArea;
        direction = speed; //rotate positively (right)
    }

    void OnTriggerExit(Collider other)
    {
        goalAngle = goalCamAngleOutArea;
        direction = -speed; //rotate negatively (left)
    }

    void Update()
    {   
        // not within the threshold of the goal angle
        if ((goalAngle == goalCamAngleInArea && currentYRotation <= goalCamAngleInArea - thresholdForCameraLockToAngle) ||
        (goalAngle == goalCamAngleOutArea && currentYRotation >= goalCamAngleOutArea + thresholdForCameraLockToAngle))
        {
            cam.transform.RotateAround(player.transform.position, Vector3.up, direction * Time.deltaTime);
            currentYRotation = cam.transform.rotation.eulerAngles.y;
        }
        else if (currentYRotation != goalAngle) //within threshold, but not at goal
        {
            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.x, goalAngle, cam.transform.rotation.z);
        }
    }
}
