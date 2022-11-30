using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVSN.GameManagment;

namespace DVSN.Plot
{
    public class DialogueCharacter : InteractionComponent
    {
        internal string characterName;

        [SerializeField] internal Dialogue baseDialogue;

        internal bool inDialogue;

        void Start()
        {
            characterName = GetComponent<InteractiveCharacter>().characterName;
        }

        internal override void Interact()
        {
            Managers.Dialogue.StartDialogue(this);
        }

        internal void StartDialogue(GameObject interlocutor)
        {
            if (!inDialogue)
            {
                inDialogue = true;
                Managers.Dialogue.StartDialogue(this);
            }
            else
            {
                EndDialogue();
            }
        }

        internal void EndDialogue()
        {

        }
    }
}
