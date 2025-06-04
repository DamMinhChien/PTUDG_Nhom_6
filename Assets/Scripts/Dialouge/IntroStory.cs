using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStory : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string text;
        public Sprite image;
    }

    [SerializeField]
    private List<DialogueLine> storyLines; // ✅ Đổi từ string → DialogueLine

    private IEnumerator Start()
    {
        yield return null;

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.ShowDialogue(storyLines, OnStoryComplete);
        }
        else
        {
            Debug.LogError("DialogueManager chưa được khởi tạo!");
        }
    }

    private void OnStoryComplete()
    {
        Debug.Log("Hoàn tất cốt truyện mở đầu, bắt đầu game.");
        SceneManager.LoadScene("Game");
    }
}
