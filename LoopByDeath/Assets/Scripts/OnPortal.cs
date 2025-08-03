using UnityEngine;

public class OnPortal : MonoBehaviour
{
    [Header("Game Win")]
    public GameObject deadPanel;
    public AudioSource source;
    public AudioClip gameOverClip;
    public AudioSource bgMusic;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            deadPanel.SetActive(true);
            source.PlayOneShot(gameOverClip, 0.05f);
            bgMusic.Pause();
        }
    }
}
