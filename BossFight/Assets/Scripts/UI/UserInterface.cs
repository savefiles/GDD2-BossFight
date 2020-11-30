using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monobehaviour attached to the player game object
// - Holds references to the non-monobehavior scripts.
// - Might be a good idea to have the player as a prefab?

public class UserInterface : MonoBehaviour
{
    // References
    private GameManager gManager;
    private GameObject player;
    private GameObject boss;

    // Health related things
    private GameObject phbar;
    private float iPhealth;
    private float cPhealth;
    public float phBarPer;
    private GameObject psbar;
    private float iPshield;
    private float cPshield;
    public float psBarPer;
    private GameObject bhbar;
    private float iBhealth;
    private float cBhealth;
    public float bhBarPer;


    // Start is called before the first frame update
    void Start()
    {
        phbar = GameObject.Find("PHBar");
        psbar = GameObject.Find("PSBar");
        bhbar = GameObject.Find("BHBar");
        player = GameObject.Find("default");
        boss = GameObject.Find("BossActual");
        Debug.Log(iBhealth);
    }

    // Update is called once per frame
    void Update()
    {
        phBarPer = player.GetComponent<Player>().playerHealthPer;
        phbar.transform.localScale = new Vector3(phBarPer, 1f, 1f);
        //psbar.transform.localScale = new Vector3(psBarPer, 1f, 1f);
        bhBarPer = boss.GetComponent<BossControl>().bossHealthPer;
        bhbar.transform.localScale = new Vector3(bhBarPer, 1f, 1f);
    }
}