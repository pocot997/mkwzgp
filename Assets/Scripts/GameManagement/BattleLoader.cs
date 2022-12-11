using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoader : MonoBehaviour
{
    [SerializeField] GameObject sceneCamera;
    [SerializeField] GameObject manager;

    [SerializeField] GameObject player;
    List<GameObject> enemies;

    CombatEnemy currentEnemy;
    float enemyHP;

    void Start()
    {
        CombatEnemy.onEnterCombat += ChangeToBattleMap;
        NoteInstanciatorManager.onFinishBattle += ChangeToMainMap;
    }

    void ChangeToBattleMap(CombatEnemy enemy)
    {
        currentEnemy = enemy;
        enemyHP = enemy.HitPoints;
        enemies = FindActiveEnemiesOnScene();
        LoadBattleMap();
        SetActiveSceneElements(false);
    }
    
    void ChangeToMainMap()
    {
        SetActiveSceneElements(true);
        UnloadBattleMap();
    }

    void SetActiveSceneElements(bool toggle)
    {
        sceneCamera.SetActive(toggle);
        manager.SetActive(toggle);
        player.SetActive(toggle);
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(toggle);
        }
    }

    List<GameObject> FindActiveEnemiesOnScene()
    {
        List<GameObject> validTransforms = new List<GameObject>();
        CombatEnemy[] objs = Resources.FindObjectsOfTypeAll<CombatEnemy>() as CombatEnemy[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                validTransforms.Add(objs[i].gameObject);
            }
        }
        return validTransforms;
    }

    void LoadBattleMap()
    {
        SceneManager.LoadScene("BattleArena", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleArena"));
    }

    void UnloadBattleMap()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
        SceneManager.UnloadSceneAsync("BattleArena");
    }
}
