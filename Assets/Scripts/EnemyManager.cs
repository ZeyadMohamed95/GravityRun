using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemy;

    [SerializeField]
    private float distanceBetweenSpawns;

    [SerializeField]
    private GameObject enemyHolder;

    public void PopulateGroundWithEnemies(GameObject ground, int groundNumber, Vector3 initialPlayerPosition, bool belowGround = false)
    {

        var groundLength = ground.transform.localScale.z * 10;

        float SpawningPointZ = ground.transform.position.z - groundLength / 2;

        if(groundNumber == 1)
        {
            SpawningPointZ = (ground.transform.position.z - groundLength / 2) + initialPlayerPosition.z + 50;
        }

        var minHorizontalSpawningPoint = ground.transform.position.x - ground.transform.localScale.x / 2;

        var maxHorizontalSpawningPoint = ground.transform.position.x + ground.transform.localScale.x / 2;

        

        while(SpawningPointZ < (ground.transform.position.z + groundLength / 2))
        {
            var XspawnPoint = Random.Range(minHorizontalSpawningPoint, maxHorizontalSpawningPoint) * 10;

            if (belowGround)
            {
                Instantiate(enemy, new Vector3(XspawnPoint, enemy.transform.position.y - 3.5f, SpawningPointZ), Quaternion.identity, this.enemyHolder.transform);
            }
            else
            {
                Instantiate(enemy, new Vector3(XspawnPoint, enemy.transform.position.y, SpawningPointZ), Quaternion.identity, this.enemyHolder.transform);
            }
            SpawningPointZ += distanceBetweenSpawns;
        }
    }
}
