using UnityEngine;

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

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new InputActions();
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
        if (inputActions.Gameplay.Attack.triggered) HandleShooting();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // TODO: Fix the animation issues
        Vector2 movement = inputActions.Gameplay.Move.ReadValue<Vector2>();
        rigidbody2D.velocity = movement * m_movementSpeed;

        if (movement.x < 0)
        {
            transform.Rotate(0f, 180f, 0f);
        }
        else if (movement.x > 0)
        {
            transform.Rotate(0f, 0f, 0f);
        }
        else
        {
            animator.Play(ANIMATION_IDLE);
        }
    }

    private void HandleShooting()
    {
        animator.Play(ANIMATION_ATK);
        Instantiate(arrowPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
    }
}
