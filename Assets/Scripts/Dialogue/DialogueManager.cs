using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public GameObject _dogPlayer;
    public bool doSendNotificationsToPlayer;

    public Animator animator;
            
    private Queue<string> sentences;


	// Use this for initialization
	void Start () {
	}

    public void StartDialogue(Dialogue dialogue, GameObject dogPlayer, bool doSendPlayerNotifications)
    {
        animator.SetBool("isOpen", true);

        _dogPlayer = dogPlayer;
        doSendNotificationsToPlayer = doSendPlayerNotifications;
        if (doSendNotificationsToPlayer)
        {
            _dogPlayer.SendMessage("DisableInput");
        }

        Debug.Log("Start conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences = new Queue<string>();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);

        if (doSendNotificationsToPlayer)
        {
            _dogPlayer.SendMessage("EnableInput");
        }
        
        Debug.Log("End of conversation.");
    }
}
