using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

namespace DVSN.Player
{
    public class PlayerManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }

        [SerializeField] internal GameObject playerObject;

        PlayerMovement playerMovement;
        PlayerLook playerLook;

        internal int level;
        internal int experience;

        public void Startup()
        {
            status = ManagerStatus.Started;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerLook = playerObject.GetComponent<PlayerLook>();
        }

        void Update()
        {
            if(Input.GetKey(KeyCode.A))
            {
                //playerObject.transform.Translate(new Vector3(-0.03f, 0, 0));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //playerObject.transform.Translate(new Vector3(0.03f, 0, 0));
            }
        }

        internal void FrizPlayer(bool toFriz)
        {
            playerMovement.enabled = !toFriz;
            playerLook.enabled = !toFriz;
        }
    }
}
