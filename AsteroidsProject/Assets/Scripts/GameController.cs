using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GAME STATES")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject onGame;
    [SerializeField] PlayerController player;

    [Header("ASTEROIDS POOL")]
    [SerializeField] List<AsteroidBehavior> asteroidsPool;

    [Header("ENEMIES POOL")]
    [SerializeField] List<EnemyBehavior> enemiesPool;

    [Header("BACKGROUND SPEED")]
    [SerializeField] GameObject background;

    private void Awake()
    {
        background.GetComponent<Renderer>().material.SetVector("Speed_", new Vector4(0, 0.33f, 0, 0));
    }

    public void GameOver()
    {
        print("GameOver");
        onGame.SetActive(false);
        gameOverScreen.SetActive(true);

        foreach (AsteroidBehavior item in asteroidsPool)
        {
            item.isMoving = false;
            item.gameObject.SetActive(true);
        }

        foreach (GameObject item in Bulletspool.Instance.BulletList)
        {
            Bulletspool.Instance.Return2Pool(item);
        }
        foreach (EnemyBehavior item in enemiesPool)
        {
            item.ResetEnemy();
        }
    }

    public void StartGame()
    {
        player.ResetPlayer();
        onGame.SetActive(true);
        gameOverScreen.SetActive(false);

        foreach (AsteroidBehavior item in asteroidsPool)
        {
            item.gameObject.SetActive(true);
            item.ResetPosition();
            item.isMoving = true;
        }        
    }
}
