using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVSN.GameManagment;

namespace DVSN.Plot
{
    public class WorldwideItem : InteractionComponent
    {
        [SerializeField] internal Item item;

        internal override void Interact()
        {
            Managers.Inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}
