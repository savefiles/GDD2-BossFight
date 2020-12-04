using UnityEngine;

public class BossProjectile : MonoBehaviour {
    //  Combat Variables
    private int projDamage;

    //  Move Variables
    private Vector3 projVect;
    private float projSpeed;

    private float projHealth;

    //  MainMethod - Setup Projectile
    public void SetupProjectile(Vector3 pVect, float pSpeed, int pDamage) {
        projVect = pVect;

        projHealth = 10;
        projSpeed = pSpeed;

        projDamage = pDamage;
    }

    //  MainMethod - Update
    void FixedUpdate() {
        if (projHealth > 0) {
            projHealth -= Time.deltaTime;
            MoveControl();
        }

        else {
            Destroy(transform.gameObject);
        }
    }

    //  SubMethod of Update - Move Control
    private void MoveControl() {
        transform.position += new Vector3(projVect.x * projSpeed, 0, projVect.z * projSpeed);
    }

    public void OnTriggerEnter(Collider other) {
        // If it collides with wall or shield, destroy.
        if (other.gameObject.layer == GameManager.SOLID_LAYER)
        {
            Destroy(transform.gameObject);
        }
        if (other.transform.tag == "Player") {
            other.transform.GetComponent<Player>().TakeDamage(projDamage);
            Destroy(transform.gameObject);
        }
    }
}
