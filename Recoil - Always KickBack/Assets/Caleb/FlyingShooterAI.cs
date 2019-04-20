using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingShooterAI : MonoBehaviour {

    NavMeshAgent agent;  //this object's nav agent
    public GameObject player;  //reference to the player
    public GameObject home;  //reference to an object denoting the starting/returning location
    public Transform homePoint;  //quick reference to home location's transform

    public float agroRange = 20;
    public float deAgroRange = 30;
    public float firingRange = 25;
    public float preferredRange = 15;
    public float preferredRangePadding = .5f;  //needed?

    //used in getting to preferred range
    Vector3 temp;

    public int currentState = 2;
    public int nextState = 2;

    //variable controlling firing
    bool canFire = false;  //whether the AI is currently allowed to fire
    float canNextFire = 0;  //when the AI is next allowed to fire
    public float fireDelay = 5;  //how long the AI waits between shots

    public enum States
    {
        IDLE,
        ATTACKING,
        RETURNING
    }

    private bool hardStop = false;  //emergency stop boolean, just in case

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        homePoint = home.transform;
    }

    // Update is called once per frame
    void Update()
    {

        //agent.destination = player.transform.position;  //move code

        switch (currentState)
        {
            case (int)States.IDLE:
                //when idling, weapons are off
                canFire = false;
                canNextFire = Time.time + fireDelay;

                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)  //if player in range
                {
                    nextState = (int)States.ATTACKING;  //start attacking
                }
                //else remain idle
                break;
            case (int)States.ATTACKING:
                //when attacking, weapons are on
                canFire = true;
                //note:  attacking state does not reset canNextFire, meaning that it will occur naturally below the switch

                if (Vector3.Distance(player.transform.position, this.transform.position) >= deAgroRange)  //if player out of range
                {
                    nextState = (int)States.RETURNING;  //always return, the odds of being on home point are nuts
                }
                else  //else player in range, so move to preferred range
                {
                    //normalize the distance from player to enemy, then scale that to get to the
                    //preferred point along that distance
                    temp = (player.transform.position - this.transform.position);
                    temp = temp.normalized * preferredRange;
                    temp.z = 0;

                    //set the agent's destination to the preferred location
                    agent.destination = temp;
                }
                break;
            case (int)States.RETURNING:
                //when returning, weapons are off
                canFire = false;
                canNextFire = Time.time + fireDelay;

                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)  //if player re-enters range while returning
                {
                    nextState = (int)States.ATTACKING;
                }
                else if (Vector3.Distance(this.transform.position, homePoint.position) <= agent.stoppingDistance && agent.isStopped)  //if agent has stopped returning
                {
                    nextState = (int)States.IDLE;  //it occurs to me that idle/returning could be merged.  eh
                }
                else  //else keep returning
                {
                    agent.destination = homePoint.position;  //set destination to home point
                }
                break;
        }

        //only perform shooting logic if can currently shoot, otherwise ignore
        if (canFire)
        {
            //if fire delay is over
            if (Time.time >= canNextFire)
            {
                //TODO:  fire at player
                canNextFire = Time.time + fireDelay;
            }
        }
    }

    private void LateUpdate()
    {
        //change state after update concludes
        currentState = nextState;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player)  //on collision with player
        {
            //TODO:  player damage
            //TODO: get normal vector from player to this, impulse this away from player
        }
    }
}
