using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalWatch : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [SerializeField]
    Text currentTime;
    [SerializeField]
    Text currentDate;

    [SerializeField]
    Player player;

    [SerializeField]
    AudioSource[] ticking;
    int tickCount = 0;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        // Show or hide watch
        if (Input.GetKeyDown("tab"))
        {
            if (canvasGroup.alpha == 1)
                canvasGroup.alpha = 0;
            else
                canvasGroup.alpha = 1;
        }

        // Update time on watch
        if (canvasGroup.alpha == 1)
        {
            currentTime.text = System.DateTime.Now.ToString("hh:mm:ss");
            currentDate.text = System.DateTime.Now.Date.ToShortDateString();
        }

        // Play clock ticking
        if (canvasGroup.alpha == 1 && !ticking[0].isPlaying && !ticking[1].isPlaying)
        {
            ticking[tickCount].Play();
            tickCount++;
            if (tickCount > 1)
                tickCount = 0;
        }
    }


}
