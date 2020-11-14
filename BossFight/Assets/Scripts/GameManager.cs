using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Big monobehavior for the game. Get using the find method.

public class GameManager : MonoBehaviour
{
    // Simple simpleton instance.
    public static GameManager instance;

    // Public layers.
    public const int SOLID_LAYER = 9;
    public const int PLAYER_LAYER = 10;
    public const int ENEMY_LAYER = 11;





    // Start is called before the first frame update
    void Start()
    {
        // Make sure there's only one instance of this class.
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem.Update();

    }
}
