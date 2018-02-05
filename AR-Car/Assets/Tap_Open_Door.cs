using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;


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
        Scene scene;
        Vector3[] localPositons = new Vector3[3];

        void Start()
        {
            scene = SceneManager.GetActiveScene();

            Debug.Log("Active scene is '" + scene.name + "'.");
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                    if (hit.transform.name == "Audi A6 AR Test")
                    {
                        if (doorsOpen == false)
                        {
                            door_front_r = hit.transform.GetChild(2);
                            door_front_l = hit.transform.GetChild(14);
                            door_back_r = hit.transform.GetChild(8);
                            door_back_l = hit.transform.GetChild(3);


                            localPositons[0] = door_front_r.transform.localPosition;
                            door_front_r.transform.Rotate(0, 0, -50);
                            door_front_r.transform.localPosition = new Vector3(0.26f, 0.02086362f, -0.20f);

                            
                           
                            door_front_l.transform.Rotate(0, 0, 50);
                            //door_front_l.transform.localPosition = new Vector3(0.26f, 0.02086362f, -0.20f);


                            door_back_l.transform.Rotate(0, 0, 50);
                            door_back_r.transform.Rotate(0, 0, -50);
                            localPositons[1] = door_back_r.transform.localPosition;
                            door_back_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.35f);

                            //door_back_r = hit.transform.GetChild(14);
                            //door_back_r.transform.localRotation *= Quaternion.Euler(0, 0, -50);
                            //door_back_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.35f);
                            doorsOpen = true;

                        }
                        else
                        {
                            door_front_r = hit.transform.GetChild(2);
                            door_front_l = hit.transform.GetChild(14);
                            door_back_r = hit.transform.GetChild(8);
                            door_back_l = hit.transform.GetChild(3);


                            door_front_r.transform.Rotate(0, 0, 50);
                            door_front_r.transform.localPosition = localPositons[0];

                           door_front_l = hit.transform.GetChild(14);
                           door_front_l.transform.Rotate(0, 0, -50);


                            door_back_l.transform.Rotate(0, 0, -50);
                            door_back_r.transform.Rotate(0, 0, 50);
                            door_back_r.transform.localPosition = localPositons[1];
                            //door_front_l.transform.localPosition = localPositons[1];

                            //door_back_l = hit.transform.GetChild(8);
                            //door_back_l.transform.localRotation *= Quaternion.Euler(0, 0, -50);
                            //door_back_l.transform.localPosition = new Vector3(0.0f, 0.0f, 0.129f);

                            //door_back_r = hit.transform.GetChild(14);
                            //door_back_r.transform.localRotation *= Quaternion.Euler(0, 0, 50);
                            //door_back_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.05554854f);
                            doorsOpen = false;
                        }
                    }

                    else if (hit.transform.name == "GT86 AR")
                    {
                        door_front_r = hit.transform.GetChild(0);
                        door_front_l = hit.transform.GetChild(1);

                        if (doorsOpen == false)
                        {
                            door_front_r.transform.Rotate(0, 0, 50);
                            door_front_l.transform.Rotate(0, 0, -50);
                            doorsOpen = true;

                        }
                        else
                        {
                            door_front_r.transform.Rotate(0, 0, -50);
                            door_front_l.transform.Rotate(0, 0, 50);
                            doorsOpen = false;
                        }

                    }



                }
            }
        }

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
            Debug.Log("tap event");
            // Ignore?
            if (scene.name != "AR_SCENEsdd")
            {

                Debug.Log("Not AR");
                return;
            }
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
            Debug.Log("got to this point");
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

                if (hit.transform.name == "Audi A6 AR Test")
                {
                    if (doorsOpen == false)
                    {
                        door_front_r = hit.transform.GetChild(2);
                        door_front_l = hit.transform.GetChild(14);
                        door_back_r = hit.transform.GetChild(8);
                        door_back_l = hit.transform.GetChild(3);


                        localPositons[0] = door_front_r.transform.localPosition;
                        door_front_r.transform.Rotate(0, 0, -50);
                        door_front_r.transform.localPosition = new Vector3(0.26f, 0.02086362f, -0.20f);



                        door_front_l.transform.Rotate(0, 0, 50);
                        //door_front_l.transform.localPosition = new Vector3(0.26f, 0.02086362f, -0.20f);


                        door_back_l.transform.Rotate(0, 0, 50);
                        door_back_r.transform.Rotate(0, 0, -50);
                        localPositons[1] = door_back_r.transform.localPosition;
                        door_back_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.35f);

                        //door_back_r = hit.transform.GetChild(14);
                        //door_back_r.transform.localRotation *= Quaternion.Euler(0, 0, -50);
                        //door_back_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.35f);
                        doorsOpen = true;

                    }
                    else
                    {
                        door_front_r = hit.transform.GetChild(2);
                        door_front_l = hit.transform.GetChild(14);
                        door_back_r = hit.transform.GetChild(8);
                        door_back_l = hit.transform.GetChild(3);


                        door_front_r.transform.Rotate(0, 0, 50);
                        door_front_r.transform.localPosition = localPositons[0];

                        door_front_l = hit.transform.GetChild(14);
                        door_front_l.transform.Rotate(0, 0, -50);


                        door_back_l.transform.Rotate(0, 0, -50);
                        door_back_r.transform.Rotate(0, 0, 50);
                        door_back_r.transform.localPosition = localPositons[1];
                        //door_front_l.transform.localPosition = localPositons[1];

                        //door_back_l = hit.transform.GetChild(8);
                        //door_back_l.transform.localRotation *= Quaternion.Euler(0, 0, -50);
                        //door_back_l.transform.localPosition = new Vector3(0.0f, 0.0f, 0.129f);

                        //door_back_r = hit.transform.GetChild(14);
                        //door_back_r.transform.localRotation *= Quaternion.Euler(0, 0, 50);
                        //door_back_r.transform.localPosition = new Vector3(-0.164994f, 0.02086362f, -0.05554854f);
                        doorsOpen = false;
                    }
                }

                else if (hit.transform.name == "GT86 AR")
                {
                    door_front_r = hit.transform.GetChild(0);
                    door_front_l = hit.transform.GetChild(1);

                    if (doorsOpen == false)
                    {
                        door_front_r.transform.Rotate(0, 0, 50);
                        door_front_l.transform.Rotate(0, 0, -50);
                        doorsOpen = true;

                    }
                    else
                    {
                        door_front_r.transform.Rotate(0, 0, -50);
                        door_front_l.transform.Rotate(0, 0, 50);
                        doorsOpen = false;
                    }

                }


            }
        }

        
        
    }
}