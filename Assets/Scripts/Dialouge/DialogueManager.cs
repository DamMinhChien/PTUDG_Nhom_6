using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using static IntroStory;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;

    private Queue<DialogueLine> dialogueLines; // ✅ sửa thành DialogueLine
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private System.Action onDialogueComplete;

    [SerializeField] private float typingSpeed = 0.03f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        dialogueUI.SetActive(false);
        dialogueLines = new Queue<DialogueLine>();
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentFullLine;
                isTyping = false;
            }
            else
            {
                ShowNextLine();
            }
        }
    }

    private string currentFullLine = "";
    public void ShowDialogue(List<DialogueLine> lines, System.Action onComplete = null)
    {
        if (lines == null || lines.Count == 0) return;

        dialogueLines.Clear();
        foreach (var line in lines)
            dialogueLines.Enqueue(line); // ✅ giữ nguyên object

        dialogueUI.SetActive(true);
        isDialogueActive = true;
        onDialogueComplete = onComplete;

        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueLines.Dequeue();
        currentFullLine = line.text;
        dialogueImage.sprite = line.image;
        dialogueImage.gameObject.SetActive(line.image != null);

        typingCoroutine = StartCoroutine(TypeLine(currentFullLine));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);

        onDialogueComplete?.Invoke();
        onDialogueComplete = null;

        Debug.Log("Kết thúc thoại → gọi callback");
    }

    public bool IsDialogueActive => isDialogueActive;
}
