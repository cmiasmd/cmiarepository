using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteract : Interactable
{

    private DialogueTrigger dt;
    private Transform tr;
    private Rigidbody2D rb;
    public bool dialogueState = false;
    private LostGhost lg;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        dt = GetComponent<DialogueTrigger>();
        lg = GetComponent<LostGhost>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            rb.MovePosition(tr.position);
            rb.isKinematic = true;
            if (Input.GetKeyDown("space") && !dialogueState)
            {
                dialogueState = true;
                dt.TriggerDialogueLostGhost(lg);
            }
        }
        else
        {
            rb.isKinematic = false;
            if (dialogueState)
            {
                dt.TriggerDialogueExit();
                dialogueState = false;
            }
        }
    }
}
