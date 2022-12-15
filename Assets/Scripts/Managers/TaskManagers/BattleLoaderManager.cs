using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoaderManager : MonoBehaviour, IGameManager
{
    //[SerializeField] GameObject sceneCamera;
    //[SerializeField] GameObject manager;

    //[SerializeField] GameObject playerSetup;
    //List<GameObject> enemies = new List<GameObject>();
    public ManagerStatus status { get; private set; }

    [SerializeField] List<GameObject> mainSceneElements = new List<GameObject>();
    [SerializeField] List<GameObject> battleSceneElements = new List<GameObject>();

    internal CombatEnemy currentEnemy;
    internal float enemyHP;

    public void Startup()
    {
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

    void ChangeToBattleMap(CombatEnemy enemy = null)
    {
        if (enemy != null)
        {
            currentEnemy = enemy;
            enemyHP = enemy.HitPoints;
        }

        //AddActiveEnemiesOnScene();

        //LoadBattleMap();
        ToggleElements();
    }
    
    void ChangeToMainMap()
    {
        ToggleElements();
       // UnloadBattleMap();
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

    //void AddActiveEnemiesOnScene()
    //{
    //    //List<GameObject> validTransforms = new List<GameObject>();
    //    CombatEnemy[] objs = Resources.FindObjectsOfTypeAll<CombatEnemy>() as CombatEnemy[];
    //    for (int i = 0; i < objs.Length; i++)
    //    {
    //        if (objs[i].hideFlags == HideFlags.None)
    //        {
    //            mainSceneElements.Add(objs[i].gameObject);
    //        }
    //    }
    //}

    //void LoadBattleMap()
    //{
    //    SceneManager.LoadScene("BattleArena", LoadSceneMode.Additive);
    //    SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleArena"));
    //}

    //void UnloadBattleMap()
    //{
    //    SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
    //    SceneManager.UnloadSceneAsync("BattleArena");
    //}
}
