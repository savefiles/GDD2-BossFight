using UnityEngine;

public enum BossState {
    stateAmused,
    stateAnnoyed,
    stateAngry,
    stateFurious,
    stateRageMonster
}

public class BossControl : MonoBehaviour {
    //  Attack Variables
    private GameObject projContainer;
    public GameObject projPrefab;

    //  Attack Pattern Variables
    private float patternACoolBase;
    private float patternACoolCurr;

    private float patternBCoolBase;
    private float patternBCoolCurr;

    //  Health Variables
    private int bossHealthBase;
    private int bossHealthCurr;

    void Start() {
        projContainer = transform.parent.GetChild(1).gameObject;

        patternACoolBase = 3;
        patternACoolCurr = patternACoolBase;

        patternBCoolBase = 2;
        patternBCoolCurr = patternBCoolBase;
    }

    void Update() {
        AttackControlA();
        AttackControlB();
    }

    //  SubMethod of Update - Attack Control A
    private void AttackControlA() {
        if (patternACoolCurr <= 0) {
            AttackPatternA();
            patternACoolCurr = patternACoolBase;
        }

        else {
            patternACoolCurr -= Time.deltaTime;
        }
    }

    //  SubMethod of AttackControlA - Attack Pattern A (Circle Fire)
    private void AttackPatternA() {
        GameObject temp;
        //  Part - S Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(0, 0, 1));

        //  Part - SE Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(.5f, 0, .5f));

        //  Part - E Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(1, 0, 0));

        //  Part - NE Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(.5f, 0, -.5f));

        //  Part - N Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(0, 0, -1));

        //  Part - NW Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(-.5f, 0, -.5f));

        //  Part - W Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(-1, 0, 0));

        //  Part - SW Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(-.5f, 0, .5f));
    }

    //  SubMethod of Update - Attack Control B
    private void AttackControlB() {
        if (patternBCoolCurr <= 0) {
            AttackPatternB();
            patternBCoolCurr = patternBCoolBase;
        }

        else {
            patternBCoolCurr -= Time.deltaTime;
        }
    }

    //  SubMethod of AttackControlB - Attack Pattern B (Straight Fire)
    private void AttackPatternB() {
        //  Spawn Projectiles
        GameObject temp;
        //  Part - S Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(0, 0, 1));

        //  Part - SSE Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(.25f, 0, .75f));

        //  Part - SSW Projectile
        temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
        temp.GetComponent<BossProjectile>().SetupProjectile(ProjectileType.typeProjA, new Vector3(-.25f, 0, .75f));
    }
}
