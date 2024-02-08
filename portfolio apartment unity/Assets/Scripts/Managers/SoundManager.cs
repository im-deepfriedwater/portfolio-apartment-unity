
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class PlaySoundEvent : UnityEvent<AudioClip> { }

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class SoundManager : Singleton<SoundManager>
{   
    [SerializeField]
    private AudioSource dialogueBlipSource;

    [HideInInspector]
    public PlaySoundEvent PlayDialogueBlipEvent;

    [SerializeField]
    private AudioSource playerFootstepsSource;
    
    [HideInInspector]
    public PlaySoundEvent PlayPlayerFootstepsEvent;

    [SerializeField]
    private AudioSource followerFootstepsSource;

    [HideInInspector]
    public PlaySoundEvent PlayFollowerFootstepsEvent;

    [SerializeField]
    private AudioSource dialogueExclaimSource;
    
    [HideInInspector]
    public PlaySoundEvent PlayDialogueExclaimEvent;

    [SerializeField]
    private AudioSource dialogueSFXSource;

    [HideInInspector]
    public PlaySoundEvent PlayGlassBreakingEvent;

    [HideInInspector]
    public UnityEvent ToggleMuteEvent;
    [HideInInspector]
    public FloatEvent ChangeVolumeEvent;

    [SerializeField]
    private float globalVolume = 1f;

    [SerializeField]
    private bool isMuted = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayDialogueBlipEvent.AddListener((AudioClip clip) => OnPlayAudio(dialogueBlipSource, clip));
        PlayDialogueExclaimEvent.AddListener((AudioClip clip) => OnPlayAudio(dialogueExclaimSource, clip));
        PlayPlayerFootstepsEvent.AddListener((AudioClip clip) => OnPlayAudio(playerFootstepsSource, clip));
        PlayFollowerFootstepsEvent.AddListener((AudioClip clip) => OnPlayAudio(followerFootstepsSource, clip));
        PlayGlassBreakingEvent.AddListener((AudioClip clip) => OnPlayAudio(dialogueSFXSource, clip));

        ChangeVolumeEvent.AddListener((float newVolume) => globalVolume = newVolume);
        ToggleMuteEvent.AddListener(() => isMuted = !isMuted); 
    }

    void OnPlayAudio(AudioSource source, AudioClip clip)
    {
        if (isMuted) return;

        source.clip = clip;
        source.Play();
    }

}
