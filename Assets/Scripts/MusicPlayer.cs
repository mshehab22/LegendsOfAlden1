using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource;
    public AudioSource loopSource;

    void Start()
    {
        // start the intro now
        introSource.Play();

        // schedule the loop to begin exactly when the intro ends
        double startTime = AudioSettings.dspTime + introSource.clip.length;
        loopSource.loop = true;
        loopSource.PlayScheduled(startTime);
    }
}
