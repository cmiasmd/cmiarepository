using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input Settings:")]
    public int playerID;
    public GameObject bubble;

    [Space]
    [Header("Character Attributes:")]
    public float MOV_BASE_SPEED = 1.0f;

    [Space]
    [Header("Character statistics:")]
    public Vector2 movDirection;
    public float movSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Game Config:")]
    public GameConfig gc;
    public GameObject flashlight;

    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
        bubble.SetActive(gc.playerInRange);
    }

    void ProcessInputs()
    {

        movDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movSpeed = Mathf.Clamp(movDirection.magnitude, 0.0f, 1.0f);
        movDirection.Normalize();

        Vector2 fldirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - flashlight.transform.position;
        float angle = Mathf.Atan2(-1 * fldirection.x, fldirection.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        flashlight.transform.rotation = Quaternion.Slerp(flashlight.transform.rotation, rotation, 1);
    }

    void Move()
    {
        rb.velocity = movDirection * movSpeed * MOV_BASE_SPEED;
    }

    void Animate()
    {
        if (movDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movDirection.x);
            animator.SetFloat("Vertical", movDirection.y);
        }
        animator.SetFloat("Speed", movSpeed);
    }
}
