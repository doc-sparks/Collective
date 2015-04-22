using UnityEngine;
using System.Collections;

public class MinionStateLifeListener : MonoBehaviour
{

	MinionStateMachine.MinionState currState_;
	private MinionStateMachine minionStateMachine_;

	// Use this for initialization
	void Start ()
	{
		// current state
		currState_ = MinionStateMachine.MinionState.idle;

		// grab the Minion state engine
		minionStateMachine_ = GetComponent<MinionStateMachine> ();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		switch (currState_) {
		case MinionStateMachine.MinionState.killed:
			// we've been killed. For the moment, just go to resurrect
			minionStateMachine_.changeState (MinionStateMachine.MinionState.resurrect);
			break;

		case MinionStateMachine.MinionState.resurrect:
						// reset position and go to idle
			transform.position = new Vector3 (0, 0, 0);
			transform.rotation = Quaternion.identity;
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			GetComponent<Rigidbody2D> ().angularVelocity = 0f;
			minionStateMachine_.changeState (MinionStateMachine.MinionState.idle);
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
