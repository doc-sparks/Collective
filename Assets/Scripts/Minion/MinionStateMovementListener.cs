using UnityEngine;
using System.Collections;

public class MinionStateMovementListener : MonoBehaviour
{


	public float moveSpeed_ = 1f;
	public float minionJumpVerticalForce_ = 200f;
	public float minionJumpHorizontalForce_ = 100f;
	private Animator minionAnimator_;
	private MinionStateMachine minionStateMachine_;
	private MinionStateMachine.MinionState currState_;

	// Use this for initialization
	void Start ()
	{
		// current state
		currState_ = MinionStateMachine.MinionState.idle;

		// grab the Minion animator
		minionAnimator_ = GetComponent<Animator> ();

		// grab the Minion state engine
		minionStateMachine_ = GetComponent<MinionStateMachine> ();

	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
	
		// do stuff depending on the state
		switch (currState_) {
		case MinionStateMachine.MinionState.idle:
						// Stop the walking anim
			minionAnimator_.SetBool ("Walking", false);
			break;

		case MinionStateMachine.MinionState.left:
						// move the minion
			transform.Translate (new Vector3 (-moveSpeed_ * Time.deltaTime, 0f, 0f));

						//Change the animation and point in the right direction
			minionAnimator_.SetBool ("Walking", true);
			if (transform.localScale.x > 0f) {
				Vector3 local_scale = transform.localScale;
				local_scale.x *= -1.0f;
				transform.localScale = local_scale;
			}
							
			break;

		case MinionStateMachine.MinionState.right:
			// move the minion
			transform.Translate (new Vector3 (moveSpeed_ * Time.deltaTime, 0f, 0f));

			//Change the animation and point in the right direction
			minionAnimator_.SetBool ("Walking", true);
			if (transform.localScale.x < 0f) {
				Vector3 local_scale = transform.localScale;
				local_scale.x *= -1.0f;
				transform.localScale = local_scale;
			}
			break;

		case MinionStateMachine.MinionState.landing:
			// we're landing so reset the Rigid body stuff and change state
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, 0f);
			transform.rotation = Quaternion.identity;

			// set the state depending on velocity - if left/right button is held down, this allows
			// character to keep moving
			if (GetComponent<Rigidbody2D> ().velocity.x < 0f) {
				minionStateMachine_.changeState (MinionStateMachine.MinionState.left);
			} else if (GetComponent<Rigidbody2D> ().velocity.x > 0f) {
				minionStateMachine_.changeState (MinionStateMachine.MinionState.right);
			} else {
				minionStateMachine_.changeState (MinionStateMachine.MinionState.idle);
			}
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
		// perform checks on state change, not continuously
		switch (new_state) {
		case MinionStateMachine.MinionState.jump:
		
			// apply jump force
			float fx = 0f;
			float fy = minionJumpVerticalForce_;

			if (currState_ == MinionStateMachine.MinionState.left) {
				fx = -minionJumpHorizontalForce_;
			} else if (currState_ == MinionStateMachine.MinionState.right) {
				fx = minionJumpHorizontalForce_;
			}

			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (fx, fy));
			break;

		case MinionStateMachine.MinionState.falling:
			// if switching to falling from left or right state, set the horizontal velocity appropriately
			if (currState_ == MinionStateMachine.MinionState.left) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed_, GetComponent<Rigidbody2D> ().velocity.y);
			} else if (currState_ == MinionStateMachine.MinionState.right) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed_, GetComponent<Rigidbody2D> ().velocity.y);
			}
			break;

		}
		currState_ = new_state;
	}
}
