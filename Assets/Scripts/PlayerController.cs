using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Action<EnemyController> PlayerHit;

    public Action PlayerDied;

    private const string GotHitString = "GotHit";
    private const string PlayerDiedString = "PlayerDied";

    [SerializeField]
    private float movementSpeed = 5.0f;

    [SerializeField]
    private float turningSpeed = 8.0f;

    [SerializeField]
    private float jumpForce = 10.0f;

    private int hp = 100;

    private bool isDead = false;

    private bool isReversingGravity;

    private GroundCheck groundCheck;

    private Rigidbody rb;

    private Animator animator;

    public int Health => this.hp;

    private void Awake()
    {
        this.groundCheck = GetComponentInChildren<GroundCheck>();
        this.rb = GetComponent<Rigidbody>();
        this.animator = GetComponentInChildren<Animator>();
        this.groundCheck.PlayerAirborne += this.playFallingAnimation;
        this.groundCheck.PlayerOnGround += this.playLandingAnimation;
    }

    private void playLandingAnimation()
    {
        this.animator.SetBool("IsGrounded", true);
    }

    private void playFallingAnimation()
    {
        this.animator.SetBool("IsGrounded", false);
    }

    private void Start()
    {
        Physics.gravity += -1 *Vector3.up * 7f;
    }

    private void Update()
    {
        if (isDead || !GameManager.GameStarted)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            this.ReverseGravity(1);

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            this.ReverseGravity(-1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead || !GameManager.GameStarted)
        {
            return;
        }

        this.MoveForward();
        this.MoveSideways();
    }

    private void ReverseGravity(int sideFator = 1)
    {
        if (isReversingGravity)
        {
            return;
        }

        transform.DORotate(new Vector3(0, 0, sideFator * 180), 0.5f, RotateMode.WorldAxisAdd);
        Physics.gravity *= -1;
        this.isReversingGravity = true;
        StartCoroutine(ResetReverseGravity());
        
    }

    private IEnumerator ResetReverseGravity()
    {
        yield return new WaitForSeconds(0.7f);
        this.isReversingGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        var enemyController = other.GetComponentInParent<EnemyController>();
        if(enemyController == null)
        {
            return;
        }

        this.PlayerHit?.Invoke(enemyController);
    }

    private void MoveForward()
    {
        transform.Translate(this.transform.forward * this.movementSpeed * Time.deltaTime, Space.World);
    }

    private void MoveSideways()
    {
        transform.Translate(Input.GetAxis("Horizontal") * this.transform.right * this.turningSpeed * Time.deltaTime, Space.World);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (groundCheck.IsGrounded)
            {
                this.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        this.hp -= damage;

        if (this.hp <= 0)
        {
            this.isDead = true;
            this.animator.SetTrigger(PlayerDiedString);
            this.PlayerDied?.Invoke();
            return;
        }

        this.animator.SetTrigger(GotHitString);
    }
}
