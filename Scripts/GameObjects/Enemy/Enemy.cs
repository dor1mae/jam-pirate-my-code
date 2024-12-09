using System;
using System.Collections;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public partial class Enemy : MonoBehaviour
{
    private int _id;

    [Header("References")]
    [SerializeField] private HealthController _healthController;
    [SerializeField] private NavMeshAgent _agent;
    
    [SerializeField] private Animator _animator;
    private Transform _playerTransform;

    [Header("Behaviour Settings")]
    public float sightRange = 10f;
    public float attackRange = 2.5f;
    public float runSpeedMultiplier = 2f;
    
    private bool playerSeen = false;
    private int lastSeenPlayer = 0;
    public bool inAttackRange = false;

    private float _timeNow;
    private float _timeBetweenChecks = 1f;

    private StateMachine _stateMachine;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Init(Hero player, SpawnPointType SpawnType, int id)
    {
        _playerTransform = player.GetComponent<Transform>();
        DefineStates(player, SpawnType);
        _id = id;
    }

    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

    public void DefineStates(Hero player, SpawnPointType SpawnType)
    {
        _stateMachine = new StateMachine();

        var spawnState = new EnemySpawnState(this, _animator, SpawnType);
        var wanderState = new EnemyWanderState(this, _animator, _agent, 10f, _id);
        var chaseState = new EnemyChaseState(this, _animator, _agent, player, _id);
        var searchState = new EnemySearchState(this, _animator, _agent, _playerTransform);
        var patrolState = new EnemyWanderState(this, _animator, _agent, 3f, _id);
        //var attackState = new EnemyAttackState(this, _animator, _agent, player);
        
        At(spawnState, wanderState, new FuncPredicate(() => (_animator.GetCurrentAnimatorStateInfo(0).IsName("WallSpawn") || _animator.GetCurrentAnimatorStateInfo(0).IsName("FloorSpawn")) && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1f));
        At(wanderState, chaseState, new FuncPredicate(() => playerSeen));
        At(chaseState, searchState, new FuncPredicate(() => !playerSeen));
        At(searchState, chaseState, new FuncPredicate(() => playerSeen));
        At(searchState, patrolState, new FuncPredicate(() => searchState.reachedLastSeenPlayer));
        At(patrolState, chaseState, new FuncPredicate(() => playerSeen));
        At(patrolState, wanderState, new FuncPredicate(() => lastSeenPlayer >= 10));
        //At(chaseState, attackState, new FuncPredicate(() => inAttackRange));
        //At(attackState, chaseState, new FuncPredicate(() => !inAttackRange));

        _stateMachine.SetState(spawnState);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventBus.Death += DieMaybe;
        _timeNow = 0;
    }


    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();
        _timeNow += Time.deltaTime;
        if (_timeNow >= _timeBetweenChecks)
        {
            CheckInRange();
            _timeNow = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }

    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    void LateUpdate()
    {
        
    }

    private void CheckInRange()
    {
        float distanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);
        if (distanceToPlayer <= sightRange)
        {
            Vector3 rayDirection = _playerTransform.transform.position - transform.position;
            if (!Physics.Raycast(transform.position, rayDirection, distanceToPlayer)) 
            {
                playerSeen = true;
                lastSeenPlayer = 0;
                CheckInAttackRange(distanceToPlayer);
            }
            else playerSeen = false;
        }
        else 
        {
            playerSeen = false;
            lastSeenPlayer +=1;
        }
    }
    
    private void CheckInAttackRange(float distanceToPlayer)
    {
        if (playerSeen && (distanceToPlayer <= attackRange)) inAttackRange = true;
        else inAttackRange = false;
    }

    private void DieMaybe(HealthController sender)
    {
        Enemy senderType = sender.GetComponentInParent<Enemy>();
        if (senderType != null) 
        {
            Destroy(gameObject);
        }
    }

}
