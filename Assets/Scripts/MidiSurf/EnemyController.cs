using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class EnemyController : MonoBehaviour
{
    private void Start()
    {
        NoteInstanciatorManager.onTrackChanged += MoveEnemyPosition;
    }

    void MoveEnemyPosition(int trackNumber)
    {
        switch (trackNumber)
        {
            case 0:
                transform.position =  (Vector3.right * MidiManagers.globalPathCoordinates.leftTrackShiftX) + new Vector3(0, 0.2f, 8);
                break;
            case 1:
                transform.position = new Vector3(0, 0.2f, 8);
                break;
            case 2:
                transform.position = (Vector3.right * MidiManagers.globalPathCoordinates.rightTrackShiftX) + new Vector3(0, 0.2f, 8);
                break;
        }
    }
}