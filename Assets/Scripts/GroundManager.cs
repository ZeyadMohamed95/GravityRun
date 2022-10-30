using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject groundPiece;

    [SerializeField]
    private GameObject groundHolder;

    private float groundPieceLength;

    private int numberOfGroundPieces = 1;

    private Vector3 lastSpawnedGroundPiecePosition;

    public int NumberOfGroundPieces => this.numberOfGroundPieces;

    public GameObject GroundPiece => this.groundPiece;

    private void Start()
    {
        this.lastSpawnedGroundPiecePosition = groundPiece.transform.position;
        this.groundPieceLength = this.groundPiece.transform.localScale.z * 10;
    }

    /// <summary>
    /// Sets the next ground piece when the player is at the right distance
    /// </summary>
    public GameObject SetNextGroundPiece()
    {
        var placeToSpawn = new Vector3(lastSpawnedGroundPiecePosition.x, lastSpawnedGroundPiecePosition.y,
            lastSpawnedGroundPiecePosition.z + this.groundPieceLength  * numberOfGroundPieces);

       var newGround = Instantiate(groundPiece, placeToSpawn, Quaternion.identity, this.groundHolder.transform);

        this.numberOfGroundPieces++;

        return newGround;
    }
}
