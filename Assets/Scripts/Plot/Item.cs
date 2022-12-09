using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVSN.GameManagment;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DVSN.Plot
{
    [CreateAssetMenu(fileName = "Item", menuName = "DVSN/Item")]
    public class Item : PlotElement
    {
        [SerializeField] internal GameObject itemObjectrefab;
        [SerializeField] internal UnlockType unlockType;

        internal Vector3 worldwideSpawnPosition;
        internal string baseSceneName;
        internal string baseOwnerName;

        internal override void Unlock()
        {
            switch(unlockType)
            {
                case UnlockType.INVENTORY:
                    Managers.Inventory.AddItem(this);
                    break;
                case UnlockType.WORLDWIDE: case UnlockType.CHARACTER:
                    Game.Instance.itemsToUnlock.Add(this);
                    Game.Instance.CheckForItemsToUnlock();
                    break;
            }
        }

        internal override void Lock()
        {

        }

        internal enum UnlockType
        {
            INVENTORY = 0,
            WORLDWIDE = 1,
            CHARACTER = 2,
            DROP = 3
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // for other non-HideInInspector fields

            Item script = (Item)target;

            switch (script.unlockType)
            {
                case Item.UnlockType.INVENTORY:
                    break;
                case Item.UnlockType.WORLDWIDE:
                    script.baseSceneName = EditorGUILayout.TextField("Base Scene Name: ", script.baseSceneName);
                    script.worldwideSpawnPosition = EditorGUILayout.Vector3Field("Worldwide Spawn Position:", script.worldwideSpawnPosition);
                    break;
                case Item.UnlockType.CHARACTER:
                    script.baseOwnerName = EditorGUILayout.TextField("Base Owner Name: ", script.baseOwnerName);
                    break;
                case Item.UnlockType.DROP:
                    break;
            }
        }
    }
#endif
}
