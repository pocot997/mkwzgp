using DVSN.GameManagment;
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
        middleTrackPlayerPosition = transform.localPosition;
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
            transform.localPosition = middleTrackPlayerPosition;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition = new Vector3(MidiManagers.globalPathCoordinates.leftTrackShiftX, middleTrackPlayerPosition.y, middleTrackPlayerPosition.z);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localPosition = new Vector3(MidiManagers.globalPathCoordinates.rightTrackShiftX, middleTrackPlayerPosition.y, middleTrackPlayerPosition.z);
        }
        else
        {
            transform.localPosition = middleTrackPlayerPosition;
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
            Managers.BattleLoader.enemyHP -= 20;
            objectInCollider = false;
            Destroy(collisionObject.gameObject);
            MidiManagers.score.UpdateScore();
            Managers.BattleLoader.currentEnemy.ChangeHitPoints(-20);
        }
        else if (Input.GetKey(KeyCode.K) && collisionObject.element == NoteElement.Earth)
        {
            Managers.BattleLoader.enemyHP -= 20;
            objectInCollider = false;
            Destroy(collisionObject.gameObject);
            MidiManagers.score.UpdateScore();
            Managers.BattleLoader.currentEnemy.ChangeHitPoints(-20);
        }
        else if (Input.GetKey(KeyCode.L) && collisionObject.element == NoteElement.Water)
        {
            Managers.BattleLoader.enemyHP -= 20;
            objectInCollider = false;
            Destroy(collisionObject.gameObject);
            MidiManagers.score.UpdateScore();
            Managers.BattleLoader.currentEnemy.ChangeHitPoints(-20);
        }
    }
}
