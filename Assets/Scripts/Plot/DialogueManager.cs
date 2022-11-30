using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DVSN.GameManagment;

namespace DVSN.Plot
{
    public class DialogueManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }

        internal bool inDialogue;
        DialogueCharacter dialogueCharacter;

        [SerializeField] GameObject dialoguePanel;
        [SerializeField] Transform responseContent;
        [SerializeField] TMP_Text dialogueCharacterNameText;
        [SerializeField] TMP_Text dialogueCharacterSentenceText;
        [SerializeField] GameObject dialogueResponseButtonPrefab;

        public void Startup()
        {
            status = ManagerStatus.Started;

            dialoguePanel.SetActive(false);
            inDialogue = false;
        }

        internal void StartDialogue(DialogueCharacter newDialogueCharacter)
        {
            if (!dialoguePanel.activeSelf)
            {
                dialogueCharacter = newDialogueCharacter;

                ToggleDialogueMode(true);

                dialogueCharacter.StartDialogue(Managers.Player.playerObject);
                dialogueCharacterNameText.text = dialogueCharacter.characterName;

                ShowDialogue(dialogueCharacter.baseDialogue);
            }
        }

        internal void ShowDialogue(Dialogue dialogue)
        {
            for(int i = responseContent.childCount - 1; i >= 0 ; i--)
            {
                Destroy(responseContent.GetChild(i).gameObject);
            }

            dialogueCharacterSentenceText.text = dialogue.sentence;

            foreach (Dialogue.Response response in dialogue.responses)
            {
                switch (response.nextDialogue.currentTalkable)
                {
                    case Dialogue.Talkable.INFINITE:
                        AddDialogueButton(delegate { ShowDialogue(response.nextDialogue); }, response.sentence);
                        break;
                    case Dialogue.Talkable.ONCE:
                        AddDialogueButton(delegate { response.nextDialogue.currentTalkable = Dialogue.Talkable.LOCKED;
                                                    ShowDialogue(response.nextDialogue); }, response.sentence);
                        break;
                    case Dialogue.Talkable.LOCKED:
                        break;
                }
            }

            if (dialogue.canExit)
            {
                AddDialogueButton(delegate { EndDialogue(); }, "Koniec");
            }

            dialogue.InterfereWithPlot();
            Managers.Quest.CheckForAllQuests(dialogue);
        }

        void AddDialogueButton(UnityAction call, string responseSentence)
        {
            GameObject button = Instantiate(dialogueResponseButtonPrefab, responseContent);
            button.GetComponent<Button>().onClick.AddListener(call);
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = responseSentence;
        }

        void EndDialogue()
        {
            ToggleDialogueMode(false);

            dialogueCharacter.EndDialogue();
        }

        void ToggleDialogueMode(bool toggle)
        {
            inDialogue = toggle;
            dialoguePanel.SetActive(toggle);
            Managers.Player.FrizPlayer(toggle);
            Cursor.visible = toggle;
            switch(toggle)
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