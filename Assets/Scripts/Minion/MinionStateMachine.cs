using UnityEngine;
using System.Collections;

// Class to handle the state machine for the minion
public class MinionStateMachine : MonoBehaviour
{

		private MinionState currState_;

		// Minion states
		public enum MinionState
		{
				idle = 0,
				left,
				right,
		}

		public delegate void MinionStateHandler (MinionStateMachine.MinionState new_state);

		public static event MinionStateHandler MinionStateChangeEvent;

		// Use this for initialization
		void Start ()
		{

				// initial state
				currState_ = MinionState.idle;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		// Attempt to change state
		public void changeState (MinionStateMachine.MinionState new_state)
		{

				Debug.Log ("MinonStateMachine.changeState: Current State = " + stateToString (currState_) +
						", New State = " + stateToString (new_state));

				if (new_state == currState_)
						return;

				// set the new state
				currState_ = new_state;

				// no restrictions on change yet so send the event
				if (MinionStateChangeEvent != null) {
						MinionStateChangeEvent (new_state);
				}	
		}

		// return a string of the given state
		public string stateToString (MinionStateMachine.MinionState state)
		{
				switch (state) {
				case MinionState.idle:
						return "Idle";

				case MinionState.left:
						return "Left";

				case MinionState.right:
						return "Right";

				}	

				return "Unknown";

		}
}
