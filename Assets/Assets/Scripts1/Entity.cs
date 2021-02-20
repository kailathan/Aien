using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Entity : MonoBehaviour
{

    //health class at bottom

    public enum State{
        Idle, 
        Following,
        RandomMove,
        Aggressive,
        Patrolling
        }
    private float health;
    private float speed;
    public bool isOnGround;
    private Rigidbody2D rb;
    public GameObject player;
    private float xdir = 1;

    public void setSpeed(float s)
    {
        speed = s;
    }
    public float getSpeed()
    {
        return speed;
    }
    public void onGround(bool condition)
    {
        isOnGround = condition;
    }
    public bool Grounded()
    {
        return isOnGround;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

    }
    public void FollowPlayer(Transform edgeCheck)
    {
            RaycastHit2D raycast = Physics2D.Raycast(edgeCheck.position, Vector2.down, 3f);
            //tp to player if the distance bw them > 5
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 5)
            {
                transform.position = player.transform.position;
            }

            //set the speed according to entity's relative position to the player
            if (player.transform.position.x > transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) > 2)
            {
                xdir = 1;
            }
            else if (player.transform.position.x <= transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) > 2)
            {
                xdir = -1;
            }
            //if the distance b/w the player and entity < 2, and raycast hits something, stop mvt
            else if (raycast)
            {
                xdir = 0;
            }

            //changing direction of entity
            if (xdir >= 0)
            { transform.eulerAngles = new Vector2(0, 0); }
            else
            { transform.eulerAngles = new Vector2(0, 180); }

            //move if the speed isnt 0 (there is no need to move)  and if the entity is on the ground
        if (isOnGround && xdir != 0)
        {
            if (raycast)
            {
                //dir = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
                rb.MovePosition(rb.position + new Vector2(xdir, 0) * speed * Time.deltaTime);

            }
            //raycast hits nothing
            else { idle(); }
        }

    }
    public void performGroundCheck(Transform t, float r)
    {
        isOnGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(t.position, r);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isOnGround = true;
            }
        }
    }
    public void Patrol(Transform edgeCheck)
    {
        RaycastHit2D raycastFowards = Physics2D.Raycast(edgeCheck.position, new Vector2(transform.forward.z, 0), 1.5f);
        RaycastHit2D raycastEdge = Physics2D.Raycast(edgeCheck.position, Vector2.down, 3f);
        if (!raycastEdge || (raycastFowards && raycastFowards.collider.gameObject.tag != "Player") )
        {

            if (xdir > 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            xdir = -xdir;
        }
        if (isOnGround)
        {  
            rb.MovePosition(rb.position + new Vector2(xdir, 0) * speed * Time.deltaTime);
        }
    }
    public IEnumerator randomMovement()
    {
        while (true) { 
            xdir = UnityEngine.Random.Range(-1, 2);

            if(xdir > 0) { transform.eulerAngles = new Vector2(0, 0); } else { transform.eulerAngles = new Vector2(0, 180); }

            if (isOnGround) { rb.velocity = new Vector2(xdir * 2, 0); }
            // rb.MovePosition(rb.position + new Vector2(xdir, 0) * speed*2 * Time.deltaTime); 

            yield return new WaitForSeconds(1.8f);
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > 3)
            {
                if (Mathf.Abs(transform.position.y - player.transform.position.y) > 5)
                {
                    transform.position = player.transform.position;
                }
            }
        }
    }

    public void attackEnemy(GameObject enemy)
    {
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
        Collider2D collider = GetComponent<Collider2D>();
        if (isOnGround) { transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, .03f); }
        if (enemy.transform.position.x - transform.position.x >= 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        if (collider.IsTouching(enemyCollider))
        {
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.forward.z * 3, 2);
        }
    }
    public void idle()
    {
        rb.velocity = Vector2.zero;
        //disable movement
    }

    public bool inRange(Transform targetTransform)
    {
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;*/

        Vector2 targetDir = targetTransform.position - transform.position;
        float angle;
        if (transform.rotation.y == 0)
        {
            angle = Vector2.Angle(Vector2.right, targetDir);
        }else
        {
            angle = Vector2.Angle(Vector2.left, targetDir);
        }
        if (targetDir.magnitude <= 5 && Mathf.Abs(angle) <= 30)
        {
            return true;
        }
        return false;
    }

}

public class HealthSystem
{
    private int health;
    private int healthMax;

    public event EventHandler onEntityDie;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }
    public int GetHealth()
    {
        return health;
    }
    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            onEntityDie?.Invoke(this, EventArgs.Empty);
        }
        Debug.Log("health remaining: " + health);
    }
    public void Heal(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, healthMax);
    }
}

