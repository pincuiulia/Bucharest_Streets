using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;  // Referința la jucător
    public float moveSpeed = 2f;  // Viteza de deplasare a inamicului
    public float attackRange = 1f;  // Distanța la care inamicul începe atacul
    public float attackDelay = 0.5f;  // Delay-ul înainte de atac

    [SerializeField] private int maxHP = 100;
    private int currentHP;
    private SpriteRenderer spriteRenderer;
    private bool isTakingDamage = false;
    private Animator animator;
    private Vector3 originalScale;
    private bool isAttacking = false;
    private bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        Debug.Log("Enemy initialized with HP: " + currentHP);
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                // Rotirea sprite-ului pentru a se oglindi corect
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                }

                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
            else
            {
                animator.SetBool("isRunning", false);
                if (canAttack)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        canAttack = false;
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackDelay);  // Delay-ul înainte de atac

        // Aplica daune jucatorului daca este in raza de atac
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(15);
                Debug.Log("Enemy: Player took 15 damage");
            }
        }

        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackDelay);  // Delay-ul înainte de a putea ataca din nou
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy collided with: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null && isAttacking)
            {
                player.TakeDamage(15);
                Debug.Log("Enemy: Player took 15 damage");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isTakingDamage) return;

        currentHP -= damage;
        Debug.Log($"Enemy: Took {damage} damage, current HP = {currentHP}");

        StartCoroutine(DamageEffect());

        if (currentHP <= 0)
        {
            Debug.Log("Enemy: Died");
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageEffect()
    {
        isTakingDamage = true;
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        isTakingDamage = false;
    }
}
