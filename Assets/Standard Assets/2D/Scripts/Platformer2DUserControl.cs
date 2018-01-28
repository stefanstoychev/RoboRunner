using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        public bool crouchEnabled = true;
        public bool autoRunRight = true;

        public float autoRunSpeed = 0.5f;
        public bool stoped = false;

        public int secondsToRespawn = 3;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {


            bool crouch = false;
            // Read the inputs.
            if (crouchEnabled)
                crouch = Input.GetKey(KeyCode.LeftControl);

            float h = autoRunSpeed;
            if(!autoRunRight)
                h = CrossPlatformInputManager.GetAxis("Horizontal");

            if (stoped)
                h = 0;

            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Finish")
            {
                stoped = true;

                StartCoroutine(WaitForIt(secondsToRespawn));
            }
        }

        IEnumerator WaitForIt(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
