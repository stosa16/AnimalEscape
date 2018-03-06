using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public GameObject dogPlayer;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        Debug.Log("DIalogue triggered");
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dogPlayer);
    }
}
