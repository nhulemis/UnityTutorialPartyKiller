using EZCameraShake;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Transform m_firePoint;

    [SerializeField] GameObject m_arrow1;
    [SerializeField] GameObject m_arrow2;
    [SerializeField] GameObject m_arrow3;

    [SerializeField] float m_refillRate = 1;

    int m_bulletReady;
    float m_nextTimeToRefill;

    // Start is called before the first frame update
    void Start()
    {
        m_arrow1.SetActive(false);
        m_arrow2.SetActive(false);
        m_arrow3.SetActive(false);

        m_bulletReady = 0;

        m_nextTimeToRefill = 1f / m_refillRate + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bulletReady <3 && Time.time > m_nextTimeToRefill)
        {
            m_bulletReady++;

            if (m_bulletReady == 1)
            {
                m_arrow1.SetActive(true);
            }
            else if (m_bulletReady == 2)
            {
                m_arrow2.SetActive(true);
            }
            else if (m_bulletReady == 3)
            {
                m_arrow3.SetActive(true);
            }

            m_nextTimeToRefill = 1f / m_refillRate + Time.time;
        }
    }

    //[Command]
    public void CmdShoot()
    {
         if (m_bulletReady <= 0 )
        {
            return;
        }

        GameObject bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        bullet.GetComponent<Bullet>().FirePoint = m_firePoint;

        if (m_bulletReady == 1)
        {
            m_arrow1.SetActive(false);
        }
        else if (m_bulletReady == 2)
        {
            m_arrow2.SetActive(false);
        }
        else if (m_bulletReady == 3)
        {
            m_arrow3.SetActive(false);
        }

        m_bulletReady--;
        m_nextTimeToRefill = 1f / m_refillRate + Time.time;
        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.5f, 0.35f);
    }
}
