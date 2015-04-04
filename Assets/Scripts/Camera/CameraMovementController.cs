using UnityEngine;
using System.Collections;

public class CameraMovementController : MonoBehaviour
{

		public float cameraTrackSpeed_ = 0.1f;
		public float trackingEpsilon_ = 0.01f;
		public GameObject targetObject_ = null;

		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void LateUpdate ()
		{
	
				// decide if/what we should be tracking
				Vector3 target_position;
				if (targetObject_) {

						// set the target position but ensure we don't change the z value
						target_position = targetObject_.transform.position;
						target_position.z = transform.position.z;
						
						// are we already at the position, give or take?
						if ((Mathf.Abs ((target_position - transform.position).x) > trackingEpsilon_) ||
								(Mathf.Abs ((target_position - transform.position).y) > trackingEpsilon_)) {

								// move to current target position using linear interpolation
								Debug.Log ("Camera Tracking object: " + targetObject_.name);
								transform.position = Vector3.Lerp (transform.position, target_position, cameraTrackSpeed_);
						}
				}
		}
}
