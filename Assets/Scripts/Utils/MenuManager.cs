using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void NewGame(){
        SceneManager.LoadScene("Start");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
