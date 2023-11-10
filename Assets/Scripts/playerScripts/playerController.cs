using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    #region

    [SerializeField] private Camera mainCamera;
    //private Rigidbody rb;
    // Player Inputs
    private PlayerInputs playerInputs;
    Vector2 runAction;   // Stores the movement input from the player.
    Vector2 aimAction;    // Stores the aim input from the player.
    public bool SprintAction = false;
    public bool shoot = false;
    public bool walkAction = false;

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

    gameManager gameManager;
    //event handler
    public bool isGameStart;
    #endregion 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<gameManager>();
        playerCombat = GetComponent<playerCombat>();
        isGameStart = false;
        //rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<playerAnimation>();
    }

    public void Update()
    {
        if(gameManager.isGameStart)
        {
            handleAllInput();
        }
    }

    #region Enable Input Manager 
    private void OnEnable()
    {
        if (playerInputs == null)
        {
            playerInputs = new PlayerInputs();

            // Subscribe to the player's movement input action.
            playerInputs.playerMoveController.playerMovement.performed += i => runAction = i.ReadValue<Vector2>();

            //Subscribe to the walk input action
            playerInputs.playerMoveController.walk.performed += i => walkAction = true;
            playerInputs.playerMoveController.walk.canceled += i => walkAction = false;
         
            // Subscribe to the player's mouse aim input action.
            playerInputs.playerMoveController.mouseAim.performed += i => aimAction = i.ReadValue<Vector2>();

            // Subscribe to the sprint Action.
            playerInputs.playerMoveController.Sprint.performed += i => SprintAction = true;
            playerInputs.playerMoveController.Sprint.canceled += i => SprintAction = false;

            //Subscribe to the shooting Action.
            playerInputs.playerActionController.shoot.performed += i => shoot = true;
            playerInputs.playerActionController.shoot.canceled += i => shoot = false;

            //Call the reload function
            playerInputs.playerActionController.reload.canceled += i => reloadAction();
        }
        playerInputs.Enable(); // Enable the player input actions.
    }

    private void OnDisable()
    {
        if (playerInputs != null)
        {
            // Unsubscribe from the player's movement input action.
            playerInputs.playerMoveController.playerMovement.performed -= i => runAction = i.ReadValue<Vector2>();

            // Subscribe to the player's mouse aim input action.
            playerInputs.playerMoveController.mouseAim.performed -= i => aimAction = i.ReadValue<Vector2>();

            // Unsubscribe to the sprint Action.
            playerInputs.playerMoveController.Sprint.performed -= i => SprintAction = true;
            playerInputs.playerMoveController.Sprint.canceled -= i => SprintAction = false;

            // Unsubscribe to the shooting Action.
            playerInputs.playerActionController.shoot.performed -= i => shoot = true;
            playerInputs.playerActionController.shoot.canceled -= i => shoot = false;

            //Unsubscribe to the walk Action
            playerInputs.playerMoveController.walk.performed -= i => walkAction = true;
            playerInputs.playerMoveController.walk.performed -= i => walkAction = false;

        }
        playerInputs.Disable(); // Disable the player input actions.
    }
    #endregion  
    //put here all input handler to one function
    public void handleAllInput()
    {
        handleRun();
        handleSprint();
        HandleAim();
        shootAction();
        handWalk();
    }

    #region Shoot Action
    public void shootAction()
    {
        //shooting logic
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
    #endregion

    #region Handle Reload
    public void reloadAction()
    {
        // check how many bullets are needed to fill in the magazine.
        int bulletsNeeded = playerCombat.M4AIstats.magazineSize - playerCombat.magazineSize;
        if (gameManager.isGameStart)
        {
            if (playerCombat.ammoBag > 0)
            {
                if (playerCombat.ammoBag >= bulletsNeeded)
                {
                    // Deduct the required bullets from the ammo bag to fill the magazine.
                    playerCombat.ammoBag -= bulletsNeeded;

                    // Set the magazine size to its maximum capacity.
                    playerCombat.magazineSize = playerCombat.M4AIstats.magazineSize;
                }
                else
                {
                    // If there are not enough bullets in the ammo bag to fill the magazine, 
                    // use all available bullets to partially reload the magazine.
                    playerCombat.magazineSize += playerCombat.ammoBag;
                    playerCombat.ammoBag = 0;
                }

                playerAnim.reloadAnimation();
                //Debug.Log("Ammo Bag: " + playerCombat.ammoBag);
                //Debug.Log("Magazine: " + playerCombat.magazineSize);
            }
            else
            {
                Debug.Log("Ammo bag is empty");
            }
        }

    }
    #endregion

    #region Handel Run
    public void handleRun()
    {
        moveDirection = runAction;
        // Smoothly blend the current animation based on the movement direction.
        currentAnimationBlend = Vector2.SmoothDamp(currentAnimationBlend, moveDirection, ref animationVelocity, animationSmoothTime * Time.deltaTime);

        movements = new Vector3(currentAnimationBlend.x, 0f, currentAnimationBlend.y).normalized;
    
        // Update the player's animation values and move the player.
        playerAnim.updateAnimValue(currentAnimationBlend.x, currentAnimationBlend.y);
        transform.position += movements * SoldierStats.runSpeed * Time.deltaTime;
    }
    #endregion

    #region Handle Walk
    public void handWalk()
    {
        if (walkAction && movements != Vector3.zero)
        {
           //deduct the walkspeed to the current movespeed
            transform.position -= movements * SoldierStats.walkSpeed * Time.deltaTime;
   
            Debug.Log("some");
        }
        else
        {
            walkAction = false;
        
        }
    }
    #endregion

    #region HandeSprint
    public void handleSprint()
    {
       if (SprintAction && movements != Vector3.zero)
        {
            //add the run speed value to the current movement speed
            transform.position += movements * SoldierStats.sprintSpeed * Time.deltaTime;
            //Debug.Log("sprint");
        }
        else
        {
            SprintAction = false;
        }
    }
    #endregion

    #region Handle Aim
    void HandleAim()
    {
        aimDir = aimAction;

        // Create a ray from the camera to the mouse position.
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(aimDir.x, aimDir.y, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Get the point on the ground where the ray hits.
            Vector3 playerPosition = hit.point;

            playerPosition.y = transform.position.y; // Maintain the player's current height.

            // Smooth rotation of the player to aim at the target.
            Quaternion targetRot = Quaternion.LookRotation(playerPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 50f * Time.deltaTime);
        }
    }
    #endregion  

}
