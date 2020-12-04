using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBars : MonoBehaviour
{
    // References
    public GameObject m_player;
    public GameObject m_barHealthPlayer;
    public GameObject m_barCooldownPlayer;

    // Scaling variables
    private Vector3 m_scaleInitHealthPlayer;
    private Vector3 m_scaleInitCooldownPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the current scales of each bar.
        m_scaleInitHealthPlayer = m_barHealthPlayer.transform.localScale;
        m_scaleInitCooldownPlayer = m_barCooldownPlayer.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
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
