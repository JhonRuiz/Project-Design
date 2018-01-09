using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Lean.Touch
{
	// This script calls the OnFingerTap event when a finger taps the screen
	public class Tap_Open_Door : MonoBehaviour
	{
        private bool doorsOpen = false;
		// Event signature
		[System.Serializable] public class LeanFingerEvent : UnityEvent<LeanFinger> {}

		[Tooltip("If the finger is over the GUI, ignore it?")]
		public bool IgnoreIfOverGui;

		[Tooltip("If the finger started over the GUI, ignore it?")]
		public bool IgnoreIfStartedOverGui;

		[Tooltip("How many times must this finger tap before OnFingerTap gets called? (0 = every time)")]
		public int RequiredTapCount = 0;

		[Tooltip("How many times repeating must this finger tap before OnFingerTap gets called? (e.g. 2 = 2, 4, 6, 8, etc) (0 = every time)")]
		public int RequiredTapInterval;

		public LeanFingerEvent OnFingerTap;
        public Transform door_front_l;
        public Transform door_front_r;
        public Transform door_back_l;
        public Transform door_back_r;

        protected virtual void OnEnable()
		{
			// Hook events
			LeanTouch.OnFingerTap += FingerTap;
		}

		protected virtual void OnDisable()
		{
			// Unhook events
			LeanTouch.OnFingerTap -= FingerTap;
		}

		private void FingerTap(LeanFinger finger)
		{
			// Ignore?
			if (IgnoreIfOverGui == true && finger.IsOverGui == true)
			{
				return;
			}

			if (IgnoreIfStartedOverGui == true && finger.StartedOverGui == true)
			{
				return;
			}

			if (RequiredTapCount > 0 && finger.TapCount != RequiredTapCount)
			{
				return;
			}

			if (RequiredTapInterval > 0 && (finger.TapCount % RequiredTapInterval) != 0)
			{
				return;
			}

            // Call event
            /* Find out which object was tapped */
            Ray screenRay = Camera.main.ScreenPointToRay(finger.ScreenPosition);
            GameObject tappedObject = null;
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit))
            {
                tappedObject = hit.collider.gameObject;
            }
            Debug.Log(tappedObject);
            if (tappedObject != null)
            {
                OnFingerTap.Invoke(finger);
                Debug.Log("touched");

                if (doorsOpen == false)
                {
                    door_front_l = this.gameObject.transform.GetChild(2);
                    door_front_l.transform.localRotation *= Quaternion.Euler(0, 0, 50);

                    door_front_r = this.gameObject.transform.GetChild(3);
                    door_front_r.transform.localRotation *= Quaternion.Euler(0, 0, -50);
                    door_front_r.transform.localPosition = new Vector3(0.26f, 0.02086362f, -0.20f);

                    door_back_l = this.gameObject.transform.GetChild(4);
                    door_back_l.transform.localRotation *= Quaternion.Euler(0, 0, 50);
                    //door_back_l.transform.localPosition = new Vector3(0.0f, 0.0f, 0.129f);

                    door_back_r = this.gameObject.transform.GetChild(5);
                    door_back_r.transform.localRotation *= Quaternion.Euler(0, 0, -50);
                    door_back_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.35f);
                    doorsOpen = true;

                }
                else
                {
                    door_front_l = this.gameObject.transform.GetChild(2);
                    door_front_l.transform.localRotation *= Quaternion.Euler(0, 0, -50);

                    door_front_r = this.gameObject.transform.GetChild(3);
                    door_front_r.transform.localRotation *= Quaternion.Euler(0, 0, 50);
                    door_front_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.05554854f);

                    door_back_l = this.gameObject.transform.GetChild(4);
                    door_back_l.transform.localRotation *= Quaternion.Euler(0, 0, -50);
                    //door_back_l.transform.localPosition = new Vector3(0.0f, 0.0f, 0.129f);

                    door_back_r = this.gameObject.transform.GetChild(5);
                    door_back_r.transform.localRotation *= Quaternion.Euler(0, 0, 50);
                    door_back_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.05554854f);
                    doorsOpen = false;
                }
            }
            
            
        }

        
        
    }
}