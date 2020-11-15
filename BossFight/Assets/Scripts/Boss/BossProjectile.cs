using UnityEngine;

public enum ProjectileType {
    typeProjA,
}

public class BossProjectile : MonoBehaviour {
    //  Combat Variables
    private int projDamage;

    //  Move Variables
    private Vector3 projVect;
    private float projSpeed;

    private float projHealth;
    public float ProjHealth => projHealth;

    void Start() {
    }

    public void SetupProjectile(ProjectileType pType, Vector3 pVect) {
        projVect = pVect;
        projHealth = 5;

        switch (pType) {
            case ProjectileType.typeProjA:
                projDamage = 1;
                projSpeed = .01f;
                break;
        }
    }

    void Update() {
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
}
