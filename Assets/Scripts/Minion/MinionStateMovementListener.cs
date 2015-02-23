using UnityEngine;
using System.Collections;

public class MinionStateMovementListener : MonoBehaviour
{

		MinionStateMachine.MinionState currState_;
		public float moveSpeed_ = 1f;

		// Use this for initialization
		void Start ()
		{
				currState_ = MinionStateMachine.MinionState.idle;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
				// do stuff depending on the state
				switch (currState_) {
				case MinionStateMachine.MinionState.idle:
						break;

				case MinionStateMachine.MinionState.left:
						transform.Translate (new Vector3 (-moveSpeed_ * Time.deltaTime, 0f, 0f));
						break;

				case MinionStateMachine.MinionState.right:
						transform.Translate (new Vector3 (moveSpeed_ * Time.deltaTime, 0f, 0f));
						break;

				}
		}

		// subscribe to the state change event when enabled
		public void OnEnable ()
		{
				MinionStateMachine.MinionStateChangeEvent += MinionStateHandler;
		}

		public void OnDisable ()
		{
				MinionStateMachine.MinionStateChangeEvent -= MinionStateHandler;
		}

		// handle the state change
		void MinionStateHandler (MinionStateMachine.MinionState new_state)
		{
				currState_ = new_state;
		}
}
