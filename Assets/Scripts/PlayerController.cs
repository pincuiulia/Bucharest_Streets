using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private int maxHP = 100;
    private int currentHP;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    public LayerMask solidObjectsLayer; // Layer-ul pentru obiectele solide
    public GameObject enemy; // Referință la obiectul inamic
    private bool isTakingDamage = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        currentHP = maxHP;

        Debug.Log("Awake: Initialized components");
    }

    private void OnEnable()
    {
        playerControls.Enable();
        Debug.Log("OnEnable: Player controls enabled");
    }

    private void OnDisable()
    {
        playerControls.Disable();
        Debug.Log("OnDisable: Player controls disabled");
    }

    private void Update()
    {
        PlayerInput();
        Debug.Log($"Update: Movement input - {movement}");

        // Verifică dacă tasta space este apăsată
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

        Debug.Log($"PlayerInput: moveX = {movement.x}, moveY = {movement.y}");
    }

    private void Move()
    {
        Vector2 newPosition = rb.position + movement * (moveSpeed * Time.fixedDeltaTime);

        // Verificăm dacă noua poziție este walkable
        if (isWalkable(newPosition))
        {
            rb.MovePosition(newPosition);
            Debug.Log($"Move: New position = {newPosition}");
        }
        else
        {
            Debug.Log($"Move: Position not walkable = {newPosition}");
        }
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            Debug.Log("AdjustPlayerFacingDirection: Facing left");
        }
        else
        {
            mySpriteRender.flipX = false;
            Debug.Log("AdjustPlayerFacingDirection: Facing right");
        }
    }

    private bool isWalkable(Vector3 targetPos)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer);
        bool walkable = hitCollider == null;
        Debug.Log($"isWalkable: targetPos = {targetPos}, walkable = {walkable}");
        return walkable;
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
        Debug.Log("Attack: Triggered attack animation");

        // Verifică dacă inamicul este aproape
        float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
        if (distanceToEnemy < 1.5f)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(25);
                Debug.Log("Attack: Enemy took 25 damage");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isTakingDamage) return;

        currentHP -= damage;
        Debug.Log($"Player: Took {damage} damage, current HP = {currentHP}");

        StartCoroutine(DamageEffect());

        if (currentHP <= 0)
        {
            // Gestionare moarte player
            Debug.Log("Player: Died");
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageEffect()
    {
        isTakingDamage = true;
        for (int i = 0; i < 3; i++)
        {
            mySpriteRender.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            mySpriteRender.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        isTakingDamage = false;
    }
}
