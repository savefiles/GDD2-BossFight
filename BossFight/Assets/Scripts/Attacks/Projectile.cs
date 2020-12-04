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
    public int damage = 10;                    // The amount of damage this projectile causes.

    private float currentLifeSpan = 0.0f;           // The amount of time this object has been alive.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.deltaTime;
        Move(dt);
        
        // Increase lifetime, check if this object needs to be destroyed
        currentLifeSpan += dt;
        if(currentLifeSpan > maxLifeSpan)
            DestroyThis();
    }

    // When this object enters another collider, act accordingly.
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        GameObject thisGameObject = gameObject;
        switch(other.gameObject.layer)
        {
            case GameManager.PLAYER_LAYER:
                other.GetComponent<Player>().TakeDamage(1.0f);
                DestroyThis();
                break;
            case GameManager.ENEMY_LAYER:
                other.gameObject.GetComponent<BossControl>().TakeDamage(damage);
                DestroyThis();
                break;
            case GameManager.SOLID_LAYER:
                if(other.GetComponent<Destroyable>() != null) other.GetComponent<Destroyable>().Hit(Destroyable.HitType.Projectile);
                DestroyThis();
                break;
        }
    }

    private void Move(float dt)
    {
        velocity += acceleration * dt;                                  // Speed up based on acceleration.
        gameObject.transform.position += direction * (velocity * dt);   // Move the object.
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
