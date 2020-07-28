using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public Transform FirePoint { get; set; }

    [SerializeField] float m_speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(FirePoint.forward * 1.5f * m_speed , ForceMode.VelocityChange);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
