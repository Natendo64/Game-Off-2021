using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hound : MonoBehaviour
{
    Transform target;
    [SerializeField]
    float speed;

    [SerializeField]
    AudioSource[] growls;

    [SerializeField]
    AudioSource cry;

    float elapsed = 0f;

    Animator animator;

    bool triggered;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        transform.LookAt(targetPosition);

        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

        //move towards the player if distance from target is greater than 1
        if (Vector3.Distance(transform.position, target.position) > 1f && !triggered)
        {
            animator.SetBool("Moving", true);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else
            animator.SetBool("Moving", false);

        elapsed += Time.deltaTime;
        if (elapsed >= 3f)
        {
            elapsed = 0f;
            growls[Random.Range(0, growls.Length)].Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flashlight")
        {
            cry.Play();
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flashlight")
        {
            triggered = false;
        }
    }
}
