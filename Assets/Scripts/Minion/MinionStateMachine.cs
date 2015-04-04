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
				killed,
				resurrect
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

				// can we make this state change?
				if (!checkStateChange (currState_, new_state))
						return;

				Debug.Log ("MinonStateMachine.changeState: state change OK");

				// set the new state
				currState_ = new_state;

				// send the event
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

				case MinionState.killed:
						return "Killed";

				case MinionState.resurrect:
						return "Resurrect";
				}	

				return "Unknown";

		}

		// check for valid state changes
		public bool checkStateChange (MinionStateMachine.MinionState old_state, MinionStateMachine.MinionState new_state)
		{
				bool flag = false;

				switch (new_state) {
				case MinionState.idle:
						flag = true;
						break;

				case MinionState.left:
				case MinionState.right:
						if (old_state == MinionState.idle) {
								flag = true;
						}
						break;

				case MinionState.killed:
						if ((old_state == MinionState.idle) ||
								(old_state == MinionState.left) || 
								(old_state == MinionState.right)) {
								flag = true;
						}

						break;

				case MinionState.resurrect:
						if (old_state == MinionState.killed) {
								flag = true;
						}
						break;

				}

				return flag;
		}
}
