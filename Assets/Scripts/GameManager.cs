using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameStarted = false;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private UiManager uiManager;

    [SerializeField]
    private GroundManager groundManager;

    [SerializeField]
    private EnemyManager enemyManager;

    [SerializeField]
    private int fallDamage = 25;

    private Vector3 initialPlayerPosition;

    private int distanceTravelled;

    private int Score = 0;

    private bool isPlayerInvincible = false;

    private bool isScoreUpdated = false;


    private void Start()
    {
        GameStarted = false;
        this.initialPlayerPosition = playerController.transform.position;
        this.enemyManager.PopulateGroundWithEnemies(groundManager.GroundPiece, groundManager.NumberOfGroundPieces, this.initialPlayerPosition);
        this.enemyManager.PopulateGroundWithEnemies(groundManager.GroundPiece, groundManager.NumberOfGroundPieces, this.initialPlayerPosition, true);

        this.playerController.PlayerHit += this.OnPlayerCollided;
        this.playerController.PlayerHit += this.uiManager.OnPlayerHit;
        this.playerController.PlayerDied += this.uiManager.ActivateGameOverPanel;
    }

    private void FixedUpdate()
    {
        this.CheckDistanceToSpawnNextGround();
    }

    private void Update()
    {
        this.CalculateDistanceTravelled();
        this.CheckFallDistance();
    }

    public void StartGame()
    {
        GameStarted = true;
    }    

    private void UpdateScore()
    {
        this.Score += 1000;
        this.uiManager.UpdateScore(this.Score);
    }

    private void CheckFallDistance()
    {
        if(this.isPlayerInvincible)
        {
            return;
        }

        if(Mathf.Abs(this.playerController.transform.position.y - initialPlayerPosition.y) >= 20)
        {
            this.dealDamageToPlayer(this.fallDamage);
        }
    }

    private void CalculateDistanceTravelled()
    {
        this.distanceTravelled = (int)Vector3.Distance(this.initialPlayerPosition, this.playerController.transform.position);
        this.uiManager.UpdateDistance(distanceTravelled.ToString());

        if(isScoreUpdated)
        {
            return;
        }

        if(this.distanceTravelled % 100 == 0 && this.distanceTravelled != 0)
        {
            this.UpdateScore();
            isScoreUpdated = true;
            StartCoroutine(this.ScoreCooldown());
        }
    }

    private IEnumerator ScoreCooldown()
    {
        yield return new WaitForSeconds(1f);
        this.isScoreUpdated = false;
    }

    private void CheckDistanceToSpawnNextGround()
    {
        if(this.distanceTravelled == 150 * groundManager.NumberOfGroundPieces)
        {
            var ground = this.groundManager.SetNextGroundPiece();
            this.enemyManager.PopulateGroundWithEnemies(ground, groundManager.NumberOfGroundPieces, this.initialPlayerPosition);
            this.enemyManager.PopulateGroundWithEnemies(ground, groundManager.NumberOfGroundPieces, this.initialPlayerPosition, true);
        }
    }

    private void OnPlayerCollided(EnemyController enemy)
    {
        if(this.isPlayerInvincible)
        {
            Destroy(enemy.gameObject);
            return;
        }

        this.dealDamageToPlayer(enemy.DamageToDeal);

        Destroy(enemy.gameObject);
    }

    private void dealDamageToPlayer(int damage)
    {
        this.playerController.TakeDamage(damage);
        this.uiManager.UpdateHealthBar(this.playerController.Health);
        this.isPlayerInvincible = true;

        StartCoroutine(this.ResetInvincibility());
    }

    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(1.5f);
        this.isPlayerInvincible = false;
    }
}
