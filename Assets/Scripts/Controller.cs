using System;
using System.Collections;
using UnityEngine;
using Mirror;

public class Controller : NetworkBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    [SerializeField]
    float m_speed = 10f;

    Rigidbody m_rb;

    Weapon m_wp;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_wp = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Time.deltaTime;
        if (isLocalPlayer)
            ApplyInput();
    }

    private void ApplyInput()
    {
        float h = Input.GetAxisRaw(HORIZONTAL);
        float v = Input.GetAxisRaw(VERTICAL);

        var dir = new Vector3(h, .0f, v);

        dir = dir.normalized;

        m_rb.MovePosition(m_rb.position + m_speed * Time.fixedDeltaTime * dir);
        if (h != 0 || v != 0)
        {
            m_rb.rotation = Quaternion.LookRotation(dir);
        }

        if (Input.GetKeyUp("space"))
        {
            m_wp.Shoot();
        }

        if (Input.GetKeyUp("n"))
        {
            KilledPlayer();
        }
    }

    public void KilledPlayer()
    {
        GameManager.Instance.IsEndGame = true;
    }
}
