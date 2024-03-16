using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private int currentTrackIndex = 0; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextTrack();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (currentTrackIndex < musicTracks.Length)
        {
            audioSource.clip = musicTracks[currentTrackIndex];
            audioSource.Play();
            currentTrackIndex++;
        }
        else
        {
            currentTrackIndex = 0;
            audioSource.clip = musicTracks[currentTrackIndex];
            audioSource.Play();
            currentTrackIndex++;
        }
    }
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
