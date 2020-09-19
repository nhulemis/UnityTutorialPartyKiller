using EZCameraShake;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public Transform FirePoint { get; set; }

    public GameObject m_explosionPrefab;

    [SerializeField] float m_speed = 30f;

    AudioSource shootFx;

    int bounceCount = 0;
    float lTimeBounce;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(FirePoint.forward * 1.5f * m_speed, ForceMode.VelocityChange);
        shootFx = GetComponent<AudioSource>();
        lTimeBounce = 0;
        shootFx.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerA")
        {
            this.Collision();
        }
        else
        {
            if (Time.time > lTimeBounce + 1f)
            {
                if (bounceCount == 4)
                {
                    this.Collision();
                }
                else
                {
                    lTimeBounce = Time.deltaTime;
                    bounceCount++;
                    Debug.Log("count :" + bounceCount);
                }
            }
        }
        // kill player
    }

    private void Collision()
    {
        GameObject exp = Instantiate(m_explosionPrefab, this.transform.position, this.transform.rotation);

        Destroy(exp, 10);

        Destroy(gameObject);

        CameraShaker.Instance.ShakeOnce(3f, 2f, 0.5f, 0.35f);

    }
}
