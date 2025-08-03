using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
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

    [Header("Die button")]
    public GameObject player;
    public Transform respawnPt;

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

        if (Input.GetButtonDown("E"))
        {
            DieButton();
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
    
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(player, respawnPt.position, Quaternion.identity); 
    }


}
