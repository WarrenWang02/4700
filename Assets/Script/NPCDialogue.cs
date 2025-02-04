using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NPCDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogueUI; // 包含所有对话UI的父对象
    public TextMeshProUGUI dialogueText; // UI文本
    public Button nextButton; // 继续对话的按钮
    public GameObject interactIndicator; // "E" 按键指示

    [Header("Dialogue Settings")]
    public string[] dialogueLines; // 存储对话内容
    private int currentLine = 0; // 当前对话索引

    private bool isPlayerNear = false;
    private bool isDialogueActive = false;

    private void Start()
    {
        dialogueUI.SetActive(false); // 确保对话 UI 关闭
        interactIndicator.SetActive(false); // 确保 E 按键指示关闭

        nextButton.onClick.AddListener(AdvanceDialogue); // 绑定按钮事件
    }

    private void Update()
    {
        if (isPlayerNear && !isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        interactIndicator.SetActive(false); // 关闭E指示
        dialogueUI.SetActive(true); // 显示UI
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine]; // 显示第一句对话
    }

    public void AdvanceDialogue()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine]; // 更新对话内容
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false); // 关闭UI
        interactIndicator.SetActive(true); // 重新显示E指示
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactIndicator.SetActive(true); // 显示E指示
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactIndicator.SetActive(false); // 关闭E指示
        }
    }
}
