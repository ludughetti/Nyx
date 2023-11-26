using UnityEngine;

//TODO: TP2 - Fix - Repeated code in Boss
public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float enemyDamage;
    private EnemyMovement enemyMovement;

    private float attackDelay = 0.5f;
    private float timerBetweenAttacks = 0f;

    //Audio
    [SerializeField] private CharacterAudioManager audioManager;

    private void Start()
    {
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    private void Update()
    {
        //TODO: TP2 - Could be a coroutine/Invoke
        if (enemyMovement.isAttacking)
        {
            timerBetweenAttacks -= Time.deltaTime;
            if (timerBetweenAttacks <= 0)
            {
                enemyMovement.isAttacking = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: TP2 - Fix - Hardcoded value/s
        if (collision.gameObject.tag == "Player" && timerBetweenAttacks <= 0)
        {
            enemyMovement.isAttacking = true;
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
            timerBetweenAttacks = attackDelay;

            audioManager.PlayCharacterAttack();
        }
    }
}
