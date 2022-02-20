using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }
}
