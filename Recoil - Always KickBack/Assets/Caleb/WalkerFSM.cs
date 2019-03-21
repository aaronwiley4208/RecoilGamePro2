using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerFSM : MonoBehaviour {

    public enum Transition
    {
        NULL = 0,
    }

    public enum StateID
    {
        NULL = 0,
    }

    public abstract class FSMstate
    {
        protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
        protected StateID stateID;
        public StateID ID { get { return stateID; } }

        public void AddTransition(Transition trans, StateID id)
        {
            if (trans == Transition.NULL)
            {
                Debug.LogError("FSM ERROR: cannot use null transition");
                return;
            }

            if (id == StateID.NULL)
            {
                Debug.LogError("FSM ERROR: cannot use null state");
                return;
            }

            if (map.ContainsKey(trans))
            {
                Debug.LogError("FSM ERROR: transition already in use");
                return;
            }
            map.Add(trans, id);
        }

        public StateID GetOutputState(Transition trans)
        {
            if (map.ContainsKey(trans))
            {
                return map[trans];
            }
            return StateID.NULL;
        }

        public virtual void DoBeforeEntering() { }

        public virtual void DoBeforeLeaving() { }

        public abstract void Reason(GameObject player, GameObject NPC);

        public abstract void Act(GameObject player, GameObject NPC);
    }

    public class FSMsystem
    {
        private List<FSMstate> states;

        private StateID currentStateID;
        public StateID CurrentStateID { get { return currentStateID; } }
        private FSMstate currentState;
        public FSMstate CurrentState { get { return currentState; } }

        public FSMsystem()
        {
            states = new List<FSMstate>();
        }

        public void AddState(FSMstate s)
        {
            if (s == null)
            {
                Debug.LogError("FSM ERROR: attempting to add null state");
            }

            if (states.Count == 0)
            {
                states.Add(s);
                currentState = s;
                currentStateID = s.ID;
                return;
            }

            foreach (FSMstate state in states)
            {
                if (state.ID == s.ID)
                {
                    Debug.LogError("FSM ERROR: trying to add existing state");
                    return;
                }
            }
            states.Add(s);
        }

        public void PerformTransition(Transition trans)
        {
            if (trans == Transition.NULL)
            {
                Debug.LogError("FSM ERROR: attempting to use null transition");
                return;
            }

            StateID id = currentState.GetOutputState(trans);
            if (id == StateID.NULL)
            {
                Debug.LogError("FSM ERROR: transition does not have valid target");
                return;
            }

            currentStateID = id;
            foreach(FSMstate state in states)
            {
                if (state.ID == currentStateID)
                {
                    currentState.DoBeforeLeaving();

                    currentState = state;

                    currentState.DoBeforeEntering();
                    break;
                }
            }
        }





    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
