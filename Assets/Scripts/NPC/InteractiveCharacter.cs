using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DVSN.Plot
{
    [RequireComponent(typeof(DialogueCharacter))]
    public class InteractiveCharacter : MonoBehaviour
    {
        [SerializeField] internal string characterName;

        internal DialogueCharacter dialogueCharacter;
        internal List<Item> inventory = new List<Item>();
    }
}
