using DVSN.GameManagment;
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
        [SerializeField] internal GameObject respawnCanvas;
        [SerializeField] internal GameObject startCanvas;

        PlayerMovement playerMovement;
        PlayerLook playerLook;

        internal int level;
        internal int experience;

        Vector3 respawnPosition;
        Vector3 respawnRotation;

        public void Startup()
        {
            status = ManagerStatus.Started;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerLook = playerObject.GetComponent<PlayerLook>();

            respawnPosition = playerObject.transform.position;
            respawnRotation = playerObject.transform.rotation.eulerAngles;

            respawnCanvas.SetActive(false);
            startCanvas.SetActive(true);

            StartCoroutine(HideCanvas(startCanvas));
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

        internal void RestartGame()
        {
            respawnCanvas.SetActive(true);
            playerObject.transform.position = respawnPosition;
            playerObject.transform.rotation = Quaternion.Euler(respawnRotation);
            CombatPlayer player = Managers.Player.playerObject.GetComponent<CombatPlayer>();
            player.HitPoints = 100;
            StartCoroutine(HideCanvas(respawnCanvas));
        }

        IEnumerator HideCanvas(GameObject canvas)
        {
            yield return new WaitForSeconds(5);
            canvas.SetActive(false);
        }
    }
}
