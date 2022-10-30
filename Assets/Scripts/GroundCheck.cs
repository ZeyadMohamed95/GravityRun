using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGrounded = false;

    public bool IsGrounded => this.isGrounded;

    public Action PlayerOnGround;

    public Action PlayerAirborne;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Ground")
        {
            return;
        }
        this.isGrounded = true;

        this.PlayerOnGround?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag != "Ground")
        {
            return;
        }
        this.isGrounded = false;

        this.PlayerAirborne?.Invoke();
    }
}
