using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderControls : MonoBehaviour
{
    Vector3 middleTrackPlayerPosition;
    bool objectInCollider = false;
    NoteController collisionObject;    
      
    void Start()
    { 
        middleTrackPlayerPosition = transform.position;
        middleTrackPlayerPosition.y = MidiManagers.globalPathCoordinates.trackShiftY;
    }

    void Update()
    {
        MoveCollider();
        CollectNote();
    }

    void MoveCollider()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            transform.position = middleTrackPlayerPosition;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(MidiManagers.globalPathCoordinates.leftTrackShiftX, middleTrackPlayerPosition.y, middleTrackPlayerPosition.z);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(MidiManagers.globalPathCoordinates.rightTrackShiftX, middleTrackPlayerPosition.y, middleTrackPlayerPosition.z);
        }
        else
        {
            transform.position = middleTrackPlayerPosition;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NoteController>() != null)
        {
            objectInCollider = true;
            collisionObject = other.GetComponent<NoteController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NoteController>() != null)
        {
            objectInCollider = false;
        }
    }

    void CollectNote()
    {
        if(!objectInCollider)
        {
            return;
        }

        if (Input.GetKey(KeyCode.J) && collisionObject.element == NoteElement.Fire)
        {
            objectInCollider = false;
            Destroy(collisionObject.gameObject);
            MidiManagers.score.UpdateScore();
        }
        else if (Input.GetKey(KeyCode.K) && collisionObject.element == NoteElement.Earth)
        {
            objectInCollider = false;
            Destroy(collisionObject.gameObject);
            MidiManagers.score.UpdateScore();
        }
        else if (Input.GetKey(KeyCode.L) && collisionObject.element == NoteElement.Water)
        {
            objectInCollider = false;
            Destroy(collisionObject.gameObject);
            MidiManagers.score.UpdateScore();
        }
    }
}
