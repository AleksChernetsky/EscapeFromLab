using UnityEngine;

public class GlobalSoundHandler : MonoBehaviour
{
    [Header("Global sound")]
    [SerializeField] private AudioClip _winSound;
    private AudioSource _audioSource;

    [Header("Item sound")]
    [SerializeField] private AudioClip _onPickUpSound;
    [SerializeField] private AudioClip _onPutDownSound;

    [Header("Sound traps")]
    [SerializeField] private AudioClip[] _soundTrapClips;
    public AudioClip OnSoundTrapTrigger => _soundTrapClips[Random.Range(0, _soundTrapClips.Length)];

    public static GlobalSoundHandler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _audioSource = GetComponent<AudioSource>();
    }

    public void OnWinSound()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_winSound);
    }

    public void OnPickUpItem() => _audioSource.PlayOneShot(_onPickUpSound);
    public void OnPutDownItem() => _audioSource.PlayOneShot(_onPutDownSound);
}