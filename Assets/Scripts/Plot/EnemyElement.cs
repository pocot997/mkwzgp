using DVSN.GameManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DVSN.Plot
{
    [CreateAssetMenu(fileName = "EnemyElement", menuName = "DVSN/EnemyElement")]
    public class EnemyElement : PlotElement
    {
        [SerializeField] internal GameObject enemyPrefab;

        internal override void Unlock()
        {
            Instantiate(enemyPrefab);
        }

        internal override void Lock()
        {
            GameObject enemy = GameObject.Find(enemyPrefab.name);
            if(enemy!= null)
            {
                Destroy(enemy);
            }
        }
    }
}
