using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : Interactable
{
    private Vector3 movDirection;
    private Transform myTransform;
    private DialogueTrigger dialogueTrigger;
    public float MOV_BASE_SPEED = 1.0f;
    public float movSpeed;
    //private Rigidbody2D myRigidbody;
    public float count = 5.0f;
    private float timeLeft;
    private bool dialogueState = false;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        //myRigidbody = GetComponent<Rigidbody2D>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        //ChangeDirection();
    }


    void Update()
    {
        
        if (playerInRange)
        {
           // myRigidbody.MovePosition(myTransform.position);
           // myRigidbody.isKinematic = true;
            if (Input.GetKeyDown("space") && !dialogueState)
            {                
                dialogueState = true;
                //dialogueTrigger.TriggerDialogue();
            }
        }
        else 
        {
            //myRigidbody.isKinematic = false;
            if (dialogueState)
            {
                dialogueTrigger.TriggerDialogueExit();
                dialogueState = false;
                //ProcessInputs();
                //Move();
            }
            //ProcessInputs();
            //Move();
        }

    }

    void ChangeDirection()
    {
        int direction = Random.Range(0, 5);
        switch(direction)
        {
            case 0:
                movDirection = Vector2.right;
                break;
            case 1:
                movDirection = Vector2.up;
                break;
            case 2:
                movDirection = Vector2.left;
                break;
            case 3:
                movDirection = Vector2.down;
                break;
            default:
                movDirection = Vector2.zero;
                break;
        };
    }
    void ProcessInputs()
    {
        movSpeed = Mathf.Clamp(movDirection.magnitude, 0.0f, 1.0f);
        movDirection.Normalize();
    }
    private void Move()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            ChangeDirection();
            timeLeft = count;
        }


        //myRigidbody.velocity = movDirection * movSpeed * MOV_BASE_SPEED;

    }
}
