using UnityEngine;

[System.Serializable]
public class AudioSegment
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public bool playOnAwake;
    [Range(-3, 3)] public float audioPitch;
    [Range(0, 1)] public float audioVolume;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSegment[] _audioSegments;

    private void Awake()
    {
        foreach (var segment in _audioSegments)
        {
            ConfigureAudioSource(segment);
        }
    }

    private void ConfigureAudioSource(AudioSegment segment)
    {
        var audioSource = segment.audioSource;
        audioSource.clip = segment.audioClip;
        audioSource.playOnAwake = segment.playOnAwake;
        audioSource.pitch = segment.audioPitch;
        audioSource.volume = segment.audioVolume;
    }

    public void PlaySegmentAudio(int playIndex)
    {
        _audioSegments[playIndex].audioSource.Play();
    }
}
