using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartGamePanelController : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    private void OnEnable()
    {
        startButton.transform.localScale = Vector3.zero;

        startButton.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
    }
}
