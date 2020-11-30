using UnityEngine;

public enum BossState {
    stateAmused,
    stateAnnoyed,
    stateAngry,
    stateFurious,
    stateRageMonster
}

public enum FireRate {
    stateFew,
    stateMed,
    stateLots
}

public enum FireSpeed {
    stateSlow,
    stateMed,
    stateFast
}

public class BossControl : MonoBehaviour {
    //  ~Boss Variables
    private BossState bossState;

    //  Attack Variables
    private GameObject projContainer;
    public GameObject projPrefab;
    private GameObject temp;

    //  Attack Pattern Variables
    private float projSpeedBase = .025f;

    private float patternACoolBase;
    private float patternACoolCurr;
    private float patternASpeed;

    private int patternANum = 24;

    private float patternBCoolBase;
    private float patternBCoolCurr;
    private float patternBSpeed;

    //  Health Variables
    private static int bossHealthBase;
    public static int bossHealthCurr;
    public static float bossHealthPer => (bossHealthCurr / bossHealthBase) * 100;


    //  Player Variables
    private GameObject playerRef;

    private void Start() {
        projContainer = transform.parent.GetChild(1).gameObject;

        bossHealthBase = 100;
        bossHealthCurr = bossHealthBase;

        playerRef = GameObject.Find("Player").transform.GetChild(0).gameObject;
    }

    //  MainMethod - Update
    void Update() {
        LifeCheck();

        switch (bossState) {
            case BossState.stateAmused:
                AttackPatternA();
                AttackPatternB();
                break;

            case BossState.stateAnnoyed:
                AttackPatternA();
                AttackPatternB();
                break;

            case BossState.stateAngry:
                AttackPatternA();
                AttackPatternB();
                break;

            case BossState.stateFurious:
                AttackPatternA();
                AttackPatternB();
                break;

            case BossState.stateRageMonster:
                AttackPatternA();
                AttackPatternB();
                break;
        }
    }

    //  SubMethod of Update - Life Check
    private void LifeCheck() {
        //Debug.Log(bossState + ": " + bossHealthPer + "(" + bossHealthCurr + "/" + bossHealthBase + ")");

        //  Part - State Amused
        if (bossHealthPer >= 90) {
            bossState = BossState.stateAmused;

            patternACoolBase = 3;
            patternASpeed = 1 * projSpeedBase;

            patternBCoolBase = 3;
            patternBSpeed = 1 * projSpeedBase;
        }

        //  Part - State Annoyed
        else if (bossHealthPer >= 70) {
            bossState = BossState.stateAnnoyed;

            patternACoolBase = 2;
            patternASpeed = 1.5f * projSpeedBase;

            patternBCoolBase = 3;
            patternBSpeed = 1.5f * projSpeedBase;
        }

        //  Part - State Angry
        else if (bossHealthPer >= 50) {
            bossState = BossState.stateAngry;

            patternACoolBase = 2;
            patternASpeed = 1.5f * projSpeedBase;

            patternBCoolBase = 3;
            patternBSpeed = 2 * projSpeedBase;
        }

        //  Part - State Furious
        else if (bossHealthPer >= 30) {
            bossState = BossState.stateFurious;

            patternACoolBase = 1;
            patternASpeed = 1.5f * projSpeedBase;

            patternBCoolBase = 2;
            patternBSpeed = 2 * projSpeedBase;
        }

        //  Part - State Rage Monster
        else if (bossHealthPer <= 10) {
            bossState = BossState.stateRageMonster;

            patternACoolBase = 1;
            patternASpeed = 2 * projSpeedBase;

            patternBCoolBase = 1;
            patternBSpeed = 2 * projSpeedBase;
        }
    }

    //  SubMethod of Update - Attack Pattern A - Circle Attack
    private void AttackPatternA() {
        if (patternACoolCurr <= 0) {
            patternACoolCurr = patternACoolBase;

            float projAngle = 360 / patternANum;

            for (int i = 0; i < patternANum; i++) {
                temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
                temp.GetComponent<BossProjectile>().SetupProjectile(new Vector3(Mathf.Cos(i * projAngle), 0, Mathf.Sin(i * projAngle)), patternASpeed, 1);
            }
        }

        else {
            patternACoolCurr -= Time.deltaTime;
        }
    }

    //  SubMethod of Update - Attack Pattern B - Spray Attack
    private void AttackPatternB() {
        if (patternBCoolCurr <= 0) {
            patternBCoolCurr = patternBCoolBase;

            float angle = Mathf.Atan2(playerRef.transform.position.z - transform.position.z, playerRef.transform.position.x - transform.position.x);

            //  Part - S Projectile
            temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
            temp.GetComponent<BossProjectile>().SetupProjectile(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)), patternBSpeed, 1);

            //  Part - SSE Projectile
            temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
            temp.GetComponent<BossProjectile>().SetupProjectile(new Vector3(Mathf.Cos(angle - .1f), 0, Mathf.Sin(angle - .1f)), patternBSpeed, 1);

            //  Part - SSW Projectile
            temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
            temp.GetComponent<BossProjectile>().SetupProjectile(new Vector3(Mathf.Cos(angle + .1f), 0, Mathf.Sin(angle + .1f)), patternBSpeed, 1);
        }

        else {
            patternBCoolCurr -= Time.deltaTime;
        }
    }

    //  MainMethod - Take Damage (param Damage)
    public void TakeDamage(int pDamage) {
        bossHealthCurr -= pDamage;
    }
}
