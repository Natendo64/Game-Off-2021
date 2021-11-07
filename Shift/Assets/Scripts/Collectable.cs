using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    Vector2 originalPosition;

    public enum Item
    {
        flashlight,
        battery
    }

    public Item item;

    Sequence hoverSequence;

    private void Start()
    {
        originalPosition = transform.position;
        hoverSequence = DOTween.Sequence().Append(transform.DOMoveY(originalPosition.y + 0.25f, 1f));
        hoverSequence.SetLoops(-1, LoopType.Yoyo);
        hoverSequence.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Player>().CollectItem(item);
            DestroyCollectable();
        }
    }

    public void DestroyCollectable()
    {
        hoverSequence.Pause();
        Destroy(gameObject);
    }
}
