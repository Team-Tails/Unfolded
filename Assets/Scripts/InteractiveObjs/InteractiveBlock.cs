using UnityEngine;

public class InteractiveBlock : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Vector3 moveDirection = Vector3.zero;
    private bool isColliding = false;

    private void OnTriggerEnter(Collider other)
    {
        // If not a rhino, don't push
        if (PlayerStateController.Instance.CurrentState != PlayerStateController.Instance.RhinoState)
        {
            return;
        }

        moveDirection = (transform.position - other.transform.position);
        moveDirection.y = 0f;
        moveDirection.Normalize();

        isColliding = true;
        SoundManager.Instance.PlaySound("BoxMove");
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        moveDirection = Vector3.zero;
    }

    private void Update()
    {
        if (isColliding)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}