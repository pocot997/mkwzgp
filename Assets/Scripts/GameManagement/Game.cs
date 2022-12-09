using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using DVSN.Plot;

namespace DVSN.GameManagment
{
    public class Game : MonoBehaviour
    {
        internal static Game Instance { get; private set; }

        internal List<InteractiveCharacter> sceneCharacters = new List<InteractiveCharacter>();
        internal List<Item> itemsToUnlock = new List<Item>();

        Scene mainScene;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            mainScene = SceneManager.GetActiveScene();
        }

        internal void CheckForItemsToUnlock()
        {
            foreach (Item item in itemsToUnlock.ToList())
            {
                switch(item.unlockType)
                {
                    case Item.UnlockType.INVENTORY:
                        break;
                    case Item.UnlockType.WORLDWIDE:
                        if(item.baseSceneName == mainScene.name)
                        {
                            GameObject worldwideItem = Instantiate(item.itemObjectrefab, item.worldwideSpawnPosition, Quaternion.identity);
                            worldwideItem.GetComponent<WorldwideItem>().item = item;
                            itemsToUnlock.Remove(item);
                        }
                        break;
                    case Item.UnlockType.CHARACTER:
                        foreach (InteractiveCharacter character in sceneCharacters)
                        {
                            if(item.baseOwnerName == character.characterName)
                            {
                                character.inventory.Add(item);
                                itemsToUnlock.Remove(item);
                            }
                        }
                        break;
                    case Item.UnlockType.DROP:
                        break;
                }
            }
        }
    }
}
