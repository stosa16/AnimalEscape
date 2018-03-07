using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public GameObject dogPlayer;
    public Dialogue dialogue;
    public GameObject dialogueManager;
    public bool doSendPlayerNotifications;

    public void TriggerDialogue()
    {
        Debug.Log("DIalogue triggered");
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogue, dogPlayer, doSendPlayerNotifications);
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dogPlayer);
    }
}
