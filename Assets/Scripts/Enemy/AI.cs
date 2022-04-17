
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    // Information from NPCEyes is used to change the enemy state and start attack animation sequence
    
    private NavMeshAgent Agent;
    private Animator anim = new Animator();
    public Transform player;
    private State currentState;
    private EnemyAttackAnimation attackAnimation;
    private NPCEyes eyes;
    private bool SetupAttack = true;
    public bool showHealthBar;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        eyes = GetComponent<NPCEyes>();
        attackAnimation = GetComponent<EnemyAttackAnimation>();
        currentState = new Idle(gameObject, Agent, anim, player);
    }
    void Update()
    {
        showHealthBar = false;

        if (eyes.Seeing == Seeing.Player)
        {
            currentState.Player = player;
            currentState.Seeing = Seeing.Player;
            SetupAttack = true;
            showHealthBar = true;
        }
        
        if (currentState is Attack attack && SetupAttack)
        {
            SetupAttack = false;
            attack.AttackAnimation = attackAnimation;
            // var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            // Smoothly rotate towards the target point.
            // transform.rotation = Quaternion.Slerp(transform.rotation.normalized, targetRotation.normalized, 180 * Time.deltaTime);            
            showHealthBar = true;
        }
        
        if (eyes.Seeing == Seeing.Nothing && !SetupAttack)
        {
            SetupAttack = true;
            showHealthBar = false;
        }
        
        if (currentState is Pursue pursue && !SetupAttack)
        {
            SetupAttack = true;
            showHealthBar = true;
        }
        currentState = currentState.Process();
    }
}
