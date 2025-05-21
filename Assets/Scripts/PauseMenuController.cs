using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;
    [SerializeField]
    GameObject pauseParent;

    public void OnPause(InputAction.CallbackContext context)
    {
        controller.enabled = false;
        pauseParent.SetActive(true);
    }
}
