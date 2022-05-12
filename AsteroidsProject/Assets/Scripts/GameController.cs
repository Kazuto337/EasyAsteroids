using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GAME STATES")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject onGame;

    [Header("ASTEROIDS POOL")]
    [SerializeField] List<AsteroidBehavior> asteroidsPool;
    public void GameOver()
    {
        onGame.SetActive(false);
        gameOverScreen.SetActive(true);

        foreach (AsteroidBehavior item in asteroidsPool)
        {
            item.isMoving = false;
            item.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        onGame.SetActive(true);
        gameOverScreen.SetActive(false);

        foreach (AsteroidBehavior item in asteroidsPool)
        {
            item.isMoving = true;
            item.gameObject.SetActive(false);
        }
    }
}
