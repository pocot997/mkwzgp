using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
    Vector3 middleTrackPlayerPosition;
    Vector3 lerpInitialPosition;

    [SerializeField] float lerpSpeed = 0.1f;
    float lerpStartTime;

    [SerializeField] Transform playerCollider;

    Renderer renderer;

    void Start()
    {
        middleTrackPlayerPosition = transform.localPosition;
        middleTrackPlayerPosition.y = MidiManagers.globalPathCoordinates.trackShiftY;
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.D))
        {
            lerpStartTime = Time.time;
            lerpInitialPosition = transform.localPosition;
        }

        transform.localPosition = Vector3.Lerp(lerpInitialPosition, playerCollider.localPosition, (Time.time - lerpStartTime) * lerpSpeed /* * Mathf.Clamp(Vector3.Distance(transform.localPosition, playerCollider.localPosition),0.3f,5)*/);

        if (Input.GetKey(KeyCode.J))
        {
            renderer.material.color = Color.red;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            renderer.material.color = Color.green;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            renderer.material.color = Color.blue;
        }
        else
        {
            renderer.material.color = Color.black;
        }
    }
}
