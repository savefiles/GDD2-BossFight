using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Vector3 direction;
    public GameObject projectilePrefab;
    // Seconds between spawns
    public float timeBetweenSpawns;

    private float timeSinceSpawn = 0.0f;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {

        // Normalize direction.
        direction = direction.normalized;

        // Get the angle that this bullet will spawn at.
        angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;


    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if(timeSinceSpawn >= timeBetweenSpawns)
        {
            SpawnBullet();
            timeSinceSpawn = 0.0f;
        }
    }

    private void SpawnBullet()
    {
        // Instantiate the prefab
        GameObject newBullet = GameObject.Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.Euler(0, -angle, 90));

        // Assign the forward vector.
        newBullet.GetComponent<Projectile>().direction = direction;
    }
}
