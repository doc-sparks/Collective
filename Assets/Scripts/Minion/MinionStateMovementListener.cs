using UnityEngine;
using System.Collections;

public class MinionStateMovementListener : MonoBehaviour
{

		MinionStateMachine.MinionState currState_;
		public float moveSpeed_ = 1f;
		private Animator minionAnimator_;

		// Use this for initialization
		void Start ()
		{
				// current state
				currState_ = MinionStateMachine.MinionState.idle;

				// grab the Minion animator
				minionAnimator_ = GetComponent<Animator> ();
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
