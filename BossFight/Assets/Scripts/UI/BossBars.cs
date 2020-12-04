using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBars : MonoBehaviour
{
    // References
    public GameObject m_boss;
    public GameObject m_barHealthBoss;

    // Scaling variables.
    private Vector3 m_scaleInitHealthBoss;

    // Start is called before the first frame update
    void Start()
    {
        // Get the current scales of each bar.
        m_scaleInitHealthBoss = m_barHealthBoss.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float bossHealthPercent = m_boss.GetComponent<BossControl>().bossHealthPer;
        m_barHealthBoss.transform.localScale = new Vector3(m_scaleInitHealthBoss.x * bossHealthPercent,
                                                           m_scaleInitHealthBoss.y,
                                                           m_scaleInitHealthBoss.z);


    }
}
