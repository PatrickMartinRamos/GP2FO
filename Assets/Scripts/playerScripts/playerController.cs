using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Rigidbody rb;
    // Player Inputs
    private PlayerInputs playerInputs;
    Vector2 walkAction;   // Stores the movement input from the player.
    Vector2 aimAction;    // Stores the aim input from the player.
    public bool SprintAction = false;
    public bool shoot = false;
 
    // Player Stats
    public playerStats SoldierStats;
    playerCombat playerCombat;
    // Player Motion
    Vector2 moveDirection; // Stores the calculated movement direction.
    Vector2 aimDir;        // Stores the calculated aim direction.
    Vector3 movements;     // Stores the calculated movement vector.

    // Animation
    playerAnimation playerAnim;
    Vector2 currentAnimationBlend;
    Vector2 animationVelocity;
    private float animationSmoothTime = 0.1f;

    public bool startShooting = false;
    
    //event handler
    public bool isGameStart;

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GetComponent<playerCombat>();
        isGameStart = false;
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<playerAnimation>();
    }

    private void OnEnable()
    {
        if (playerInputs == null)
        {
            playerInputs = new PlayerInputs();

            // Subscribe to the player's movement input action.
            playerInputs.playerMoveController.playerMovement.performed += i => walkAction = i.ReadValue<Vector2>();

            // Subscribe to the player's mouse aim input action.
            playerInputs.playerMoveController.mouseAim.performed += i => aimAction = i.ReadValue<Vector2>();

            // Subscribe to the sprint Action.
            playerInputs.playerMoveController.Sprint.performed += i => SprintAction = true;
            playerInputs.playerMoveController.Sprint.canceled += i => SprintAction = false;

            //Subscribe to the shooting Action.
            playerInputs.playerActionController.shoot.performed += i => shoot = true;
            playerInputs.playerActionController.shoot.canceled += i => shoot = false;
        }
        playerInputs.Enable(); // Enable the player input actions.
    }

    private void OnDisable()
    {
        if (playerInputs != null)
        {
            // Unsubscribe from the player's movement input action.
            playerInputs.playerMoveController.playerMovement.performed -= i => walkAction = i.ReadValue<Vector2>();

            // Subscribe to the player's mouse aim input action.
            playerInputs.playerMoveController.mouseAim.performed -= i => aimAction = i.ReadValue<Vector2>();

            // Unsubscribe to the sprint Action.
            playerInputs.playerMoveController.Sprint.performed -= i => SprintAction = true;
            playerInputs.playerMoveController.Sprint.canceled -= i => SprintAction = false;

            // Unsubscribe to the shooting Action.
            playerInputs.playerActionController.shoot.performed -= i => shoot = true;
            playerInputs.playerActionController.shoot.canceled -= i => shoot = false;
        }
        playerInputs.Disable(); // Disable the player input actions.
    }

    public void Update()
    {
        if(isGameStart)
        {
            handleAllInput();
        }
     
    }
    //put here all input handler to one function
    public void handleAllInput()
    {
        handleWalk();
        handleSprint();
        HandleAim();
        shootAction();
    }
    public void shootAction()
    {
        if(shoot)
        {
            playerCombat.startShootAction();
            startShooting = true;
        }
        else
        {
            playerCombat.stopShootAction();
            shoot = false;
            startShooting = false;
        }
    }

    public void handleWalk()
    {
        moveDirection = walkAction;
        // Smoothly blend the current animation based on the movement direction.
        currentAnimationBlend = Vector2.SmoothDamp(currentAnimationBlend, moveDirection, ref animationVelocity, animationSmoothTime * Time.deltaTime);

        movements = new Vector3(currentAnimationBlend.x, 0f, currentAnimationBlend.y).normalized;
    
        // Update the player's animation values and move the player.
        playerAnim.updateAnimValue(currentAnimationBlend.x, currentAnimationBlend.y);
        transform.position += movements * SoldierStats.walkSpeed * Time.deltaTime;
    }
    public void handleSprint()
    {
       if (SprintAction && movements != Vector3.zero)
        {
            //add the run speed value to the current movement speed
            transform.position += movements * SoldierStats.runSpeed * Time.deltaTime;
            //Debug.Log("sprint");
        }
        else
        {
            SprintAction = false;
        }
    }
    void HandleAim()
    {
        aimDir = aimAction;

        // Create a ray from the camera to the mouse position.
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(aimDir.x, aimDir.y, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Get the point on the ground where the ray hits.
            Vector3 playerPosition = hit.point;

            // Set the player's position on the X and Z axis to match the mouse position.
            playerPosition.y = transform.position.y; // Maintain the player's current height.

            // Smoothly rotate the player to aim at the target.
            Quaternion targetRot = Quaternion.LookRotation(playerPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 50f * Time.deltaTime);
        }
    }

}
