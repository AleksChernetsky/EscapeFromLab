using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform _lookAtPoint;
    [SerializeField] private GameObject _endGameCamera;
    private float _rayDistance = 50f;

    public RaycastHit LookAtObject { get; private set; }
    public Vector3 LookDirection { get; private set; }

    private void Start()
    {
        QuestHandler.instance.OnEndGameEvent += OnEndGameCamera;
    }

    private void FixedUpdate()
    {
        if (!_endGameCamera.activeInHierarchy)
        {
            LookDirection = new Vector3(_lookAtPoint.position.x, transform.parent.position.y, _lookAtPoint.position.z) - transform.position;
            SetLookAtPosition();
        }
    }
    private void OnEndGameCamera(bool isWin)
    {
        _endGameCamera.SetActive(true);
    }
    private void SetLookAtPosition()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            LookAtObject = hit;
            _lookAtPoint.position = LookAtObject.point;
        }
        else
        {
            _lookAtPoint.position = ray.GetPoint(_rayDistance);
        }
    }
}