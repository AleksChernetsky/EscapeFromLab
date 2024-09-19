using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] private Transform _body;
    private float _walkSpeed = 2f;
    private float _runSpeed = 4f;
    private float _gravity = -9.81f;
    private float _verticalVelocity = 0;

    [Header("Movement sound")]
    [SerializeField] private AudioClip[] _stepSounds;
    private float _stepInterval = 0.5f;
    private float _stepTimer;

    [Header("References")]
    private AudioSource _audioSource;
    private CharacterController _characterController;
    private PlayerInputService _playerinput;
    private AnimationHandler _animationHandler;
    private CameraHandler _cameraHandler;
    private Inventory _inventory;

    private void OnEnable()
    {
        _playerinput = new PlayerInputService();

        _playerinput.Initialize();
        _playerinput.OnInteractInput += Interact;
    }
    private void OnDisable()
    {
        _playerinput.DisableInput();
        _playerinput.OnInteractInput -= Interact;

        QuestHandler.instance.OnEndGameEvent -= OnQuestCompleted;
    }
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();

        _animationHandler = GetComponentInChildren<AnimationHandler>();
        _cameraHandler = GetComponentInChildren<CameraHandler>();
        _inventory = GetComponentInChildren<Inventory>();

    }
    private void Start()
    {
        QuestHandler.instance.OnEndGameEvent += OnQuestCompleted;        
    }

    private void Update()
    {
        Movement(_playerinput.MovementInput, _playerinput.RunInput);
    }

    #region Actions
    private void Movement(Vector3 inputDirection, bool runInput)
    {
        // set body direction
        _body.forward = _cameraHandler.LookDirection.normalized;

        // set move direction
        Vector3 moveDirection = _body.right * inputDirection.x + _body.forward * inputDirection.y;
        _stepInterval = runInput ? 0.3f : 0.5f;
        _characterController.Move(moveDirection * (runInput ? _runSpeed : _walkSpeed) * Time.deltaTime);
        FootStepSound();
        CalculateGravity();

        // set animation
        _animationHandler.MovementAnim(inputDirection, runInput, true);
    }
    private void CalculateGravity()
    {
        _verticalVelocity = _characterController.isGrounded ? -1f : _verticalVelocity + _gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, _verticalVelocity, 0);
        _characterController.Move(gravityMove * Time.deltaTime);
    }
    private void Interact()
    {
        // check if object in front
        RaycastHit interactedObject = _cameraHandler.LookAtObject;
        if (interactedObject.distance >= 2.5f)
            return;

        // add object to inventory if pickable
        if (interactedObject.collider.TryGetComponent(out IPickable pickable))
        {
            _inventory.AddItem(pickable);
            return;
        }

        // place object on putable if it possible
        if (interactedObject.collider.TryGetComponent(out IPutable putable))
        {
            putable.PutItem(_inventory);
        }
    }
    #endregion

    private void FootStepSound()
    {
        if (_characterController.velocity.magnitude > 0.1f)
        {
            _stepTimer += Time.deltaTime;
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
    private void OnQuestCompleted(bool isWin)
    {
        _playerinput.DisableInput();
    }
}
