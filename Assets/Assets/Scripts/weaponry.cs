using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets._2D;

public class weaponry : MonoBehaviour
{
 
    public Transform Firepoint;
    public GameObject impactEffect;
    public LineRenderer linerenderer;
    public float delay = 0f;
    public int knockback = 2;
    [SerializeField] private int offset = -90;
    public int restrictAngle;
    public CameraShake shake;

    //private PlayerController2D controller;

    private Animator animator;

    public int flight;

    public float attackRange;

    public GameObject projectile;

    public Rigidbody2D rb;

    private bool canStabAgain = true;

    private bool canShootAgain = true;

    public delegate void Del();

    private Del resetBowTimer;


    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        //controller = GetComponent<PlayerController2D>();
    }

    private void Start()
    {
        resetBowTimer = () => { canShootAgain = true; };
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.T))
        {
            if(delay <= 0)
            {
                StartCoroutine(shoot());
            }
        }
    }
    IEnumerator shoot()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(Firepoint.position, Firepoint.right);
        delay = 1f;
        if (hitinfo)
        {
            Rigidbody2D rb = hitinfo.transform.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                Health health = rb.GetComponent<Health>();
                StartCoroutine (shake.shake(0.15f, 0.1f));
                if (rb.transform.position.x >= Firepoint.position.x)
                {
                    rb.velocity = new Vector2(knockback, knockback);
                    
                }
                else
                {
                    rb.velocity = new Vector2(-knockback, knockback);
                }
                health.takeDamage(5);
            }
            GameObject particle =  Instantiate(impactEffect, hitinfo.point, Quaternion.identity);
            Destroy(particle, 0.6f);
            linerenderer.SetPosition(0, Firepoint.position);
            linerenderer.SetPosition(1, hitinfo.point);
        }
        else
        {
            linerenderer.SetPosition(0, Firepoint.position);
            linerenderer.SetPosition(1, Firepoint.position + Firepoint.right * 100);
        }
        linerenderer.enabled = true;

        yield return new WaitForSeconds(0.2f);

        linerenderer.enabled = false;

        yield return new WaitForSeconds(1f);
        delay--;
    }

    public IEnumerator shoot2()
    {
        if (canShootAgain == true)
        {
            canShootAgain = false;
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //player is looking to the right
            if (transform.rotation.y == 0)
            {
                if (Mathf.Abs(rotZ) >= restrictAngle)
                {
                    if (rotZ > 0)
                    {
                        rotZ = restrictAngle;
                    }
                    else
                    {
                        rotZ = -restrictAngle;
                    }
                }

            }
            else //if player is looking to the left
            {
                if (Mathf.Abs(rotZ) <= 180 - restrictAngle)
                {
                    if (rotZ > 0)
                    {
                        rotZ = 180 - restrictAngle;
                    }
                    else
                    {
                        rotZ = -(180 - restrictAngle);
                    }
                }
            }
            Instantiate(projectile, Firepoint.position, Quaternion.Euler(0f, 0f, rotZ + offset));
            yield return new WaitForSeconds(0.5f);
            resetBowTimer();
        }
    }


    public IEnumerator stab()
    {
        if (canStabAgain == true) {
            canStabAgain = false;

            //always nessecary to negate movement smoothing
            //controller.setMovementSmoothing(0.3f);

            animator.SetBool("isAttacking", true);
            float time = 0.7f;
            rb.velocity = Vector2.up * 2;
            yield return new WaitForSeconds(0.2f);
            rb.velocity = new Vector2(transform.forward.z * flight, 2f);
            // rb.MovePosition((Vector2)transform.position + new Vector2(transform.forward.z, 0.2f) * flight * Time.deltaTime);
            while (time >= 0)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Firepoint.position, attackRange);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.tag == "Enemy")
                    {
                        print("hit an enemy");
                    }
                }
                time -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            animator.SetTrigger("Idle");
            animator.SetBool("isAttacking", false);

            //return to default movement smoothing
            //controller.setMovementSmoothing(0.05f);

            yield return new WaitForSeconds(2f);
            canStabAgain = true;
        }
 
   }


    private void OnDrawGizmosSelected()
    {
        if (Firepoint == null)
            return;

        Gizmos.DrawWireSphere(Firepoint.position, attackRange);
        
    }



}
