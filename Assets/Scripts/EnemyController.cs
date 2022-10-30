using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyCollider enemyCollider;

    [SerializeField]
    private int damageToDeal = 25;

    public EnemyCollider EnemyCollider => this.enemyCollider;

    public int DamageToDeal => this.damageToDeal;

    private void Awake()
    {
        this.enemyCollider = GetComponentInChildren<EnemyCollider>();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        this.enemyCollider.EnemyHit -= Destroy;
    }
}
