using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DVSN.Plot
{
    public class PlotReseter : MonoBehaviour
    {
        [SerializeField] List<Dialogue> allDialogues;
        [SerializeField] List<KillQuest> allQuests;

        void Start()
        {
            ResetAllDialogues();
            ResetAllQuests();
        }

        void ResetAllDialogues()
        {
            foreach (Dialogue dialogue in allDialogues)
            {
                dialogue.Reset();
            }
        }

        void ResetAllQuests()
        {
            foreach (KillQuest quest in allQuests)
            {
                quest.Reset();
            }
        }
    }
}
