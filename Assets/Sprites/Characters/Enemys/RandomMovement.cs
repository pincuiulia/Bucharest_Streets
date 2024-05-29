using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // Viteza de mișcare
    public float moveInterval = 2f; // Intervalul de timp la care inamicul schimbă direcția
    public Animator animator; // Referință la Animator
    public LayerMask solidObjectsLayer; // Layer pentru obiecte solide

    private Vector2 movement; // Vectorul de mișcare curent
    private float moveTimer;
    private bool facingRight = true; // Variabilă pentru a verifica direcția în care se uită personajul
    private Rigidbody2D rb; // Referință la Rigidbody2D

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        moveTimer = moveInterval;
        SetRandomMovement();
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            SetRandomMovement();
            moveTimer = moveInterval;
        }

        UpdateAnimator();
        FlipCharacter();
    }

    void FixedUpdate()
    {
        Move();
    }

    void SetRandomMovement()
    {
        float moveX = Random.Range(-1f, 1f);
        float moveY = Random.Range(-1f, 1f);
        movement = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        Vector2 newPosition = rb.position + movement * (moveSpeed * Time.fixedDeltaTime);

        // Verificăm dacă noua poziție este walkable
        if (isWalkable(newPosition))
        {
            rb.MovePosition(newPosition);
        }
        else
        {
            // Dacă poziția nu este walkable, setează o nouă mișcare aleatorie
            SetRandomMovement();
        }
    }

    bool isWalkable(Vector2 targetPos)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer);
        return hitCollider == null;
    }

    void UpdateAnimator()
    {
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("IsMoving", movement != Vector2.zero);
    }

    void FlipCharacter()
    {
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inversează scala pe axa X
        transform.localScale = scale;
    }
}
