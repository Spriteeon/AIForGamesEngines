using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        LevelEnd,
    }

    private GameState m_CurrentState = GameState.Paused;

    [SerializeField]
    private GameObject player;

    [Header("UI")]
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text attemptsText;

    private int playerScore = 0;
    private int playerAttempts = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!player) // player null
        {
            throw new System.Exception("Game Controller: Player null!");
        }

        InitialiseUI();

        if (m_CurrentState != GameState.Playing)
        {
            PauseGame();
        }
    }  
    
    void InitialiseUI()
    {
        if ((!scoreText) || (!attemptsText))
        {
            throw new System.Exception("Game Controller: Text null!");            
        }

        scoreText.text = playerScore.ToString();
        attemptsText.text = playerAttempts.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame() 
    {
        Debug.Log(m_CurrentState.ToString());
        switch(m_CurrentState)
        {
            case GameState.Playing:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                player.GetComponent<FirstPersonController>().enabled = false;
                pauseUI.SetActive(true);
                m_CurrentState = GameState.Paused;
                break;

            case GameState.Paused:
                Time.timeScale = 1;
                player.GetComponent<FirstPersonController>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseUI.SetActive(false);
                m_CurrentState = GameState.Playing;
                break;

            case GameState.LevelEnd:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                player.GetComponent<FirstPersonController>().enabled = false;
                m_CurrentState = GameState.Paused; // Resets state for next level
                break;

            default:
                break;
        }

        Debug.Log(m_CurrentState.ToString());
    }

    public void RespawnPlayer()
    {
        Debug.Log("Player respawning");
        playerAttempts++;
        UpdateAttemptsUI();

        GameObject checkpoint = player.GetComponent<PlayerController>().GetCurrentCheckpoint();

        if (checkpoint)
        {
            Debug.Log(checkpoint.transform.position);
            player.GetComponent<FirstPersonController>().m_CharacterController.enabled = false;
            player.GetComponent<FirstPersonController>().m_CharacterController.transform.position = checkpoint.transform.position + new Vector3(-10,25,0); // Spawns player above spawn point so they fall to platform
            player.GetComponent<FirstPersonController>().m_CharacterController.enabled = true;
        }
        else
        {
            throw new System.Exception("Game Controller: Checkpoint null");
        }

        AddScore(-100);
    }

    public void AddScore(int value)
    {
        playerScore += value;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = playerScore.ToString();
    }

    private void UpdateAttemptsUI()
    {
        attemptsText.text = playerAttempts.ToString();
    }

    public void SetState(GameState newState)
    {
        m_CurrentState = newState;
    }
}
