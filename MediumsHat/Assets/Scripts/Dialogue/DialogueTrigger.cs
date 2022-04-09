using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool isInRange;
    public int numPnj;

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
        DialogueManager.instance.startDialogue(numPnj);
    }
}
