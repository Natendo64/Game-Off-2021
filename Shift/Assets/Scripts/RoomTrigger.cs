using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField]
    RoomManager.Direction direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && (direction == RoomManager.Direction.left || direction == RoomManager.Direction.right))
            FindObjectOfType<RoomManager>().NextRoom(direction);
        if (collision.gameObject.tag == "Player" && direction == RoomManager.Direction.up && Input.GetKeyDown("w"))
            FindObjectOfType<RoomManager>().NextRoom(direction);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && direction == RoomManager.Direction.up && Input.GetKeyDown("w"))
            FindObjectOfType<RoomManager>().NextRoom(direction);
    }
}
