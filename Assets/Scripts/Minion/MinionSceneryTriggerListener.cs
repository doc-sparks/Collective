using UnityEngine;
using System.Collections;

public class MinionSceneryTriggerListener : MonoBehaviour
{

	public float velocityEpsilon_ = 0.01f;
	private MinionStateMachine minionStateMachine_;		

		// Use this for initialization
		void Start ()
		{

				// grab the Minion state engine
				minionStateMachine_ = GetComponent<MinionStateMachine> ();
		}
	
		// Update is called once per frame
		void LateUpdate ()
		{
			// check if we're falling
			if (rigidbody2D.velocity.y != 0f)
		{
			minionStateMachine_.changeState (MinionStateMachine.MinionState.falling);
		}
		}

		// we've been insta-killed by the scenery
		void hitSceneryInstantKill ()
		{
				Debug.Log ("MinionSceneryTriggerListener.hitSceneryInstantKill:  ");
				minionStateMachine_.changeState (MinionStateMachine.MinionState.killed);
		}
}
