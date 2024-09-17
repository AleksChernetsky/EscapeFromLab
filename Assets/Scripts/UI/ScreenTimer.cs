using System.Diagnostics;

using TMPro;

using UnityEngine;

public class ScreenTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    private Stopwatch stopwatch = new Stopwatch();

    private void Start() => stopwatch.Start();
    private void Update() => _timerText.text = stopwatch.Elapsed.ToString("mm\\:ss");
}