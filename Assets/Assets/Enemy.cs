using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Transform edgeCheck;
    private Transform groundCheck;
    [SerializeField] private float radius;
    public string displayState;
    public HealthSystem healthSystem;

    State state;
    private void OnEnable()
    {
        healthSystem = new HealthSystem(100);
        healthSystem.onEntityDie += HealthSystem_onEntityDie;
        edgeCheck = transform.Find("edgeCheck");
        groundCheck = transform.Find("groundCheck");
        setSpeed(2f);
        state = State.Patrolling;
    }

    private void HealthSystem_onEntityDie(object sender, System.EventArgs e)
    {
        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.x >= 0)
        {
            rb.AddForce(new Vector2(1, 3.5f));
        }
        else
        {
            rb.AddForce(new Vector2(-1, 3.5f));
        }
        cc.enabled = false;
        Destroy(this.gameObject, 2f);
    }

    private void FixedUpdate()
    {
        displayState = "" + state;

        performGroundCheck(groundCheck, radius);
        if (state == State.Patrolling) Patrol(edgeCheck);
        if (state == State.Aggressive) attackEnemy(player);
        if (inRange(player.transform)){
            state = State.Aggressive;
        }
        else if ((player.transform.position - transform.position).magnitude >= 10)
        {
            state = State.Patrolling;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(groundCheck.position.x, groundCheck.position.y, 0), radius);
    }
    */
}
