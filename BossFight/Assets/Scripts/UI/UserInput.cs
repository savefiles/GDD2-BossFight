using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monobehaviour attached to the player game object
// - Holds references to the non-monobehavior scripts.
// - Might be a good idea to have the player as a prefab?

public class Player : MonoBehaviour
{
    // Private references
    private PlayerInput pInput;
    private PlayerManager pManager;
    private GameManager gManager;

    // The camera that follows the player
    public Camera pCamera;

    // Prefabs
    public GameObject prefabBullet;
    public GameObject prefabMelee;

    // Physics and movement variables
    public Vector3 forwardVector;
    public float moveSpeed = 10.0f;
    public bool isInAnimation = false;          // Don't let the player rotate (or move?) while doing an animation.

    // Health related things
    public float health = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate variables.
        pInput = new PlayerInput(this);
        pManager = new PlayerManager();
        forwardVector = gameObject.transform.right;

        // Get scene references.
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        pInput.Update();
    }
}