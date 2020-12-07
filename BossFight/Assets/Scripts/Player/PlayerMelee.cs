using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The player's melee attack, using an invisible game object for detecting hits.


public class PlayerMelee : MonoBehaviour
{
    // The enemy layer.
    int m_ilayerMask = 1 << 11;

    Transform m_player;

    // Properties of the swing
    float m_fTotalRotation = 90.0f;         // Degrees
    float m_fTotalSwingTime = 0.5f;         // Seconds

    // Damage related vars
    int m_iDamage = 10;

    // Calculated variables
    float m_fMiddleAngle;
    float m_fStartAngle;
    float m_fEndAngle;
    float m_fTimeSinceSpawn;
    float m_fNonSwingPhaseTime;

    // Only hit the boss once.
    bool m_hasHitBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player (parent to this object).
        m_player = gameObject.transform.parent;

        // Slighly change the rotation of the player, so that they end up swinging with the center at current rotation.
        m_fMiddleAngle = m_player.transform.eulerAngles.y;
        m_fStartAngle = m_fMiddleAngle + m_fTotalRotation / 2.0f;
        m_fEndAngle = m_fMiddleAngle - m_fTotalRotation / 2.0f;

        // The duration of the swing.
        m_fTimeSinceSpawn = 0.0f;
        m_fNonSwingPhaseTime = m_fTotalSwingTime / 10.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle;
        m_fTimeSinceSpawn += Time.deltaTime;
        
        // If the swing is done, destroy this game object.
        if(m_fTimeSinceSpawn > m_fTotalSwingTime)
        {
            m_player.GetComponent<Player>().isInAnimation = false;
            Destroy(gameObject);
        }

        
        // Current time since spawn determines phase of the swing.
        // - Windup phase
        if(m_fTimeSinceSpawn < m_fNonSwingPhaseTime)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            angle = Mathf.Lerp(m_fMiddleAngle, m_fStartAngle, m_fTimeSinceSpawn / m_fNonSwingPhaseTime);
            RotatePlayer(angle);
            return;
        }
        // - Winddown phase
        if (m_fTimeSinceSpawn > m_fTotalSwingTime - m_fNonSwingPhaseTime)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            angle = Mathf.Lerp(m_fEndAngle, m_fMiddleAngle, 1.0f + ((m_fTimeSinceSpawn - m_fTotalSwingTime) / m_fNonSwingPhaseTime));
            RotatePlayer(angle);
            return;
        }
        // - Swing phase
        // Raycast, do the swing.
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        angle = Mathf.Lerp(m_fStartAngle, m_fEndAngle, (m_fTimeSinceSpawn - m_fNonSwingPhaseTime) / (m_fTotalSwingTime - 2.0f * m_fNonSwingPhaseTime));
        RotatePlayer(angle);
    }

    private void RotatePlayer(float angle)
    {
        m_player.rotation = Quaternion.Euler(0, angle, 0);
    }

    // When this object enters another collider, act accordingly.
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        switch (other.gameObject.layer)
        {
            case GameManager.ENEMY_LAYER:
                if(m_hasHitBoss) return;
                other.gameObject.GetComponent<BossControl>().TakeDamage(m_iDamage);
                m_hasHitBoss = true;
                break;
            case GameManager.SOLID_LAYER:
                if(other.GetComponent<Destroyable>() != null) other.GetComponent<Destroyable>().Hit(Destroyable.HitType.Melee);
                break;
        }

    }
}
