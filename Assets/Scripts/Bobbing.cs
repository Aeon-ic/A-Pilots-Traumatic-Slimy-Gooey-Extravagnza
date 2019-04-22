using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    [Tooltip("This is the distance upwards from the center of the object that it can travel")]
    public float upwardsDistance;
    [Tooltip("This is the distance downwards from the center of the object that it can travel")]
    public float downwardsDistance;
    [Tooltip("This is the speed at which the object bobs")]
    public float movementSpeed;
    //true for up, false for down
    bool travelDirection = true;
    Vector3 startPosition;

    private void Awake()
    {
        startPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y < startPosition.y - downwardsDistance)
        {
            travelDirection = true;
        }
        
        if (this.gameObject.transform.position.y > startPosition.y + upwardsDistance)
        {
            travelDirection = false;
        }

        if (travelDirection == true)
        {
            this.gameObject.transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        }
        else
        {
            this.gameObject.transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        }
    }
}
