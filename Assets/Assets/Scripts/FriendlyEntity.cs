using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class FriendlyEntity : Entity
{
    public Transform edgeCheck;
    public Transform groundCheck;
    public float radius;
    public float speed;

    public string displayState;

    private State state;

    public GameObject enemy;

    private bool coroutineStarted;

    private void Start()
    {
        state = State.Following;
        setSpeed(speed);
    }
   

    // Update is called once per frame
    void FixedUpdate()
    {
        displayState = "" + state;
        performGroundCheck(groundCheck, radius);

        if (state == State.Following)
        {
            FollowPlayer(edgeCheck);
            //if entity is following player and distance is less than 2
            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 2)
            {
                state = State.RandomMove;
                StartCoroutine(randomMovement());
            }
        }
        //entering aggresive state
        if (state == State.Following || state == State.RandomMove)
        {
            if (Mathf.Abs(enemy.transform.position.x - transform.position.x) <= 3)
            {
                if (state == State.RandomMove) StopAllCoroutines(); //StopCoroutine("randomMovement");
                state = State.Aggressive;
            }
        }
        //doing stuff in aggressive state
        if (state == State.Aggressive) {
            attackEnemy(enemy);
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > 4)
                state = State.Following;
        }
        //exiting randomMove state
        if (state == State.RandomMove && Mathf.Abs(player.transform.position.x - transform.position.x) > 2)
        {
            state = State.Following;
            //StopCoroutine("randomMovement");
            StopAllCoroutines();

        }
        if (state == State.Patrolling)
        {
            Patrol(edgeCheck);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(groundCheck.position.x, groundCheck.position.y, 0), radius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            state = State.Following;
        }
    }
    public void printState()
    {
         print(state);
    }
}
