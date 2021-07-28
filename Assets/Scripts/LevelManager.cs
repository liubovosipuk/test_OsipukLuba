using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }

    public Transform spawnPosistion;
    public Transform playerTransform;

    private float deathBound = -12f;
    [SerializeField] private GameObject panelWin,
                                        loseGamePanel;

    private void Awake()
    {
        Instance = this; 
    }

    private void Update()
    {
        if (playerTransform.position.y <= deathBound)
        {
            LoseGame();
        }
    }

    public void Win()
    {
        panelWin.SetActive(true);
        
    }

    private void LoseGame()
    {
        loseGamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
} // LevelManager
