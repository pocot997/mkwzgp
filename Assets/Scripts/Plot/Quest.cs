using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVSN.GameManagment;

namespace DVSN.Plot
{
    public abstract class Quest : PlotElement
    {
        [SerializeField] internal string questName;
        [TextArea(3, 10)]
        [SerializeField] internal string description;
        [SerializeField] int experienceReward;

        internal abstract void Check(PlotElement plotElement);

        internal override void Unlock()
        {
            Managers.Quest.activeQuests.Add(this);
        }

        internal override void Lock()
        {
            FailQuest();
            Managers.Quest.activeQuests.Remove(this);
        }

        protected void FinishQuest()
        {
            Managers.Player.experience += experienceReward;

            InterfereWithPlot();
        }

        void FailQuest()
        {

        }
    }
}
