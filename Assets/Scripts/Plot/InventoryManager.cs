using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVSN.GameManagment;

namespace DVSN.Plot
{
    public class InventoryManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }

        [SerializeField] List<Item> inventory = new List<Item>();

        public void Startup()
        {
            status = ManagerStatus.Started;
        }

        internal void AddItem(Item item)
        {
            inventory.Add(item);
            item.InterfereWithPlot();
            Managers.Quest.CheckForAllQuests(item);
        }
    }
}
