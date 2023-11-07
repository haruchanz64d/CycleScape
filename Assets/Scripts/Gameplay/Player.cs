using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [Header("Animation Constants")]
    private const string ANIMATION_IDLE = "Player_Idle";
    private const string ANIMATION_ATK = "Player_Attack";
    private const string ANIMATION_WALK = "Player_Walk";
    private const string ANIMATION_DEAD = "Player_Death";
    private const string ANIMATION_MOONWALK = "Player_Moonwalk";
    private Animator animator;
    [Header("Movement")]
    [SerializeField] private float m_movementSpeed = 6.5f;
    new Rigidbody2D rigidbody2D;
    private InputActions inputActions;   
    [Header("Shooting")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private bool isAttacking = false;
    private int arrowCount = 5;
    public int GetArrowCount() { return arrowCount; }
    private int maxArrowCount = 5;
    public int GetMaxArrowCount() { return maxArrowCount; }
    private float bowReloadTime = 1.5f;
    private bool isReloading = false;
    private float nextTimeToDrawArrow = 0f;

    private HealthSystem health;
    public bool isDead = false;

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
        if (inputActions.Gameplay.Attack.triggered && Time.time >= nextTimeToDrawArrow && arrowCount > 0)
        {
            isAttacking = true;
            HandleShooting();

            arrowCount--;

            if (arrowCount == 0)
            {
                isReloading = true;
                nextTimeToDrawArrow = Time.unscaledTime + bowReloadTime;
            }
        }
        else if (inputActions.Gameplay.Attack.IsPressed() && arrowCount > 0)
        {
            isAttacking = true;

            if (Time.time >= nextTimeToDrawArrow)
            {
                HandleShooting();

                arrowCount--;

                if (arrowCount == 0)
                {
                    isReloading = true;
                    nextTimeToDrawArrow = Time.unscaledTime + bowReloadTime;
                }
                else
                {
                    nextTimeToDrawArrow = Time.time + 0.2f;
                }
            }
        }
        else
        {
            isAttacking = false;
        }

        if (isReloading && Time.unscaledTime >= nextTimeToDrawArrow)
        {
            arrowCount = maxArrowCount;
            isReloading = false;
            nextTimeToDrawArrow = 0f;
        }

        HandleAnimations();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleAnimations()
    {
        if (isDead)
            return;
        //if (isAttacking) animator.Play(ANIMATION_ATK);
        if (rigidbody2D.velocity.magnitude > 0)
            animator.Play(ANIMATION_MOONWALK);
        else if (rigidbody2D.velocity.magnitude < 0)
            animator.Play(ANIMATION_WALK);
        else
            animator.Play(ANIMATION_IDLE);
    }

    private void HandleMovement()
    {
        Vector2 movement = inputActions.Gameplay.Move.ReadValue<Vector2>();
        rigidbody2D.velocity = new Vector2(movement.x * m_movementSpeed, movement.y * m_movementSpeed);
    }

    public void HandleDeathAnimation()
    {
        animator.Play(ANIMATION_DEAD);
    }

    private void HandleShooting()
    {
        Instantiate(arrowPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
    }
}