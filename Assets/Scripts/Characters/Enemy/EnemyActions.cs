using UnityEngine;
using UnityEngine.AI;

public class EnemyActions : MonoBehaviour
{
    [SerializeField] private AudioClip[] _stepSounds;
    private float _stepTimer;
    private float _stepInterval;

    private NavMeshAgent _agent;
    private AnimationHandler _animationHandler;
    private AudioSource _audioSource;
    private EnemyNavigation _enemyNavigation;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animationHandler = GetComponent<AnimationHandler>();
        _audioSource = GetComponent<AudioSource>();
        _enemyNavigation = GetComponent<EnemyNavigation>();
    }
    private void Update()
    {
        MovementEffect();
    }
    private void MovementEffect()
    {
        _animationHandler.MovementAnim(_agent.velocity, _enemyNavigation.IsRunning, false);
        FootStepSound();
    }
    private void FootStepSound()
    {
        if (_agent.velocity.magnitude > 0.1f)
        {
            _stepTimer += Time.deltaTime;
            _stepInterval = _enemyNavigation.IsRunning ? 0.3f : 0.5f;

            if (_stepTimer >= _stepInterval)
            {
                _audioSource.pitch = Random.Range(0.95f, 1.1f);
                _audioSource.PlayOneShot(_stepSounds[Random.Range(0, _stepSounds.Length)]);
                _stepTimer = 0f;
            }
        }
        else
        {
            _stepTimer = 0f;
        }
    }
}