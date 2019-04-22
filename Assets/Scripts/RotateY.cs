using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    [Tooltip("This is the speed at which the object rotates")]
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
