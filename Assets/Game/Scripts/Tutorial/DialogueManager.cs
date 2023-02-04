using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private Queue<string> sentences;

    [SerializeField] GameObject DialogueGameObject;

    [SerializeField] TMP_Text DialogueTitle;
    [SerializeField] TMP_Text DialogueText;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        Time.timeScale = 0;
        GridManager.isPaused = true;

        DialogueGameObject.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DialogueTitle.text = dialogue.name;
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        DialogueText.text = sentence;
    }

    void EndDialogue() {
        Time.timeScale = 1f;
        GridManager.isPaused = false;

        DialogueGameObject.SetActive(false);
    }
}
