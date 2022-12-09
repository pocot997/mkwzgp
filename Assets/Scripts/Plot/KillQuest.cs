using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVSN.Enemy;
using System;

namespace DVSN.Plot
{
    [CreateAssetMenu(fileName = "KillQuest", menuName = "DVSN/Quest/Kill")]
    public class KillQuest : Quest
    {
        [SerializeField] EnemyType killEnemyType;
        [SerializeField] int numberOfEnemiesToKill;

        int killedEnemies;

        internal void Reset()
        {
            killedEnemies = 0;
        }

        internal override void Check(PlotElement plotElement)
        {
            try
            {
                EnemyElement enemyElement = plotElement as EnemyElement;
                if (enemyElement.enemyPrefab.GetComponent<EnemyAi>().enemyType == killEnemyType)
                {
                    killedEnemies++;
                    if (killedEnemies >= numberOfEnemiesToKill)
                    {
                        FinishQuest();
                    }
                }
            }
            catch(NullReferenceException)
            { }
        }
    }
}
