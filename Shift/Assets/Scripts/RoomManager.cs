using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

public class RoomManager : MonoBehaviour
{
    public enum Direction
    {
        left,
        right,
        up
    }

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject room;

    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    Light2D[] lights;

    [SerializeField]
    AudioSource lightsOn;
    [SerializeField]
    AudioSource lightsOff;

    [SerializeField]
    Vector2[] spawnPoints;
    [SerializeField]
    GameObject[] collectables;
    [SerializeField]
    GameObject[] enemies;

    GameObject spawnedItem;
    GameObject spawnedEnemy;

    Direction direction;

    [SerializeField]
    Flashlight flashlight;

    public void NextRoom(Direction direction)
    {
        this.direction = direction;
        ScreenFade();
        StartCoroutine(LoadRoom());
    }

    IEnumerator LoadRoom()
    {
        yield return new WaitForSeconds(0.5f);
        switch (direction)
        {
            case Direction.left:
                player.transform.position = new Vector2(room.transform.position.x + 7.75f, player.transform.position.y);
                break;
            case Direction.right:
                player.transform.position = new Vector2(room.transform.position.x - 7.75f, player.transform.position.y);
                break;
            case Direction.up:
                player.transform.position = new Vector2(room.transform.position.x, player.transform.position.y);
                break;
        }
        if (spawnedItem != null)
            spawnedItem.GetComponent<Collectable>().DestroyCollectable();
        if (spawnedEnemy != null)
            Destroy(spawnedEnemy);
        CheckLights();
        SpawnItem();
        SpawnEnemy();
    }

    void ScreenFade()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1, 0.5f));
        sequence.Append(canvasGroup.DOFade(0, 0.5f));
        sequence.Play();
    }

    void CheckLights()
    {
        int random = Random.Range(0, 5);

        if (random == 0)
        {
            if (lightsOn.isPlaying)
            {
                lightsOn.Stop();
                lightsOff.Play();
            }
            foreach (Light2D light in lights)
                light.enabled = false;
        }
        else
        {
            if (!lightsOn.isPlaying)
                lightsOn.Play();
            foreach (Light2D light in lights)
                light.enabled = true;
        }
    }

    void SpawnItem()
    {
        int spawnChance = Random.Range(0, 5);

        if (spawnChance == 0)
        {
            GameObject itemToSpawn = collectables[Random.Range(0, collectables.Length)];
            Vector2 spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)];

            switch (itemToSpawn.GetComponent<Collectable>().item)
            {
                case Collectable.Item.flashlight:
                    if (!flashlight.obtained)
                        spawnedItem = Instantiate(itemToSpawn, spawnLocation, Quaternion.identity);
                    break;
                case Collectable.Item.battery:
                    if (flashlight.obtained)
                        spawnedItem = Instantiate(itemToSpawn, spawnLocation, Quaternion.identity);
                    break;
            }
        }
    }

    void SpawnEnemy()
    {
        int spawnChance = Random.Range(0, 5);

        if (spawnChance == 0)
        {
            GameObject enemyToSpawn = enemies[Random.Range(0, enemies.Length)];
            Vector2 spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)];

            spawnedEnemy = Instantiate(enemyToSpawn, spawnLocation, Quaternion.identity);
        }
    }
}
