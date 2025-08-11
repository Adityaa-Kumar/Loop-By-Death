using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;

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

    [Header("Die button")]
    public GameObject player;
    public Transform respawnPt;

    [Header("PauseScreen")]
    public GameObject pausePanel;
    public bool paused = false;

    void Start()
    {
        deadPanel.SetActive(false);

        resume();
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

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            DieButton();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                pause();
            }
            else if (paused == true)
            {
                resume();
            }
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

    public void DieButton()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isDead = true;
        StartCoroutine(Respawn());
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level2");
    }
    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void GoToLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void GoToLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void GoToLevel5()
    {
        SceneManager.LoadScene("Level5");
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(player, respawnPt.position, Quaternion.identity); 
    }

    public void pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        print("Pause");
    }

    public void resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        paused = false;
        print("Resume");
    }
}
