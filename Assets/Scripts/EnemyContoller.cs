using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;
    private SpriteRenderer spriteRenderer;
    private bool isTakingDamage = false;

    private void Start()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
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
