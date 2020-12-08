using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monobehaviour attached to the player game object
// - Holds references to the non-monobehavior scripts.
// - Might be a good idea to have the player as a prefab?

public class Player : MonoBehaviour
{
    // Private references
    public PlayerInputs pInput;
    private GameManager gManager;

    // The camera that follows the player
    public Camera pCamera;

    // Prefabs
    public GameObject prefabBullet;
    public GameObject prefabMelee;
    public GameObject prefabShield;

    // Physics and movement variables
    public Vector3 forwardVector;
    public float moveSpeedModifier = 1.0f;      // Is the player sped up or slowed down?
    public float maxSpeed = 10.0f;              // Max velocity of the player.
    public bool isInAnimation = false;          // Don't let the player rotate (or move?) while doing an animation.

    // Health related things
    public float playerHealthBase;
    public float playerHealthCurr;
    public float playerHealthPer => (playerHealthCurr / playerHealthBase);
    private float m_IFramesCooldown = 0.3f;
    private float m_IFramesCurr = 0.0f;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate variables.
        pInput = new PlayerInputs(this, animator);
        forwardVector = gameObject.transform.right;
        
        playerHealthBase = 5;
        playerHealthCurr = playerHealthBase;

        // Get scene references.
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pInput.Update();
        m_IFramesCurr += Time.deltaTime;
    }

    // As the name suggests, deal damage to the player.
    public void TakeDamage(float damage)
    {
        if(m_IFramesCurr > m_IFramesCooldown)
        {
            playerHealthCurr -= damage;
            m_IFramesCurr = 0.0f;
        }

        // Player is dead, call game over.
        if(playerHealthCurr <= 0.01f)
        {
            GameManager.instance.GameOver();
        }
    }
}
