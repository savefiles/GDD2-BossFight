using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Physics stuff (should be set by the prefab)
    public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
    public float velocity = 10.0f;
    public float acceleration = 0.0f;
    public float maxLifeSpan = 5.0f;                // Max amount of time before this bullet is destroyed.

    private float currentLifeSpan = 0.0f;           // The amount of time this object has been alive.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        Move(dt);
        
        // Increase lifetime, check if this object needs to be destroyed
        currentLifeSpan += dt;
        if(currentLifeSpan > maxLifeSpan)
            Destroy(gameObject);
    }

    private void Move(float dt)
    {
        velocity += acceleration * dt;                                  // Speed up based on acceleration.
        gameObject.transform.position += direction * (velocity * dt);   // Move the object.
    }
}
