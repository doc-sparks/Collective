using UnityEngine;
using System.Collections;

public class MinionSceneryTriggerListener : MonoBehaviour
{

		private MinionStateMachine minionStateMachine_;

		// Use this for initialization
		void Start ()
		{

				// grab the Minion state engine
				minionStateMachine_ = GetComponent<MinionStateMachine> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		// we've been insta-killed by the scenery
		void hitSceneryInstantKill ()
		{
				Debug.Log ("MinionSceneryTriggerListener.hitSceneryInstantKill:  ");
				minionStateMachine_.changeState (MinionStateMachine.MinionState.killed);
		}
}
