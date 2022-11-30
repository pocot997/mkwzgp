using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DVSN.Plot
{
    [CreateAssetMenu(fileName = "Quest", menuName = "DVSN/Quest/Item")]
    public class ItemQuest : Quest
    {
        [SerializeField] Item finishingItem;

        internal override void Check(PlotElement plotElement)
        {
            try
            {
                Item item = (Item)plotElement;

                if (item == finishingItem)
                {
                    FinishQuest();
                }
            }
            catch { };
        }
    }
}
