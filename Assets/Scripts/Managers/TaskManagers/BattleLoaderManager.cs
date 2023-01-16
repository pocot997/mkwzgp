using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoaderManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] List<GameObject> mainSceneElements = new List<GameObject>();
    [SerializeField] List<GameObject> battleSceneElements = new List<GameObject>();

    [SerializeField] internal CombatEnemy currentEnemy;
    internal float enemyHP;
    internal bool isInBattle = false;

    public void Startup()
    {
        enemyHP = 100;
        foreach (GameObject obj in mainSceneElements)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in battleSceneElements)
        {
            obj.SetActive(false);
        }

        CombatEnemy.onEnterCombat += ChangeToBattleMap;
        NoteInstanciatorManager.onFinishBattle += ChangeToMainMap;
        status = ManagerStatus.Started;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeToBattleMap();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeToMainMap();
        }
    }

    internal void ChangeToBattleMap(CombatEnemy enemy = null)
    {
        if (enemy != null)
        {
            currentEnemy = enemy;
            enemyHP = enemy.HitPoints;
        }

        ToggleElements();
    }
    
    void ChangeToMainMap()
    {
        ToggleElements();
    }

    void ToggleElements()
    {
        foreach(GameObject obj in mainSceneElements)
        {
            obj.SetActive(!obj.activeSelf);
        }

        foreach(GameObject obj in battleSceneElements)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
