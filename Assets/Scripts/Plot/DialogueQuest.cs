using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DVSN.Plot
{
    [CreateAssetMenu(fileName = "Quest", menuName = "DVSN/Quest/Dialogue")]
    public class DialogueQuest : Quest
    {
        [SerializeField] Dialogue finishingDialogue;

        internal override void Check(PlotElement plotElement)
        {
            try
            {
                Dialogue dialogue = (Dialogue)plotElement;

                if (dialogue == finishingDialogue)
                {
                    FinishQuest();
                }
            }
            catch { };
        }
    }
}
