using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text distanceText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text initialScore;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private GameOverPanelController GameOverPanel;

    [SerializeField]
    private Image damageScreem;

    private int score = 0;

    private Vector3 initialScorePosition;

    private void Awake()
    {
        this.initialScorePosition = this.initialScore.transform.position;
    }

    public void UpdateDistance(string distance)
    {
        this.distanceText.text = distance;
    }

    public void UpdateScore(int score)
    {
        this.initialScore.gameObject.SetActive(true);
        this.initialScore.text = (score - this.score).ToString();
        this.score = score;
        this.initialScore.transform.DOMove(scoreText.transform.position, 1f).OnComplete(this.ApplyScore);
    }

    private void ApplyScore()
    {
        scoreText.DOCounter(0, this.score, 0.5f);
        this.initialScore.gameObject.SetActive(false);
        this.initialScore.transform.position = this.initialScorePosition;
    }

    public void UpdateHealthBar(float newHealth)
    {
        var fillValue = newHealth / 100;

        DOTween.To(() => healthBar.fillAmount, x => healthBar.fillAmount = x, fillValue, 1);
    }

    public void ActivateGameOverPanel()
    {
        this.GameOverPanel.gameObject.SetActive(true);
        this.GameOverPanel.UpdateScore(this.score.ToString());
    }

    public void OnPlayerHit(EnemyController enemyController)
    {
        DOTween.To(() => this.damageScreem.color, x => this.damageScreem.color = x, new Color(1, 1, 1, 0.3f), 0.2f).SetOptions(true).OnComplete(DisappearDamageScreen);
    }

    private void DisappearDamageScreen()
    {
        DOTween.To(() => this.damageScreem.color, x => this.damageScreem.color = x, new Color(1, 1, 1, 0), 0.2f).SetOptions(true);
    }
}
