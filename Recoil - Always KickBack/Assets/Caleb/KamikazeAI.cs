using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KamikazeAI : MonoBehaviour {

    NavMeshAgent agent;  //this object's nav agent
    public GameObject player;  //reference to the player
    public GameObject home;  //reference to an object denoting the starting/returning location
    public Transform homePoint;  //quick reference to home location's transform

    public float agroRange = 10;
    public float deAgroRange = 20;

    public int currentState = 2;
    public int nextState = 2;

    public enum States
    {
        IDLE,
        ATTACKING,
        RETURNING
    }

    private bool hardStop = false;  //emergency stop boolean, just in case

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        homePoint = home.transform;
	}
	
	// Update is called once per frame
	void Update () {

        //agent.destination = player.transform.position;  //move code

        switch (currentState)
        {
            case (int)States.IDLE:
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)  //if player in range
                {
                    nextState = (int)States.ATTACKING;  //start attacking
                }
                //else remain idle
                break;
            case (int)States.ATTACKING:
                if (Vector3.Distance(player.transform.position, this.transform.position) >= deAgroRange)  //if player out of range
                {
                    nextState = (int)States.RETURNING;  //always return, the odds of being on home point are nuts
                } else  //else player in range, so do attack
                {
                    agent.destination = player.transform.position;  //sets destination to be player's current position
                    //note that this is non-predictive; further, because returning state resets the destination, there
                    //shouldn't be any worry about odd behavior when transitioning to returning
                }
                break;
            case (int)States.RETURNING:
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)  //if player re-enters range while returning
                {
                    nextState = (int)States.ATTACKING;
                }
                else if (Vector3.Distance(this.transform.position,homePoint.position) <= agent.stoppingDistance && agent.isStopped)  //if agent has stopped returning
                {
                    nextState = (int)States.IDLE;  //it occurs to me that idle/returning could be merged.  eh
                }
                else  //else keep returning
                {
                    agent.destination = homePoint.position;  //set destination to home point
                }
                break;
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
            hardStop = true;  //engage emergency stop
            //TODO:  player damage
            //Kamikaze effect
            Destroy(this.gameObject);  //kamikaze go bye bye
        }
    }
}
