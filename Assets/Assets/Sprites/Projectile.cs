using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject particle;
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 velDir = transform.InverseTransformDirection(rb.velocity);
            rb.AddForce(new Vector2 (velDir.x * 3, 1));

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.healthSystem.Damage(15);

        }
        else
        {
            GameObject effect = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(effect, 1.5f);
            Destroy(this.gameObject);
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
