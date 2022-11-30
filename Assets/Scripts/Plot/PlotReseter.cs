using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DVSN.Plot
{
    public class PlotReseter : MonoBehaviour
    {
        [SerializeField] List<Dialogue> allDialogues;

        void Start()
        {
            ResetAllDialogues();
        }

        void ResetAllDialogues()
        {
            foreach (Dialogue dialogue in allDialogues)
            {
                dialogue.Reset();
            }
        }
    }
}
