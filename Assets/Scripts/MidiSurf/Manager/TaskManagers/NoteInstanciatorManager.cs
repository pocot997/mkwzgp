using DVSN.GameManagment;
using DVSN.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NoteInstanciatorManager : MonoBehaviour, ManagerInterface
{
    public ManagerStatus status { get; private set; }

    [SerializeField] List<GameObject> notes;
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private float spawnDelay = 2.3f;
    [SerializeField] private float noteInstantiationHight = 0.1f;

    internal List <float> noteSpawningWithDelay = new List<float> ();

    private int lastTrackId = 0;
    internal int newTrackId = 1;
    private float lastTrackProbability = 90f;
    private float drawnprobability = 0f;
    int maxCounter = 30;
    int counter = 0;

    [SerializeField] float noteInstantiationCooldown = 1f;
    float lastNoteInstantiationTime = 0f;

    [SerializeField] Transform noteOffset;

    internal delegate void TrackChanged(int trackId);
    internal static event TrackChanged onTrackChanged;

    internal delegate void FinishBattle();
    internal static event FinishBattle onFinishBattle;

    [SerializeField] Slider playerHealthBar;
    [SerializeField] Slider enemyHealthBar;

    bool secendEnable = false;

    void ChangeTrack()
    {
        if (onTrackChanged != null)
        {
            onTrackChanged(newTrackId);
        }
    }
    
    void FinishBattleWithPlayer()
    {
        if (onFinishBattle != null)
        {
            onFinishBattle();
        }
    }

    public void Startup()
    {
        SongManager.onNotesReady += NoteInstantioation;
        CombatEnemy.onEnterCombat += EnableCombat;
        ChangeTrack();
        status = ManagerStatus.Started;
    }

    void EnableCombat(CombatEnemy enemy = null)
    {
        if(secendEnable)
        {
            StartCoroutine(NoteController());
        }
    }

    internal void InstantiateNote()
    {
        if (lastNoteInstantiationTime + noteInstantiationCooldown > Time.fixedTime)
        {
            return;
        }

        lastNoteInstantiationTime = Time.fixedTime;
        int currentTrack = newTrackId;

        NoteInstantioation(currentTrack);

        drawnprobability = UnityEngine.Random.Range(0, 100);
        if (drawnprobability <= lastTrackProbability)
        {
            newTrackId = currentTrack;
            lastTrackProbability -= 10;
        }
        else
        {
            newTrackId = UnityEngine.Random.Range(0, 3);
            ChangeTrack();
            lastTrackId = currentTrack;
            lastTrackProbability = 90;
        }
    }

    internal void NoteInstantioation(int trackNumber)
    {
        int noteID = Random.Range(0, notes.Count);
        switch(trackNumber)
        {
            case 0:
                Instantiate(notes[noteID], this.transform.localPosition + (Vector3.right * MidiManagers.globalPathCoordinates.leftTrackShiftX) + new Vector3(0, noteInstantiationHight, 8), this.transform.rotation, noteOffset);
                break;
            case 1:
                Instantiate(notes[noteID], this.transform.localPosition + new Vector3(0, noteInstantiationHight, 8), this.transform.rotation, noteOffset);
                break;
            case 2:
                Instantiate(notes[noteID], this.transform.localPosition + (Vector3.right * MidiManagers.globalPathCoordinates.rightTrackShiftX) + new Vector3(0, noteInstantiationHight, 8), this.transform.rotation, noteOffset);
                break;
        }
    }

    public void NoteInstantioation(List<float> noteSpawningTimes)
    {
        //noteSpawningTimes.RemoveRange(0, 2);
        foreach (float spawningTime in noteSpawningTimes)
        {
            noteSpawningWithDelay.Add(spawningTime - spawnDelay);
        }

        StartCoroutine(NoteController());
    }

    private IEnumerator NoteController()
    {
        CombatEnemy currentEnemy = Managers.BattleLoader.currentEnemy;
        float currentEnemyHP = Managers.BattleLoader.enemyHP;
        CombatPlayer player = Managers.Player.playerObject.GetComponent<CombatPlayer>();
       

        while (/*noteSpawningWithDelay.Count > 0*/ Managers.BattleLoader.enemyHP > 0 && player.HitPoints > 0)
        {
            enemyHealthBar.value = Managers.BattleLoader.enemyHP;
            playerHealthBar.value = player.HitPoints;

            if (noteSpawningWithDelay[counter] <= MidiManagers.gameAudio.audioSource.time)
            {
                counter++;
                player.HitPoints = 100;
                InstantiateNote();
                //noteSpawningWithDelay.RemoveAt(0);
                if (counter > maxCounter)
                {
                    counter = 0;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        for (int i = noteOffset.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(noteOffset.transform.GetChild(i).gameObject);
        }

        secendEnable = true;

        if (player.HitPoints <= 0)
        {
            // player lose here. DOnt know what to do
        }

        FinishBattleWithPlayer();
    }
}
