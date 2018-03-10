using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnStart : MonoBehaviour {

    public GameObject dialogueTriggerObject;

    void Start () {
        var dialogueTrigger = dialogueTriggerObject.GetComponent<DialogueTrigger>();      
        dialogueTrigger.TriggerDialogue();
    }
	
	void Update () {
		
	}
}
