using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBars : MonoBehaviour
{
    // References
    public GameObject m_boss;
    public List<GameObject> m_barsHealthBoss;   // [0] is the first health bar to break.

    // Scaling variables.
    private Vector3 m_scaleInitHealthBoss;

    // Thresholds and lengths of phases.
    List<float> m_bossThresholds = new List<float> { 1.00f, 0.90f, 0.70f, 0.50f, 0.30f };
    static int c = 0; // Current phase

    // Start is called before the first frame update
    void Start()
    {
        // Get the current scales of each bar.
        m_scaleInitHealthBoss = m_barsHealthBoss[0].transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        float bossHealthPercent = m_boss.GetComponent<BossControl>().bossHealthPer;
        
        // Determine the health bar to scale.
        for(int i = m_bossThresholds.Count - 1; i >= 0; i--)
        {
            if (bossHealthPercent <= m_bossThresholds[i])
            {
                // Phase change, set last health bar to 0
                if(c != i)
                    m_barsHealthBoss[c].transform.localScale = new Vector3(0.0f, m_scaleInitHealthBoss.y, m_scaleInitHealthBoss.z);
                c = i; 
                break;
            }
        }

        // Last value has ternary operator
        float bossHealthScalar = 1 - (m_bossThresholds[c] - bossHealthPercent) / (m_bossThresholds[c] - (c != m_bossThresholds.Count - 1 ? m_bossThresholds[c + 1] : 0.0f));
        bossHealthScalar = Mathf.Max(bossHealthScalar, 0.0f);
        m_barsHealthBoss[c].transform.localScale = new Vector3(m_scaleInitHealthBoss.x * bossHealthScalar,
                                                           m_scaleInitHealthBoss.y,
                                                           m_scaleInitHealthBoss.z);


    }
}
