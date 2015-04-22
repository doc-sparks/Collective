using UnityEngine;
using System.Collections;

public class MinonKeyboardController : MonoBehaviour
{

	private float lastMove_ = 0f;
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

	void LateUpdate ()
	{

		// get horizontal control
		// NOTE: Set InputManager/Horiztonal/Gravity 3 -> 100
		// NOTE: Set InputManager/Horiztonal/Sensitivity 3 -> 10
		float hz = Input.GetAxis ("Horizontal");

		// check against previous input to see if there's a change
		if (hz != lastMove_) {
			if (hz < 0f) {
				// moving left
				minionStateMachine_.changeState (MinionStateMachine.MinionState.left);
			} else if (hz > 0f) {
				// moving right
				minionStateMachine_.changeState (MinionStateMachine.MinionState.right);
			} else {
				// Stopped moving
				minionStateMachine_.changeState (MinionStateMachine.MinionState.idle);
			}
		}

		// store previous move
		lastMove_ = hz;

		// Sort out jumping
		float jump = Input.GetAxis ("Jump");
		if (jump > 0.0f) {
			minionStateMachine_.changeState (MinionStateMachine.MinionState.jump);
		}
	}
}
