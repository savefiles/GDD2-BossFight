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
    private float projSpeedBase = .045f;

    private float patternACoolBase;
    private float patternACoolCurr;

    private float patternBCoolBase;
    private float patternBCoolCurr;

    //  Health Variables
    public float bossHealthBase;
    public float bossHealthCurr;
    public float bossHealthPer => (bossHealthCurr / bossHealthBase);


    //  Player Variables
    private GameObject playerRef;

    private void Start() {
        projContainer = transform.parent.GetChild(1).gameObject;

        bossHealthBase = 150;
        bossHealthCurr = bossHealthBase;

        playerRef = GameObject.Find("Player").transform.GetChild(0).gameObject;
    }

    //  MainMethod - Update
    void FixedUpdate() {
        LifeCheck();

        switch (bossState) {
            case BossState.stateAmused:
                AttackPatternA(1, 12);
                AttackPatternB(1);
                break;

            case BossState.stateAnnoyed:
                AttackPatternA(1, 24);
                AttackPatternB(1);
                break;

            case BossState.stateAngry:
                AttackPatternA(2, 12);
                AttackPatternB(2);
                break;

            case BossState.stateFurious:
                AttackPatternA(2, 24);
                AttackPatternB(2);
                break;

            case BossState.stateRageMonster:
                AttackPatternA(3, 24);
                AttackPatternB(3);
                break;
        }
    }

    //  SubMethod of Update - Life Check
    private void LifeCheck() {
        //Debug.Log(bossState + ": " + bossHealthPer + "(" + bossHealthCurr + "/" + bossHealthBase + ")");

        //  Part - State Amused
        if (bossHealthPer >= 0.90f) {
            patternACoolBase = 3.0f;
            patternBCoolBase = 2;

            bossState = BossState.stateAmused;
        }

        //  Part - State Annoyed
        else if (bossHealthPer >= 0.70f) {
            patternACoolBase = 2.4f;
            patternBCoolBase = 1;

            //if (bossState == BossState.stateAmused) {
            //    patternACoolCurr = patternACoolBase;
            //    patternBCoolCurr = patternBCoolBase;
            //}

            bossState = BossState.stateAnnoyed;
        }

        //  Part - State Angry
        else if (bossHealthPer >= 0.50f) {
            patternACoolBase = 2;
            patternBCoolBase = 1.6f;

            //if (bossState == BossState.stateAnnoyed) {
            //    patternACoolCurr = patternACoolBase;
            //    patternBCoolCurr = patternBCoolBase;
            //}

            bossState = BossState.stateAngry;
        }

        //  Part - State Furious
        else if (bossHealthPer >= 0.30f) {
            patternACoolBase = 1.8f;
            patternBCoolBase = 1;

            //if (bossState == BossState.stateAngry) {
            //    patternACoolCurr = patternACoolBase;
            //    patternBCoolCurr = patternBCoolBase;
            //}

            bossState = BossState.stateFurious;
        }

        //  Part - State Rage Monster
        else {
            patternACoolBase = 1.8f;
            patternBCoolBase = 2;

            //if (bossState == BossState.stateFurious) {
            //    patternACoolCurr = patternACoolBase;
            //    patternBCoolCurr = patternBCoolBase;
            //}

            bossState = BossState.stateRageMonster;
        }
    }

    //  Methods - Attack Pattern A - Circle Attack
    private void AttackPatternA(int pType, float pNum) {
        if (patternACoolCurr <= 0) {
            patternACoolCurr = patternACoolBase;

            switch (pType) {
                //  Part - Single Round
                case 1:
                    AttackActualA(pNum, projSpeedBase);
                    break;

                //  Part - Double Round
                case 2:
                    AttackActualA(pNum, projSpeedBase);
                    AttackActualA(pNum, projSpeedBase * 0.9f);
                    break;

                //  Part - Triple Round
                case 3:
                    AttackActualA(pNum, projSpeedBase);
                    AttackActualA(pNum, projSpeedBase * 0.9f);
                    AttackActualA(pNum, projSpeedBase * 0.8f);
                    break;
            }
        }

        else {
            patternACoolCurr -= Time.deltaTime;
        }
    }

    private void AttackActualA(float pNum, float pSpeed) {
        float projAngle = 360 / pNum;

        for (int i = 0; i < pNum; i++) {
            temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
            temp.GetComponent<BossProjectile>().SetupProjectile(new Vector3(Mathf.Cos(i * projAngle), 0, Mathf.Sin(i * projAngle)), pSpeed, 1);
        }
    }

    //  Methods - Attack Pattern B - Spray Attack
    private void AttackPatternB(int pType) {
        if (patternBCoolCurr <= 0) {
            patternBCoolCurr = patternBCoolBase;

            switch (pType) {
                //  Part - Triple Fire
                case 1:
                    AttackActualB(3, 2 * projSpeedBase);
                    break;

                //  Part - Penta Fire
                case 2:
                    AttackActualB(5, 2 * projSpeedBase);
                    break;

                //  Part - Three Triple Fire
                case 3:
                    AttackActualB(3, 2 * projSpeedBase);
                    AttackActualB(3, 2 * projSpeedBase * 0.9f);
                    AttackActualB(3, 2 * projSpeedBase * 0.8f);
                    break;
            }
        }

        else {
            patternBCoolCurr -= Time.deltaTime;
        }
    }

    private void AttackActualB(int pNum, float pSpeed) {
        float angle = Mathf.Atan2(playerRef.transform.position.z - transform.position.z, playerRef.transform.position.x - transform.position.x);
        float startDiff = 0;

        switch (pNum) {
            //  Part - Triple Fire
            case 3:
                startDiff = -.1f;

                for (int i = 0; i < pNum; i++) {
                    temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
                    temp.GetComponent<BossProjectile>().SetupProjectile(new Vector3(Mathf.Cos(angle + startDiff + (.1f * i)), 0, Mathf.Sin(angle + startDiff + (.1f * i))), pSpeed, 1);
                }
                break;

            //  Part - Penta Fire
            case 5:
                startDiff = -.2f;

                for (int i = 0; i < pNum; i++) {
                    temp = Instantiate(projPrefab, transform.position, Quaternion.identity, projContainer.transform);
                    temp.GetComponent<BossProjectile>().SetupProjectile(new Vector3(Mathf.Cos(angle + startDiff + (.1f * i)), 0, Mathf.Sin(angle + startDiff + (.1f * i))), pSpeed, 1);
                }
                break;
        }
    }

    //  MainMethod - Take Damage (param Damage)
    public void TakeDamage(int pDamage) {
        bossHealthCurr -= pDamage;

        if (bossHealthCurr <= 0.01f) {
            GameManager.instance.GameWon();
        }
    }
}
