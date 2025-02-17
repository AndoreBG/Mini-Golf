using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject Sombra;

    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject goalFX;

    [Header("Atributos")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;

    private bool isDragging;
    private bool inHole;

    [Header("Audios")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip goalSound;
    [SerializeField] private AudioClip winGameSound;

    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayerInput();

        if (LevelManager.main.outOfStrokes && rb.velocity.magnitude <= 0.2f && !LevelManager.main.levelCompleted)
        {
            LevelManager.main.GameOver();
        }
    }

    private bool isReady()
    {
        return rb.velocity.magnitude <= 0.2f;
    }

    private void PlayerInput()
    {
        if (!isReady()) return;

        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) DragStart();
        if (Input.GetMouseButton(0) && isDragging) DragChange(inputPos);
        if (Input.GetMouseButtonUp(0) && isDragging) DragRelease(inputPos);
    }

    private void DragStart()
    {
        isDragging = true;
        lr.positionCount = 2;
    }

    private void DragChange(Vector2 pos)
    {
        Vector2 dir = (Vector2)transform.position - pos;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) /2, maxPower /2)); 
    }

    private void DragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lr.positionCount = 0;

        if (distance < 1f)
        {
            return;
        }

        LevelManager.main.IncreaseStroke();

        Vector2 dir = (Vector2)transform.position - pos;

        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);

        PlayHitSound();
    }

    private void CheckWinState() 
    {
        if (inHole) return;

        if(rb.velocity.magnitude <= maxGoalSpeed) 
        {
            PlayGoalSound();
            inHole = true;

            rb.velocity = Vector2.zero;

            this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            Sombra.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

            GameObject fx = Instantiate(goalFX, transform.position, Quaternion.identity);
            Destroy(fx, 1.5f);

            LevelManager.main.LevelComplete();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal") CheckWinState();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Goal") CheckWinState();
    }

    private void PlayHitSound()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
    }

    private void PlayGoalSound()
    {
        audioSource.clip = goalSound;
        audioSource.Play();
    }

    private void PlayWinGameSound()
    {
        audioSource.clip = winGameSound;
        audioSource.Play();
    }
}
