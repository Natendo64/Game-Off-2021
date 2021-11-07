using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    public bool obtained;

    public float battery = 100f;
    float elapsed = 0f;

    [SerializeField]
    Image batteryBar;
    [SerializeField]
    Text batteryTitle;

    public GameObject player;
    Light2D flashlight;
    SpriteRenderer flashlightImage;

    [SerializeField]
    AudioSource on;
    [SerializeField]
    AudioSource off;

    [SerializeField]
    CanvasGroup watch;

    [SerializeField]
    Animator animator;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    PolygonCollider2D polygonCollider2D;

    void Start()
    {
        flashlight = GetComponent<Light2D>();
        flashlightImage = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        if (watch.alpha == 0)
        {
            // convert mouse position into world coordinates
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // get direction you want to point at
            Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;

            // set vector of transform directly
            if (obtained && watch.alpha == 0)
                transform.up = direction;

            // convert mouse position into world coordinates
            Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

            // convert player position into world coordinates
            Vector2 playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);

            // move flashlight based on whether the mouse is to the left or right of the player
            if (mouse.x < playerScreenPoint.x)
            {
                spriteRenderer.flipX = true;
                transform.localPosition = new Vector2(0.03f, transform.localPosition.y);
            }
            else
            {
                spriteRenderer.flipX = false;
                transform.localPosition = new Vector2(-0.03f, transform.localPosition.y);
            }

            // check for flashlight toggle button
            if (Input.GetKeyDown("f") && battery > 0f && obtained)
                Toggle();

            // drain battery when flashlight is on
            if (flashlight.enabled)
                Drain();
        }
    }

    void Toggle()
    {
        flashlight.enabled = !flashlight.enabled;
        polygonCollider2D.enabled = !polygonCollider2D.enabled;

        flashlightImage.enabled = !flashlightImage.enabled;
        if (flashlight.enabled)
        {
            animator.SetBool("Flashlight", true);
            on.Play();
        }
        else
        {
            off.Play();
            animator.SetBool("Flashlight", false);
        }
    }

    void Drain()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = 0f;
            if (battery > 0f)
            {
                battery -= 2f;
            }
            else
            {
                Toggle();
            }
            UpdateFlashlight();
        }
    }

    void UpdateFlashlight()
    {
        batteryBar.fillAmount = battery / 100f;
    }

    public void ObtainedFlashlight()
    {
        obtained = true;
        batteryBar.gameObject.SetActive(true);
        batteryTitle.gameObject.SetActive(true);
    }

    public void NewBattery()
    {
        battery = 100f;
        UpdateFlashlight();
    }
}
