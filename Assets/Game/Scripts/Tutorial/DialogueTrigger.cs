using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] Dialogue dialogue2;

    private void Start() {
        Invoke(nameof(TriggerDialogue), 20);
    }

    public void TriggerDialogue() {
        DialogueManager.instance.StartDialogue(dialogue);
        Invoke(nameof(TriggerDialogue2), 10);
    }

    public void TriggerDialogue2() {
        DialogueManager.instance.StartDialogue(dialogue2);
    }
}
