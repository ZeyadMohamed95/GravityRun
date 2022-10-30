using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameOverPanelController : MonoBehaviour
{
    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private TMP_Text scoreText;

    private void OnEnable()
    {
        restartButton.transform.localScale = Vector3.zero;

        restartButton.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
    }

    public void UpdateScore(string score)
    {
        this.scoreText.text = score;
    }

}
