using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    // The ways to destroy the object.
    public bool byShooting = false;
    public bool byMelee = false;

    // Information about the object.
    private float health = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(HitType hitType)
    {
        if (hitType == HitType.Projectile && byShooting)
            health -= 4.0f;
        if (hitType == HitType.Melee && byMelee)
            health -= 11.0f;
        if (health <= 0.0f)
            Destroy(gameObject);
    }

    public enum HitType { Melee, Projectile }
}
