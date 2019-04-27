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

    //magnitudes for movement, put here for convenience
    public float moveX = 1;
    public float moveY = .5f;

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
    void Update() {
        switch (currentState)
        {
            case (int)States.PATROLLING:  //completed
                //if player in range
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)
                {
                    nextState = (int)States.ATTACKING;
                }
                //if outside of patrol zone
                else if (this.transform.position.x < leftPoint.position.x || this.transform.position.x > rightPoint.position.x)
                {
                    //and if outside of ybounds
                    if (this.transform.position.y < leftPoint.position.y - yBound || this.transform.position.y > leftPoint.position.y + yBound)
                    {
                        nextState = (int)States.LOST;
                    }
                    else  //else within ybounds
                    {
                        Debug.Log("PATROLLING>RETURNING");
                        nextState = (int)States.RETURNING;
                    }
                }
                else
                {
                    //return code below

                    //move cooldown check
                    if (Time.time >= canNextMove)
                    {
                        canNextMove = Time.time + moveCooldown;

                        //"turning"
                        if (isMovingLeft)
                        {
                            //if reaching the leftmost edge of the patrol route
                            if (this.transform.position.x <= leftPoint.position.x + patrolLeniency)
                            {
                                isMovingLeft = false;
                            }
                        }
                        else
                        {
                            //if reaching the rightmost edge of the patrol route
                            if (this.transform.position.x >= rightPoint.position.x - patrolLeniency)
                            {
                                isMovingLeft = true;
                            }
                        }

                        //moving
                        //when moving left
                        if (isMovingLeft)
                        {
                            //Debug.Log("I AM MOVING LEFT");
                            //moveVector = this.transform.position;
                            moveVector.x = 0 - moveX;
                            moveVector.y = moveY;
                            moveVector.z = 0;

                            this.GetComponent<Rigidbody>().AddForce(moveVector * moveForce, ForceMode.Impulse);
                        }
                        else  //if moving right
                        {
                            //Debug.Log("I AM MOVING RIGHT");
                            //moveVector = this.transform.position;
                            moveVector.x = moveX;
                            moveVector.y = moveY;
                            moveVector.z = 0;

                            this.GetComponent<Rigidbody>().AddForce(moveVector * moveForce, ForceMode.Impulse);
                        }
                    }
                }
                break;
            case (int)States.ATTACKING:  //completed
                //if player not in range
                if (Vector3.Distance(player.transform.position, this.transform.position) > agroRange)
                {
                    //assuming x plane for now
                    //if within patrol zone
                    if (this.transform.position.x >= leftPoint.position.x && this.transform.position.x <= rightPoint.position.x)
                    {
                        //special case check, in case enemy gets stranded directly above/below patrol zone
                        if (this.transform.position.y < leftPoint.position.y + yBound && this.transform.position.y > leftPoint.position.y - yBound)
                        {
                            Debug.Log("ATTACKING>PATROLLING");
                            nextState = (int)States.PATROLLING;
                        }
                        else  //edge case lost transition
                        {
                            Debug.Log("ATTACKING>LOST");
                            nextState = (int)States.LOST;
                        }
                    }
                    else  //not changing directly to patrolling
                    {
                        //if within y bounds, return
                        if (this.transform.position.y < leftPoint.position.y + yBound && this.transform.position.y > leftPoint.position.y - yBound)
                        {
                            Debug.Log("ATTACKING>RETURNING");
                            nextState = (int)States.RETURNING;
                        }
                        else  //else enemy is lost
                        {
                            Debug.Log("ATTACKING>LOST");
                            nextState = (int)States.LOST;
                        }
                    }
                }
                else  //else, execute attack code
                {
                    //can enemy currently attack?
                    if (Time.time > canNextAttack)
                    {
                        Debug.Log("CHARGE!");
                        //set next viable attack time, spaghetti style
                        canNextAttack = Time.time + attackCooldown;

                        //what?  me?  ripping this from jason's gun script?  whaaaaat?  noooo, definitely not
                        toPlayer = (player.transform.position - transform.position).normalized;
                        toPlayer.z = 0;
                        toPlayer *= lungeForce;  //pre-emptive scaling
                        if (toPlayer.y < moveY)
                        {
                            toPlayer.y = moveY;  //clamping so that the enemy moves while the player is on the ground
                        }
                        this.GetComponent<Rigidbody>().AddForce(toPlayer, ForceMode.Impulse);
                    }

                }
                break;
            case (int)States.RETURNING:  //completed
                //if player in range
                if (Vector3.Distance(player.transform.position, this.transform.position) <= agroRange)
                {
                    Debug.Log("RETURNING>ATTACKING");
                    nextState = (int)States.ATTACKING;
                }
                //if player not in range and player within patrol zone
                else if (this.transform.position.x >= leftPoint.position.x && this.transform.position.x <= rightPoint.position.x)
                {
                    //special case check, in case enemy gets stranded directly above/below patrol zone
                    if (this.transform.position.y < leftPoint.position.y + yBound && this.transform.position.y > leftPoint.position.y - yBound)
                    {
                        Debug.Log("RETURNING>PATROLLING");
                        nextState = (int)States.PATROLLING;
                    }
                    else  //edge case lost transition
                    {
                        Debug.Log("RETURNING>LOST 1");
                        nextState = (int)States.LOST;
                    }
                }
                //otherwise, if enemy outside of y bounds
                else if (this.transform.position.y < leftPoint.position.y - yBound || this.transform.position.y > leftPoint.position.y + yBound)
                {
                    Debug.Log("RETURNING>LOST 2");
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
                            //moveVector = this.transform.position;
                            moveVector.x = moveX;
                            moveVector.y = moveY;
                            moveVector.z = 0;

                            this.GetComponent<Rigidbody>().AddForce(moveVector * moveForce * urgency, ForceMode.Impulse);
                        } else if (this.transform.position.x >= rightPoint.position.x)
                        {
                            isMovingLeft = true;
                            //arbitrary movement vector
                            //moveVector = this.transform.position;
                            moveVector.x = 0 - moveX;
                            moveVector.y = moveY;
                            moveVector.z = 0;

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
                    Debug.Log("LOST>ATTACKING");
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
