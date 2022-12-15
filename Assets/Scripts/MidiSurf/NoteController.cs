using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    [SerializeField] float speed;
    public NoteElement element;

    void Update()
    {
        // Moves the object forward
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.localPosition.z <= -1)
            Destroy(this.gameObject);
    }
}

public enum NoteElement
{
    Fire,
    Earth,
    Water
}
