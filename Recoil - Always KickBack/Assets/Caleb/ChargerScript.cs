using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerScript : MonoBehaviour {

    public GameObject player;
    public static GameObject leftPoint;
    public static GameObject rightPoint;

    private FSMscript.FSMsystem fsm;

    public void SetTransition(FSMscript.Transition t) { fsm.PerformTransition(t); }


	// Use this for initialization
	void Start () {
        MakeFSM();
	}

    public void FixedUpdate()
    {
        fsm.CurrentState.Reason(player, gameObject);
        fsm.CurrentState.Act(player, gameObject);
    }

    private void MakeFSM()
    {
        PatrolState patrol = new PatrolState();
        patrol.AddTransition(FSMscript.Transition.SawPlayer, FSMscript.StateID.Attacking);

        AttackPlayerState attack = new AttackPlayerState();
        attack.AddTransition(FSMscript.Transition.LostInRange, FSMscript.StateID.Patroling);
        attack.AddTransition(FSMscript.Transition.LostOutOfRange, FSMscript.StateID.Returning);

        ReturnState doReturn = new ReturnState();
        doReturn.AddTransition(FSMscript.Transition.IsBack, FSMscript.StateID.Patroling);
        doReturn.AddTransition(FSMscript.Transition.SawPlayer, FSMscript.StateID.Attacking);

        fsm = new FSMscript.FSMsystem();
        fsm.AddState(patrol);
        fsm.AddState(attack);
        fsm.AddState(doReturn);
    }

    public class PatrolState : FSMscript.FSMstate
    {
        private int currentPoint;  //0 is left waypoint, 1 is right, i'm not making an enum for it
        
        public PatrolState()
        {
            stateID = FSMscript.StateID.PatrolState;
        }

        public override void Reason(GameObject player, GameObject npc)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(npc.transform.position, npc.transform.forward, out hit, 15F))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    npc.GetComponent<ChargerScript>().SetTransition(FSMscript.Transition.SawPlayer);
                }
            }
        }

        public override void Act(GameObject player, GameObject npc)
        {
            Vector3 vel = npc.GetComponent<Rigidbody>().velocity;
            Vector3 moveDir;
            if (currentPoint == 0)
            {
                moveDir = leftPoint.transform.position - npc.transform.position;
            } else if (currentPoint == 1)
            {
                moveDir = rightPoint.transform.position - npc.transform.position;
            }
            else
            {
                moveDir = npc.transform.position;
            }

            if (moveDir.magnitude < 1)
            {
                currentPoint = (currentPoint + 1) % 2;  //swanky eh?
            } else
            {
                vel = moveDir.normalized * 10;

                npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(moveDir), 5 * Time.deltaTime);
                npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);
            }

            npc.GetComponent<Rigidbody>().velocity = vel;
        }
    }

    public class AttackPlayerState : FSMscript.FSMstate
    {
        public AttackPlayerState()
        {
            stateID = FSMscript.StateID.AttackingPlayer;
        }

        public override void Reason(GameObject player, GameObject npc)
        {
            if (Vector3.Distance(npc.transform.position, player.transform.position) >= 30)
            {
                if (true)  //lost in range check
                {
                    npc.GetComponent<ChargerScript>().SetTransition(FSMscript.Transition.LostInRange);
                } else
                {
                    npc.GetComponent<ChargerScript>().SetTransition(FSMscript.Transition.LostOutOfRange);
                }
            }
        }

        public override void Act(GameObject player, GameObject npc)
        {
            //TODO:  impulse at player
        }
    }

    public class ReturningState : FSMscript.FSMstate
    {
        public ReturningState()
        {
            stateID = FSMscript.StateID.Returning;
        }

        public override void Reason(GameObject player, GameObject npc)
        {
            //TODO:  if between both waypoints, move to farther one
        }

        public override void Act(GameObject player, GameObject npc)
        {
            //TODO:  move to nearest waypoint, unless y too bad, then cower
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
