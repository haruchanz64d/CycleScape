using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    [Header("Animation Constants")]
    private const string ANIMATION_IDLE = "Player_Idle";
    private const string ANIMATION_ATK = "Player_Attack";
    private const string ANIMATION_WALK = "Player_Walk";
    private const string ANIMATION_DEAD = "Player_Dead";
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float m_movementSpeed = 5.0f;
    new Rigidbody2D rigidbody2D;
    private InputActions inputActions;

    [Header("Shooting")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject arrowPrefab;
    private bool isAttacking = false;

    private HealthSystem health;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new InputActions();
        health = GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void Update()
    {
        if (inputActions.Gameplay.Attack.triggered) 
            HandleShooting();
        HandleAnimations();

        // Debugging Purposes
        if (Input.GetKey(KeyCode.LeftControl))
        {
            health.TakeDamage(1.0f);
        }
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleAnimations()
    {
        if (isAttacking)
            animator.Play(ANIMATION_ATK);
        else if (Mathf.Abs(rigidbody2D.velocity.x) > 0.1f)
        {
            animator.Play(ANIMATION_WALK);
            if (rigidbody2D.velocity.x > 0)
                transform.localScale = new Vector3(2.5f, 2.5f, 1);
            else
                transform.localScale = new Vector3(-2.5f, 2.5f, 1);
        }
        else
            animator.Play(ANIMATION_IDLE);
    }

    private void HandleMovement()
    {
        Vector2 movement = inputActions.Gameplay.Move.ReadValue<Vector2>();
        rigidbody2D.velocity = new Vector2(movement.x * m_movementSpeed, rigidbody2D.velocity.y);

        if (movement.x < 0)
            transform.localScale = new Vector3(-2.5f, 2.5f, 1);
        else if (movement.x > 0)
            transform.localScale = new Vector3(2.5f, 2.5f, 1);
    }

    private void HandleShooting()
    {
        Instantiate(arrowPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
    }

}
