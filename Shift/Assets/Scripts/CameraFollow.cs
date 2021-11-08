using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void Update()
    {
        if (player.transform.position.x <= -5.75f || player.transform.position.x >= 5.75f)
        {
            transform.parent = null;
            if (player.transform.position.x <= -5.75f)
                transform.position = new Vector3(-5.75f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(5.75f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.parent = player.transform;
        }

    }
}
