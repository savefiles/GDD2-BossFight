using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monobehaviour attached to the player game object
// - Holds references to the non-monobehavior scripts.
// - Might be a good idea to have the player as a prefab?

public class UserInterface : MonoBehaviour
{
    // References
    GameManager gManager;

    // Health related things
    public float health = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get scene references.
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}