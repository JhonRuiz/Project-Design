using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Lean.Touch
{
	// This script calls the OnFingerTap event when a finger taps the screen
	public class Tap_Open_Door_Copy : MonoBehaviour
	{
        public Texture[] textures;
        public int currentTexture;
        private bool headlightsOn = false;
        public Color oldMaterial;
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
            if (tappedObject.name == "Front Headlights")
            {
                OnFingerTap.Invoke(finger);
                Debug.Log("touched");

                if (headlightsOn == false)
                {
                    headlightsOn = true;
                    oldMaterial = GetComponent<Renderer>().material.color;
                    GetComponent<Renderer>().material.color = Color.yellow;

                }
                else
                {
                    headlightsOn = false;

                    GetComponent<Renderer>().material.color = oldMaterial;
                }
            }
            
            
        }

        
        
    }
}