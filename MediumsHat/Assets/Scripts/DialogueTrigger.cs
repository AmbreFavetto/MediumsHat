using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isInRange;

    void Update()
    {
        if(isInRange && Input.GetButtonDown("Interaction"))
        {
            triggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            isInRange = false;
        }
    }

    public void triggerDialogue()
    {
        DialogueManager.instance.startDialogue(1);
    }
}
