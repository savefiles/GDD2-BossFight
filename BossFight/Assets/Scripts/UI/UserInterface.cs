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
    private static float phealth;
    private static float Iphealth;
    private static int bhealth;
    public float pHBarWidth;
    public float bHBarWidth;
    public int pHBarPer;
    public int bHBarPer;
    public float IpHBarWidth;
    public float IbHBarWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Get scene references.
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        phealth = player.Health;
        Iphealth = player.Health;
        boss = GameObject.Find("Boss");
        bhealth = boss.ssHealthPer;
        pHBarWidth = PlayerHealthBarValue;
        IpHBarWidth = PlayerHealthBarValue;
        bHBarWidth = BossHealthBarValue;
        IbHBarWidth = BossHealthBarValue;
    }

    // Update is called once per frame
    void Update()
    {
        phealth = player.Health;
        bhealth = boss.ssHealthPer;

        bHBarWidth = IbHBarWidth * bhealth;
        pHBarWidth = IpHBarWidth * ((phealth / Iphealth) * 100);
    }
}