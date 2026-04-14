using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Zvuky")]
    public AudioClip explosionSound;
    public AudioClip lightningSound;
    public AudioClip arrowSound;
    public AudioClip teleportSound;
    public AudioClip pickaxeSound;
    public AudioClip meleeSound;
    public AudioClip deathSound;
    public AudioClip turnEndSound;

    private AudioSource _source;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        _source.PlayOneShot(clip, volume);
    }

    public void PlayExplosion() => Play(explosionSound);
    public void PlayLightning() => Play(lightningSound);
    public void PlayArrow() => Play(arrowSound);
    public void PlayTeleport() => Play(teleportSound);
    public void PlayPickaxe() => Play(pickaxeSound, 0.7f);
    public void PlayMelee() => Play(meleeSound);
    public void PlayDeath() => Play(deathSound);
    public void PlayTurnEnd() => Play(turnEndSound, 0.5f);
}