using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private LayerMask _playerLayer;
    private Collider[] _playerCollider;
    private bool _playerBlocked;
    private float _distanceToCheck = 5f;
    private float _checkDelay;

    public bool IsRunning { get; private set; }

    private NavMeshAgent _agent;
    private Transform _player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        SetAgentDestination();
    }
    private void FixedUpdate()
    {
        DetectPlayer();
    }
    private void SetAgentDestination()
    {
        if (_player != null && !PlayerBlocked())
        {
            _agent.SetDestination(_player.position);

            if (Vector3.Distance(transform.position, _player.position) < 0.5f)
            {
                _agent.isStopped = true;
                QuestHandler.instance.OnEndGame(false);
            }
        }
        else
        {
            if (!_agent.hasPath)
            {
                int randomPoint = Random.Range(0, _patrolPoints.Length);
                _agent.SetDestination(_patrolPoints[randomPoint].position);
            }
        }
    }
    private void DetectPlayer()
    {
        _checkDelay += Time.deltaTime;
        if (_checkDelay >= 0.5f)
        {
            _playerCollider = Physics.OverlapSphere(transform.position, _distanceToCheck, _playerLayer);
            for (int i = 0; i < _playerCollider.Length; i++)
            {
                _player = _playerCollider[i].transform;
            }
            PlayerBlocked();
            _checkDelay = 0;
        }
    }
    private bool PlayerBlocked()
    {
        NavMeshHit hit;
        if (_player != null)
            _playerBlocked = NavMesh.Raycast(transform.position, _player.transform.position, out hit, NavMesh.AllAreas);

        return _playerBlocked;
    }
}
