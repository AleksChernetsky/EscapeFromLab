using UnityEngine;

public class SoundTrap : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {
        int triggerChance = Random.Range(0, 2); // chanse 50/50

        if (triggerChance == 1)
            _audioSource.PlayOneShot(GlobalSoundHandler.instance.OnSoundTrapTrigger);
    }
}
