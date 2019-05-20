using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Warp : MonoBehaviour
{
    public Transform warpPoint;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Player.instance.player.gameObject.transform.position = warpPoint.position;
        }
    }
}
