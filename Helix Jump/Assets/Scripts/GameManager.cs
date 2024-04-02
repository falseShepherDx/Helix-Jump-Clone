using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private int score;
    public TextMeshProUGUI scoreText;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip ringPassAudio;
    
    private void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void UpdateScore(int ringScore)
    {
        score += ringScore;
        float audioPitch = (Ball.instance.passedRingCount+2)/3f;
        _audioSource.pitch = audioPitch;
        _audioSource.PlayOneShot(ringPassAudio);
        Ball.instance.passedRingCount++;
        scoreText.text = score.ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
