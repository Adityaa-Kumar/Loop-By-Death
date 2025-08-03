using UnityEngine;

public class OnPortal : MonoBehaviour
{
    [Header("Game Win")]
    public GameObject deadPanel;
    public AudioSource source;
    public AudioClip gameOverClip;
    public AudioSource bgMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        deadPanel.SetActive(true);
        source.PlayOneShot(gameOverClip, 0.05f);
        bgMusic.Pause();
    }
}
