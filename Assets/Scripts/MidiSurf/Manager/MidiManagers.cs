using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ReferenceHolder))]
[RequireComponent(typeof(GlobalPathCoordinates))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent (typeof(AudioManager))]
[RequireComponent(typeof(NoteInstanciatorManager))]
public class MidiManagers : MonoBehaviour
{
    public static ReferenceHolder referenceHolder { get; private set; }
    public static GlobalPathCoordinates globalPathCoordinates { get; private set; }
    public static ScoreManager score { get; private set; }
    public static AudioManager gameAudio { get; private set; }
    public static NoteInstanciatorManager noteInstanciator { get; private set; }
    public static bool allLoaded { get; private set; }

    private List<ManagerInterface> _startSequence;


    private void Awake()
    {
        allLoaded = false;
        referenceHolder = GetComponent<ReferenceHolder>();
        globalPathCoordinates = GetComponent<GlobalPathCoordinates>();
        score = GetComponent<ScoreManager>();
        gameAudio = GetComponent<AudioManager>();
        noteInstanciator = GetComponent<NoteInstanciatorManager>();

        _startSequence = new List<ManagerInterface>();

        _startSequence.Add(referenceHolder);
        _startSequence.Add(globalPathCoordinates);
        _startSequence.Add(score);
        _startSequence.Add(gameAudio);
        _startSequence.Add(noteInstanciator);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (ManagerInterface manager in _startSequence)
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

            foreach (ManagerInterface manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                    numReady++;
            }

            yield return null;
        }
        allLoaded = true;
    }
}
