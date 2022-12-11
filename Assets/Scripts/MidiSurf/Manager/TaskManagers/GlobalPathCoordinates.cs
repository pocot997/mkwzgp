using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPathCoordinates : MonoBehaviour, ManagerInterface
{
    public ManagerStatus status { get; private set; }

    public float leftTrackShiftX;
    public float rightTrackShiftX;
    public float trackShiftY;


    public void Startup()
    {
        status = ManagerStatus.Started;
    }
}
