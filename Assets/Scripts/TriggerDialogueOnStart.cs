using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnStart : MonoBehaviour {

    public Dialogue dialogue;
    public GameObject dogPlayer;

    void Start () {
        //var dogPlayer = GameObject.FindGameObjectsWithTag("DialogObstacle")[0];
        var dialogueTrigger = dogPlayer.GetComponent<DialogueTrigger>();
        dialogueTrigger.dogPlayer = dogPlayer;
        dialogueTrigger.dialogue = dialogue;        
        dialogueTrigger.TriggerDialogue();
    }
	
	void Update () {
		
	}
}
