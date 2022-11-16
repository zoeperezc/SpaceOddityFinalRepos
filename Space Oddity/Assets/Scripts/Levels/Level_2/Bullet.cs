using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;

    public float speed;

    private void Start()
    {
        Destroy(this.gameObject,4.0f);
    }

    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("InvaderL2"))
        {
            other.gameObject.GetComponent<BlueDamage>().life -= 1;
            Destroy(gameObject);
            GameManager.GM.BulletColl(this);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            Destroy(gameObject);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("InvaderL3"))
        {
            Destroy(gameObject);
            GameManager.GM.BulletColl(this);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            Destroy(gameObject);
            GameManager.GM.BulletColl(this);
        }
    }
}