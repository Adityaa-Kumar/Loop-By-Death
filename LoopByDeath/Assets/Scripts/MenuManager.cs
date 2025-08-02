using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
