using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoopManager : MonoBehaviour
{
    [Header("Loops")]
    public double loops = 5;
    public TextMeshProUGUI loopText;

    [Header("Game Over")]
    public GameObject deadPanel;
    public AudioSource source;
    public AudioClip gameOverClip;
    public AudioSource bgMusic;

    void Start()
    {
        deadPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (loops == 0)
        {
            deadPanel.SetActive(true);
            source.PlayOneShot(gameOverClip, 0.05f);
            bgMusic.Pause();
        }
    }

    public void ReduceLoops()
    {
        loops--;
        loopText.text = loops.ToString();
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
