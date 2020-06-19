using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float projectileSpeed;
    public float time;
    public float distance;
    public float damage;
    public float fireRate;

    Rigidbody2D rb;
    float initialTime;
    Vector3 initialPos;

    AudioSource soundEffect;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        soundEffect = GetComponent<AudioSource>();
    }

    void Start()
    {
        initialPos = transform.position;
        initialTime = Time.time;
        rb.velocity = transform.up * projectileSpeed;
        soundEffect.Play();
    }

    void FixedUpdate()
    {
        Vector3 dist = (transform.position - initialPos);
        if (dist.magnitude >= distance || Time.time - initialTime >= time)
        {
            Destroy(this.gameObject);
        }
    }

    public void refreshSpeed()
    {
        rb.velocity = transform.up * projectileSpeed;
    }
}
