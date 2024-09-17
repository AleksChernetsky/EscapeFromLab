using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [Header("Movement")]
    private Vector3 _lerpedInput = Vector3.zero;
    private float _velocityLerp = 6f;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void MovementAnim(Vector2 inputValues, bool runInput, bool playerInput)
    {
        _lerpedInput = inputValues == Vector2.zero && _lerpedInput.magnitude < 0.1f
            ? Vector3.zero
            : Vector3.Lerp(_lerpedInput, inputValues, _velocityLerp * Time.deltaTime);

        SetMotionType(inputValues, runInput);

        if (playerInput)
        {
            _animator.SetFloat("InputX", _lerpedInput.x);
            _animator.SetFloat("InputY", _lerpedInput.y);
        }
        _animator.SetFloat("Velocity", _lerpedInput.magnitude);
    }
    private void SetMotionType(Vector2 inputValues, bool runInput)
    {
        _animator.SetBool("Run", runInput && inputValues != Vector2.zero);
    }
}
