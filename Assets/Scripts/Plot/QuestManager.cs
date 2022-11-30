using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DVSN.GameManagment;

namespace DVSN.Plot
{
    public class QuestManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }

        internal List<Quest> activeQuests = new List<Quest>();

        [SerializeField] GameObject activeQuestPanel;
        [SerializeField] Transform activeQuestContent;
        [SerializeField] GameObject questButtonPrefab;
        [SerializeField] GameObject questDetailsPanel;
        [SerializeField] TMP_Text questNameText;
        [SerializeField] TMP_Text questDescriptionText;

        public void Startup()
        {
            status = ManagerStatus.Started;

            activeQuestPanel.SetActive(false);
            questDetailsPanel.SetActive(false);
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                ShowActiveQuests();
            }
        }

        internal void CheckForAllQuests(PlotElement plotElement)
        {
            foreach(Quest quest in activeQuests)
            {
                quest.Check(plotElement);
            }
        }

        void ShowActiveQuests()
        {
            if (!activeQuestPanel.activeSelf)
            {
                ToggleDialogueMode(true);

                activeQuestPanel.SetActive(true);
                questDetailsPanel.SetActive(false);

                for (int i = activeQuestContent.childCount - 1; i >= 0; i--)
                {
                    Destroy(activeQuestContent.GetChild(i).gameObject);
                }

                foreach (Quest quest in activeQuests)
                {
                    AddQuestButton(delegate { ShowQuestDetails(quest); }, quest.questName);
                }
            }
            else
            {
                ToggleDialogueMode(false);
                activeQuestPanel.SetActive(false);
                questDetailsPanel.SetActive(false);
            }
        }

        void ShowQuestDetails(Quest quest)
        {
            questDetailsPanel.SetActive(true);
            questNameText.text = quest.questName;
            questDescriptionText.text = quest.description;
        }

        void AddQuestButton(UnityAction call, string questName)
        {
            GameObject button = Instantiate(questButtonPrefab, activeQuestContent);
            button.GetComponent<Button>().onClick.AddListener(call);
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = questName;
        }

        void ToggleDialogueMode(bool toggle)
        {
            Managers.Player.FrizPlayer(toggle);
            Cursor.visible = toggle;
            switch (toggle)
            {
                case true:
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case false:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
            }
        }
    }
}
