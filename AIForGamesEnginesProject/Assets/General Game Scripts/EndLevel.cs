using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject endLevelUI;
    [SerializeField]
    private GameObject nextLevelButton;

    [SerializeField]
    private bool lastLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameController)
        {
            throw new System.Exception("End Level: Game controller is null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pauses game and stop player movement
            gameController.SetState(GameController.GameState.LevelEnd);
            gameController.PauseGame();

            // Displays end UI and allows mouse cursor use
            endLevelUI.SetActive(true);
            
            if (lastLevel)
            {
                nextLevelButton.SetActive(false);
            }
        }
    }
}
