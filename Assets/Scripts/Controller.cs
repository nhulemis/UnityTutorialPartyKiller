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

    float statusTime;

    public bool isBot = false;

    enum Status
    { 
        RUN,
        SHOOT,
        NONE
    }

    float x, y;

    Status status = Status.NONE;


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
        //if (isLocalPlayer)
        //    ApplyInput();

        if (!isBot)
        {
            float h = Input.GetAxisRaw(HORIZONTAL);
            float v = Input.GetAxisRaw(VERTICAL);
            ApplyInput(h,v); // player
        }
        //else
        //    BotBehavior(); // Bot
    }

    private void RandomStatusBot()
    {
        statusTime = UnityEngine.Random.Range(5, 11) / 10;
        int percent = UnityEngine.Random.Range(0, 101);

        if (percent <60)
        {
            status = Status.RUN;
        }
        else if (percent <90)
        {
            status = Status.SHOOT;
        }
        else
        {
            status = Status.NONE;
        }
        x = UnityEngine.Random.Range(-11, 11) / 10;
        y = UnityEngine.Random.Range(-11, 11) / 10;
    }

    private void BotBehavior()
    {
        if (statusTime <= 0)
        {
            RandomStatusBot();
        }
        else
        {
            statusTime -= Time.deltaTime;

            switch (status)
            {
                case Status.RUN:
                    ApplyInput(y, x);
                    break;
                case Status.SHOOT:
                    m_wp.Shoot();
                    statusTime= 0;
                    break;
                case Status.NONE:
                    RandomStatusBot();
                    break;
                default:
                    break;
            }
        }


    }

    private void ApplyInput(float h , float v)
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Riser" && isBot ) 
        {
            Debug.Log("dao chieu");
            x *= -1;
            y *= -1;
        }
    }
}
