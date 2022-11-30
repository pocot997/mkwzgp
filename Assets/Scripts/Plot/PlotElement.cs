using System.Collections.Generic;
using UnityEngine;
using DVSN.GameManagment;

namespace DVSN.Plot
{
    public abstract class PlotElement : ScriptableObject
    {
        [SerializeField] List<PlotElement> elementsToUnlock;
        [SerializeField] List<PlotElement> elementsToLock;

        internal abstract void Unlock();

        internal abstract void Lock();

        internal void InterfereWithPlot()
        {
            foreach(PlotElement interactionElement in elementsToUnlock)
            {
                interactionElement.Unlock();
            }

            foreach (PlotElement interactionElement in elementsToLock)
            {
                interactionElement.Lock();
            }
        }
    }
}
