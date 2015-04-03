using UnityEngine;
using System.Collections;

public class CameraMinionStateListener : MonoBehaviour {

	private MinionStateMachine.MinionState currState_;

	// Use this for initialization
	void Start () {


		// initial minion state
		currState_ = MinionStateMachine.MinionState.idle;	
	}
	
	// Update is called once per frame
	void Update () {
	
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
