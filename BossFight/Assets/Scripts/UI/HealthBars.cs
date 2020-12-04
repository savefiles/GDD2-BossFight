using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBars : MonoBehaviour
{
    // References
    public GameObject m_player;
    public GameObject m_boss;
    public GameObject m_barHealthPlayer;
    public GameObject m_barCooldownPlayer;
    public GameObject m_barHealthBoss;

    // Scaling variables.
    private Vector3 m_scaleInitHealthPlayer;
    private Vector3 m_scaleInitCooldownPlayer;
    private Vector3 m_scaleInitHealthBoss;

    // Start is called before the first frame update
    void Start()
    {
        // Get the current scales of each bar.
        m_scaleInitHealthPlayer = m_barHealthPlayer.transform.localScale;
        m_scaleInitCooldownPlayer = m_barCooldownPlayer.transform.localScale;
        m_scaleInitHealthBoss = m_barHealthBoss.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float bossHealthPercent = m_boss.GetComponent<BossControl>().bossHealthPer;
        m_barHealthBoss.transform.localScale = new Vector3(m_scaleInitHealthBoss.x * bossHealthPercent,
                                                           m_scaleInitHealthBoss.y,
                                                           m_scaleInitHealthBoss.z);

        float playerHealthPercent = m_player.GetComponent<Player>().playerHealthPer;
        m_barHealthPlayer.transform.localScale = new Vector3(m_scaleInitHealthPlayer.x * playerHealthPercent,
                                                             m_scaleInitHealthPlayer.y,
                                                             m_scaleInitHealthPlayer.z);

        float playerCooldownPercent = m_player.GetComponent<Player>().pInput.ShieldCooldown;
        m_barCooldownPlayer.transform.localScale = new Vector3(m_scaleInitCooldownPlayer.x * playerCooldownPercent,
                                                               m_scaleInitCooldownPlayer.y,
                                                               m_scaleInitCooldownPlayer.z);
    }
}
