using UnityEngine;
using UnityEngine.UI;

public class PlaneState : PlayerState
{
    protected const float PLANE_JUMP_HEIGHT = 0;
    protected const float PLANE_GRAVITY_MULTIPLIER = 0.2f;
    private const float FLY_TIME = 5f;

    private float timer = 5;
    private PlayerState previousState;
    private PlayerController playerController;
    private Image bar;
    private GameObject flightBar;
    private GameObject statusBar;

    public override void Start(PlayerStateController controller, GameObject statusBar)
    {
        base.Start(controller);
        jumpHeight = PLANE_JUMP_HEIGHT;
        gravityMutliplier = PLANE_GRAVITY_MULTIPLIER;
        playerController = controller.GetComponent<PlayerController>();
        this.statusBar = statusBar;
        flightBar = statusBar.transform.GetChild(1).gameObject;
        bar = flightBar.GetComponent<Image>();
    }

    public override void Update()
    {
        base.Update();

        if (controller.CurrentState != this) return;



        timer -= Time.deltaTime;
        bar.fillAmount = Mathf.Lerp(0, 1, timer/FLY_TIME);
        if (timer <= 0)
        {
            EndPlaneState();

        }
    }

    public override void EnterState(PlayerState prevState)
    {
        base.EnterState(prevState);
        statusBar.SetActive(true);
        flightBar.SetActive(true);
        previousState = prevState;
        timer = 5;
    }

    public void EndPlaneState()
    {
        statusBar.SetActive(false);
        flightBar.SetActive(false);
        if (playerController.isFlying)
        {
            playerController.isFlying = false;
        }
        if (previousState != null)
        {
            controller.ChangeState(previousState);
            previousState = null;
        }
        else
        {
            Debug.LogWarning("Plane state was entered without a previous state. Defaulting to bunny state.");
            controller.ChangeState(controller.BunnyState);
            timer = 5;
        }

    }
}
