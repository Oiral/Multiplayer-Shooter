using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public GameObject bulletHitParticle;
    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<PlayerHealthScript>();

        if (health != null)
        {
            health.TakeDamage(10);
        }

        Destroy(gameObject);
        Instantiate(bulletHitParticle, collision.contacts[0].point, Quaternion.identity);
    }
}
