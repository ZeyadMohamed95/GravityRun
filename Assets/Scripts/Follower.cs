using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFollow;

    [SerializeField]
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        this.Follow();
    }

    private void Follow()
    {
        transform.position = objectToFollow.transform.position + offset;
    }
}
