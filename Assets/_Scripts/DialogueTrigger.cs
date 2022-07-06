using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(NPCBounds npcbounds)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, npcbounds);
    }

    public void TriggerObjectDialogue(Object obj)
    {
        FindObjectOfType<DialogueManager>().StartObjectDialogue(dialogue, obj);
    }

    public void TriggerDialogueLostGhost(LostGhost lg)
    {
        FindObjectOfType<DialogueManager>().StartDialogueLostGhots(dialogue, lg);
    }

    public void TriggerDialogueExit()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
    }
}
