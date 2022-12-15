using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVSN.Player;
using DVSN.Plot;
using DVSN.Spellcasting;

namespace DVSN.GameManagment
{
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(SpellcastingManager))]
    [RequireComponent(typeof(QuestManager))]
    [RequireComponent(typeof(DialogueManager))]
    [RequireComponent(typeof(InventoryManager))]
    [RequireComponent (typeof(SongManager))]
    [RequireComponent(typeof (BattleLoaderManager))]
    public class Managers : MonoBehaviour
    {
        public static PlayerManager Player { get; private set; }
        public static SpellcastingManager Spellcasting { get; private set; }
        public static QuestManager Quest { get; private set; }
        public static DialogueManager Dialogue { get; private set; }
        public static InventoryManager Inventory { get; private set; }
        public static SongManager Song { get; private set; }
        public static BattleLoaderManager BattleLoader { get; private set; }

        public static bool allLoaded { get; private set; }

        private List<IGameManager> _startSequence;

        private void Awake()
        {
            allLoaded = false;
            Player = GetComponent<PlayerManager>();
            Spellcasting = GetComponent<SpellcastingManager>();
            Quest = GetComponent<QuestManager>();
            Dialogue = GetComponent<DialogueManager>();
            Inventory = GetComponent<InventoryManager>();
            Song = GetComponent<SongManager>();
            BattleLoader = GetComponent<BattleLoaderManager>();

            _startSequence = new List<IGameManager>();

            _startSequence.Add(Player);
            _startSequence.Add(Spellcasting);
            _startSequence.Add(Quest);
            _startSequence.Add(Dialogue);
            _startSequence.Add(Inventory);
            _startSequence.Add(Song);
            _startSequence.Add(BattleLoader);

            StartCoroutine(StartupManagers());
        }

        private IEnumerator StartupManagers()
        {
            foreach (IGameManager manager in _startSequence)
            {
                manager.Startup();
            }

            yield return null;

            int numModels = _startSequence.Count;
            int numReady = 0;

            while (numReady < numModels)
            {
                int lastReady = numReady;
                numReady = 0;

                foreach (IGameManager manager in _startSequence)
                {
                    if (manager.status == ManagerStatus.Started)
                        numReady++;
                }

                yield return null;
            }
            allLoaded = true;
        }
    }
}
