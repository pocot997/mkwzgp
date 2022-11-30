using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DVSN.Plot
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "DVSN/Dialogue")]
    public class Dialogue : PlotElement
    {
        [TextArea(3, 10)]
        [SerializeField] internal string sentence;
        [SerializeField] internal List<Response> responses;
        [SerializeField] Talkable baseTalkable;
        [SerializeField] internal bool canExit;

        internal Talkable currentTalkable;

        internal override void Unlock()
        {
            currentTalkable = Talkable.ONCE;
        }

        internal override void Lock()
        {
            currentTalkable = Talkable.LOCKED;
        }

        internal void Reset()
        {
            currentTalkable = baseTalkable;
        }

        [System.Serializable]
        public class Response
        {
            [TextArea(3, 10)]
            [SerializeField] internal string sentence;
            [SerializeField] internal Dialogue nextDialogue;
        }

        internal enum Talkable
        {
            INFINITE = 0,
            ONCE = 1,
            LOCKED = 2
        }
    }
}
