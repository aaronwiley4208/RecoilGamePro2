using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : MonoBehaviour {

    public GameObject player;
    public GameObject leftObj;
    public Transform leftPoint;
    public GameObject rightObj;
    public Transform rightPoint;

    public int currentState = 0;
    public int nextState = 0;

    //range in which the enemy will attack the player
    public int agroRange = 10;

    //range above/below patrol zone where enemy can still function
    public int yBound = 3;

    public float lungeForce = 80;
    public float attackCooldown = 5;
    private float canNextAttack = 0;
    private Vector3 toPlayer;

    public float gravMult = 15;

    public bool isMovingLeft = true;
    private Vector3 moveVector;
    //patrolLeniency is a bit of give for patrolling, due to the jerkiness of movement
    public float patrolLeniency = 1;
    public float moveForce = 40;
    //urgency is a modifier to make the enemy return faster
    public float urgency = 1.2f;
    public float moveCooldown = 3;
    private float canNextMove = 0;

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
    }
	
	// Update is called once per frame
	void Update () {
		switch (currentState)
        {
            case (int)States.PATROLLING:  //completed
                //if player in range
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)
                {
                    nextState = (int)States.ATTACKING;
                }
                else
                {
                    //return code below

                    //move cooldown check
                    if (Time.time >= canNextMove)
                    {
                        canNextMove = Time.time + moveCooldown;

                        //when moving left
                        if (isMovingLeft)
                        {
                            moveVector = this.transform.position;
                            moveVector.x -= 1;
                            moveVector.y += .2f;

                            this.GetComponent<Rigidbody>().AddForce(moveVector * moveForce, ForceMode.Impulse);
                            //if reaching the leftmost edge of the patrol route
                            if (this.transform.position.x <= leftPoint.position.x + patrolLeniency)
                            {
                                isMovingLeft = false;
                            }
                        }
                        else  //if moving right
                        {
                            moveVector = this.transform.position;
                            moveVector.x += 1;
                            moveVector.y += .2f;

                            this.GetComponent<Rigidbody>().AddForce(moveVector * moveForce, ForceMode.Impulse);
                            //if reaching the leftmost edge of the patrol route
                            if (this.transform.position.x >= leftPoint.position.x - patrolLeniency)
                            {
                                isMovingLeft = true;
                            }
                        }
                    }
                }
                break;
            case (int)States.ATTACKING:  //completed
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
                    else  //not changing directly to patrolling
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
                    //can enemy currently attack?
                    if (Time.time > canNextAttack)
                    {
                        //set next viable attack time, spaghetti style
                        canNextAttack = Time.time + attackCooldown;

                        //what?  me?  ripping this from jason's gun script?  whaaaaat?  noooo, definitely not
                        toPlayer = (transform.position - player.transform.position).normalized;
                        this.GetComponent<Rigidbody>().AddForce(toPlayer * lungeForce, ForceMode.Impulse);
                    }
                    
                }
                break;
            case (int)States.RETURNING:  //completed
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
                    //return code below

                    //move cooldown check
                    if (Time.time >= canNextMove)
                    {
                        canNextMove = Time.time + moveCooldown;

                        if (this.transform.position.x <= leftPoint.position.x)
                        {
                            isMovingLeft = false;
                            //arbitrary movement vector
                            moveVector = this.transform.position;
                            moveVector.x += 1;
                            moveVector.y += .2f;

                            this.GetComponent<Rigidbody>().AddForce(moveVector * moveForce * urgency, ForceMode.Impulse);
                        } else if (this.transform.position.x >= rightPoint.position.x)
                        {
                            isMovingLeft = true;
                            //arbitrary movement vector
                            moveVector = this.transform.position;
                            moveVector.x -= 1;
                            moveVector.y += .2f;

                            this.GetComponent<Rigidbody>().AddForce(moveVector * moveForce * urgency, ForceMode.Impulse);
                        }
                        else
                        {
                            nextState = (int)States.LOST;
                            print("BAD RETURN CHECK HELP ME");
                        }
                    }
                }   
                break;
            case (int)States.LOST:  //complete?
                //if player in range
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)
                {
                    nextState = (int)States.ATTACKING;
                }
                else
                {
                    //cower
                    //do lost code
                    //thinking there just isn't lost code.  maybe some fluff, but nothing important
                }
                break;
        }
	}

    private void LateUpdate()
    {
        //change state after update concludes
        currentState = nextState;
    }

    private void FixedUpdate()
    {
        //pfft, no, never would i EVER rip code off'a jason's PlayerRecoilMovement scrip. pft
        this.GetComponent<Rigidbody>().AddForce(Physics.gravity * gravMult, ForceMode.Acceleration);
    }
}
