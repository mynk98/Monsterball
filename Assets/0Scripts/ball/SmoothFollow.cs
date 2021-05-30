using UnityEngine;

#pragma warning disable 649
namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 5.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		public float height = 5.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;
		[SerializeField]
		int mouseYSensitivity=50;
		int Switch = 0;

		// Use this for initialization
		void Start()
        {
		}

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			// Calculate the current rotation angles
			var wantedRotationAngle = target.eulerAngles.y;

			var currentRotationAngle = transform.eulerAngles.y;
			//var currentHeight = transform.position.y;
			
			if(Input.GetAxis("Change View") != 0 && Switch==0)
            {
				Switch = 1;
				distance += 1;
				if (distance >= 8)
                {
					distance = 3;
                }
            }
			if (Input.GetAxis("Change View") == 0)
			{
				Switch = 0;
			}

			height -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseYSensitivity;
			if (height > 5)
            {
				height = 5;
            }
			else if (height < 0.5f)
            {
				height = 0.5f;
            }
			var wantedHeight = target.position.y + height;


			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			//currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);


			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x ,wantedHeight /*+currentHeight*/ , transform.position.z);

			// Always look at the target
			transform.LookAt(target);
		}
	}
}