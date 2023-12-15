using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float lifetime = 3f;
    public float moveSpeed;

    public Rigidbody2D theRB;
    public int damageAmount = 1;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = -transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")

        {

            PlayerHealthController.instance.DamagePlayer();
                Destroy(gameObject);

        }
        /*
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            
        }
        */

    }



}
