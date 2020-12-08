using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

// Class that deals with the inputs regarding the player
// - Movement, attacks, etc.
public class PlayerInputs
{
    // The player that this input is attached to.
    Player m_player;
    Rigidbody m_pRigid;
    public Animator animator;

    // The keyboard that is currently being used (might not be necessary)
    Keyboard m_keyboard;

    // The controls for the different actions a player can take.
    InputActionMap m_actionMap;
    public InputAction m_actionMove;
    InputAction m_actionLook;
    InputAction m_actionShoot;
    InputAction m_actionMelee;
    public InputAction m_actionShield;

    // Action cooldowns (seconds)
    float m_cooldownShootMin = 0.3f;
    float m_cooldownShootCurr;
    float m_cooldownMeleeMin = 1.0f;
    float m_cooldownMeleeCurr;
    float m_cooldownShieldMin = 3.0f;
    float m_cooldownShieldDur = 1.0f;
    float m_cooldownShieldCurr = 3.0f;

    public float yTarget;
    public float xTarget;

    public float ShieldCooldown => Mathf.Min(m_cooldownShieldCurr/m_cooldownShieldMin, 1.0f);


    // Member variables.
    bool m_isShooting = false;
    Vector3 m_distToCamera = new Vector3(0.0f, 16.0f, 0.0f);
    float m_forceMultiplier = 100.0f;

    public PlayerInputs(Player player, Animator anime)
    {
        m_player = player;
        m_pRigid = m_player.GetComponent<Rigidbody>();
        animator = anime;
        m_keyboard = Keyboard.current;
        CreateInputMap();
    }

    public void Update()
    {
        float dt = Time.deltaTime;
        UpdateCooldowns(dt);

        MovePlayer();

        // If they are not shooting or are on cooldown, don't shoot.
        if(m_isShooting && m_cooldownShootCurr > m_cooldownShootMin)
        {
            ShootBullet();
            m_cooldownShootCurr = 0.0f;
        }


        // The camera should be on top of the player
        m_player.pCamera.transform.position = m_player.gameObject.transform.position + m_distToCamera;

        // Limit the speed of the player.
        if(m_pRigid.velocity.sqrMagnitude > Mathf.Pow(m_player.maxSpeed, 2))
        {
            m_pRigid.velocity = m_pRigid.velocity.normalized * m_player.maxSpeed;
        }
        

        // Skip these if the player is in an animation.
        if (m_player.isInAnimation) { return; }
        RotatePlayer();

        animator.SetFloat("y", yTarget);
        animator.SetFloat("x", xTarget);
    }


    private void CreateInputMap()
    {
        m_actionMap = new InputActionMap("Player");

        // Add the movement actions to the action map.
        m_actionMove = m_actionMap.AddAction("move");
        m_actionMove.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        // Add the look actions to the action map.
        m_actionLook = m_actionMap.AddAction("look");
        m_actionLook.AddBinding("<Mouse>/position");

        m_actionShoot = m_actionMap.AddAction("shoot");
        m_actionShoot.AddBinding("<Mouse>/leftButton");
        m_actionShoot.started += ctx => { m_isShooting = true; };
        m_actionShoot.canceled += ctx => { m_isShooting = false; };

        m_actionMelee = m_actionMap.AddAction("melee");
        m_actionMelee.AddBinding("<Mouse>/rightButton");
        m_actionMelee.started += ctx => { MeleeAttack(); };

        m_actionShield = m_actionMap.AddAction("shield");
        m_actionShield.AddBinding("<Keyboard>/q");
        m_actionShield.started += ctx => { m_player.StartCoroutine(Shield()); };

        // Enable the action map.
        m_actionMap.Enable();
    }

    // Function that moves the player given a context.
    private void MovePlayer()
    {
        // Move the player and the camera.
        var input = m_actionMove.ReadValue<Vector2>();
        m_player.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(input.x, 0.0f, input.y) 
                                                               * m_player.moveSpeedModifier 
                                                               * Time.deltaTime 
                                                               * m_forceMultiplier, 
                                                               ForceMode.VelocityChange);
        animator.SetBool("isMoving", true);
        yTarget = Mathf.MoveTowards(yTarget, input.y, .05f);
        xTarget = Mathf.MoveTowards(xTarget, input.x, .05f);

        if(Mathf.Abs(input.x) < .1 && Mathf.Abs(input.y) < .1)
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void RotatePlayer()
    {
        // Calculate the rotation the player
        var mousePixelPos = m_actionLook.ReadValue<Vector2>();
        var mouseWorldPos = m_player.pCamera.ScreenToWorldPoint(new Vector3(mousePixelPos.x, mousePixelPos.y, m_player.pCamera.nearClipPlane));
        var forward = mouseWorldPos - m_player.gameObject.transform.position;
        var angle = Mathf.Atan2(forward.z, forward.x) * Mathf.Rad2Deg;

        // Set the forward vector and the rotation.
        m_player.forwardVector = new Vector3(forward.x, 0.0f, forward.z);
        m_player.gameObject.transform.localRotation = Quaternion.Euler(0, -angle, 0);
    }

    // Called from callback for shoot button.
    private void ShootBullet()
    {
        // Don't let them shoot mid melee
        if (m_player.isInAnimation) return;

        // Calculate the new angle/forward vector.
        var newForward = m_player.forwardVector.normalized + new Vector3(0.0f, 0.5f, 0.0f);
        var angle = Mathf.Atan2(newForward.z, newForward.x) * Mathf.Rad2Deg;

        // Calculate the bullet position.
        var bulletPos = m_player.transform.position + newForward;

        // Instantiate the prefab
        GameObject newBullet = GameObject.Instantiate(m_player.prefabBullet, bulletPos, Quaternion.Euler(0, -angle, 90));
        
        // Assign the forward vector.
        newBullet.GetComponent<Projectile>().direction = m_player.forwardVector.normalized;
    }


    // Instantiate the melee weapon prefab.
    private void MeleeAttack()
    {
        // Don't let them melee mid melee
        if(m_player.isInAnimation == true) return;

        GameObject.Instantiate(m_player.prefabMelee, m_player.transform);
        m_player.isInAnimation = true;
    }

    // Called from callback for shield button, is coroutine.
    private IEnumerator Shield()
    {
        // Don't let them shield mid animation or until cooldown is over.
        if (m_player.isInAnimation == true) yield break;
        if (m_cooldownShieldCurr < m_cooldownShieldMin) yield break;
        m_cooldownShieldCurr = 0.0f;
        animator.SetBool("isBlocking", true);
        // Instatiate the shield prefab.
        GameObject shield = GameObject.Instantiate(m_player.prefabShield, m_player.transform);

        // Don't let the player move or act for the duration.
        m_player.isInAnimation = true;
        m_player.moveSpeedModifier = 0.0f;

        // Wait until a certain time has passed, then destroy the shield.
        yield return new WaitForSeconds(m_cooldownShieldDur);


        // Destroy the shield
        GameObject.Destroy(shield);

        // Give the player control again.
        m_player.isInAnimation = false;
        m_player.moveSpeedModifier = 1.0f;
        animator.SetBool("isBlocking", false);
    }

    // Helper function to update the cooldown member variables.
    private void UpdateCooldowns(float dt)
    {
        m_cooldownShootCurr += dt;
        m_cooldownMeleeCurr += dt;
        m_cooldownShieldCurr += dt;
    }

}
