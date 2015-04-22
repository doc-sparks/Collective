using UnityEngine;
using System.Collections;

public class MinionSceneryTriggerListener : MonoBehaviour
{

	public float fallingEpsilon_ = 0.01f;
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
		if (Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.y) > fallingEpsilon_) {
			minionStateMachine_.changeState (MinionStateMachine.MinionState.falling);
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Platform") {

			foreach (ContactPoint2D pnt in col.contacts) {
				if (pnt.point.y < transform.position.y) {
					Debug.Log ("MinionSceneryTriggerListener.OnCollisionEnter2D:  hit platform");
					minionStateMachine_.changeState (MinionStateMachine.MinionState.landing);
					break;
				}
			}
		}
	}

	// we've been insta-killed by the scenery
	void hitSceneryInstantKill ()
	{
		Debug.Log ("MinionSceneryTriggerListener.hitSceneryInstantKill:  ");
		minionStateMachine_.changeState (MinionStateMachine.MinionState.killed);
	}
}
