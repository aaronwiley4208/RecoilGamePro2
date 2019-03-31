using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : MonoBehaviour {

    public GameObject player;
    public GameObject leftObj;
    public Transform leftPoint;
    public GameObject rightObj;
    public Transform rightPoint;

    public int[,] transitions = new int[8, 2];

    public int currentState = 0;
    public int nextState = 0;

    //range in which the enemy will attack the player
    public int agroRange = 10;

    //range above/below patrol zone where enemy can still function
    public int yBound = 3;

    public enum States
    {
        PATROLLING, //as name states
        ATTACKING,  //launching at player
        RETURNING,  //lost player and left patrol zone, returning
        LOST        //lost player and too far above/below patrol zone
    }

	// Use this for initialization
	void Start () {
        leftPoint = leftObj.transform;
        rightPoint = rightObj.transform;

        //transitions array represents valid state transitions
        // x,0 is the source state for a given transition, x,1 is the destination state

        //patrolling>attacking, enemy has seen player
        transitions[0, 0] = (int)States.PATROLLING;
        transitions[0, 1] = (int)States.ATTACKING;

        //attacking>patrolling, enemy loses player while inside patrol zone
        transitions[1, 0] = (int)States.ATTACKING;
        transitions[1, 1] = (int)States.PATROLLING;

        //attacking>returning, enemy loses player outside patrol zone, within y bounds
        transitions[2, 0] = (int)States.ATTACKING;
        transitions[2, 1] = (int)States.RETURNING;

        //attacking>lost, enemy loses player outside patrol zone, outside y bounds
        transitions[3, 0] = (int)States.ATTACKING;
        transitions[3, 1] = (int)States.LOST;

        //returning>patrolling, enemy returns to patrol zone after losing player
        transitions[4, 0] = (int)States.RETURNING;
        transitions[4, 1] = (int)States.PATROLLING;

        //returning>lost, enemy leaves y bounds while returning
        transitions[5, 0] = (int)States.RETURNING;
        transitions[5, 1] = (int)States.LOST;

        //returning>attacking, enemy was returning but has resighted the player
        transitions[6, 0] = (int)States.RETURNING;
        transitions[6, 1] = (int)States.ATTACKING;

        //lost>attacking, enemy was lost but has resighted the player
        transitions[7, 0] = (int)States.LOST;
        transitions[7, 1] = (int)States.ATTACKING;
    }
	
	// Update is called once per frame
	void Update () {
		switch (currentState)
        {
            case (int)States.PATROLLING:
                //if player in range
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)
                {
                    nextState = (int)States.ATTACKING;
                }
                else
                {
                    //given current direction, move towards end point; once within range of end point, swap direction
                    //do patrol code
                }
                break;
            case (int)States.ATTACKING:
                //if player not in range
                if (Vector3.Distance(player.transform.position, this.transform.position) > 10)
                {
                    //assuming x plane for now
                    //if within patrol zone
                    if (this.transform.position.x >= leftPoint.position.x && this.transform.position.x <= rightPoint.position.x)
                    {   
                        //special case check, in case enemy gets stranded directly above/below patrol zone
                        if (this.transform.position.y < leftPoint.position.y+yBound && this.transform.position.y > leftPoint.position.y - yBound)
                        {
                            nextState = (int)States.PATROLLING;
                        }
                        else  //edge case lost transition
                        {
                            nextState = (int)States.LOST;
                        }
                    }
                    else  //not returning to patrolling
                    {
                        //if within y bounds, return
                        if (this.transform.position.y < leftPoint.position.y + yBound && this.transform.position.y > leftPoint.position.y - yBound)
                        {
                            nextState = (int)States.RETURNING;
                        }
                        else  //else enemy is lost
                        {
                            nextState = (int)States.LOST;
                        }
                    }
                }
                else  //else, execute attack code
                {
                    //every so often, impulse at player
                    //do attack code
                }
                break;
            case (int)States.RETURNING:
                //if player in range
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)
                {
                    nextState = (int)States.ATTACKING;
                }
                //if player not in range and player within patrol zone
                else if (this.transform.position.x >= leftPoint.position.x && this.transform.position.x <= rightPoint.position.x)
                {
                    //special case check, in case enemy gets stranded directly above/below patrol zone
                    if (this.transform.position.y < leftPoint.position.y + yBound && this.transform.position.y > leftPoint.position.y - yBound)
                    {
                        nextState = (int)States.PATROLLING;
                    }
                    else  //edge case lost transition
                    {
                        nextState = (int)States.LOST;
                    }
                }
                //otherwise, if enemy outside of y bounds
                else if (this.transform.position.y < leftPoint.position.y + yBound && this.transform.position.y > leftPoint.position.y - yBound)
                {
                    nextState = (int)States.LOST;
                }
                else
                {
                    //if too far left, go right; else go left
                    //do return code
                }
                break;
            case (int)States.LOST:
                //if player in range
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)
                {
                    nextState = (int)States.ATTACKING;
                }
                else
                {
                    //cower
                    //do lost code
                }
                break;
        }
	}

    private void LateUpdate()
    {
        currentState = nextState;
    }
}
