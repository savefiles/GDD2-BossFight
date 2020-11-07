using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Class that deals with the inputs regarding the player
// - Movement, attacks, etc.
public class PlayerInput
{
    // The player that this input is attached to.
    Player m_player;

    // The keyboard that is currently being used (might not be necessary)
    Keyboard m_keyboard;

    // The controls for the different actions a player can take.
    InputActionMap m_actionMap;
    InputAction m_actionMove;
    InputAction m_actionLook;
    InputAction m_actionShoot;

    public PlayerInput(Player player)
    {
        m_player = player;
        m_keyboard = Keyboard.current;
        CreateInputMap();
    }

    public void Update()
    {
        MovePlayer();
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
        m_actionShoot.started += ctx => { ShootBullet(); };

        // Enable the action map.
        m_actionMap.Enable();
    }

    // Function that moves the player given a context.
    private void MovePlayer()
    {
        // Move the player and the camera.
        var input = m_actionMove.ReadValue<Vector2>();
        m_player.gameObject.transform.position += new Vector3(input.x, 0.0f, input.y) * m_player.moveSpeed * Time.deltaTime;
        m_player.pCamera.transform.position += new Vector3(input.x, 0.0f, input.y) * m_player.moveSpeed * Time.deltaTime;

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
        // Calculate the new angle/forward vector.
        var newForward = m_player.forwardVector.normalized;
        var angle = Mathf.Atan2(newForward.z, newForward.x) * Mathf.Rad2Deg;

        // Instantiate the prefab
        GameObject newBullet = GameObject.Instantiate(m_player.prefabBullet, m_player.transform.position, Quaternion.Euler(0, -angle, 90));
        
        // Assign the forward vector.
        newBullet.GetComponent<Projectile>().direction = m_player.forwardVector.normalized;
    }

}
