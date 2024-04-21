using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    private AudioSource _audio;
    public AudioMixer audioMixer;

    private void Awake()
    {
        _audio = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    void Start()
    {
        if (audioMixer != null)
        {
            SetAudioLowpass(false);
        }
        else
        {
            Debug.Log("audio mixer is null");
        }
    }

    public void PlayAudio()
    {
        _audio.Play();
    }

    public void SetAudioLowpass(bool fadeOut)
    {
        StartCoroutine(FadeAudioMixer("LowpassLevel", 0.50f, fadeOut ? 500f : 5000f));
    }

    private IEnumerator FadeAudioMixer(string paramName, float fadeTime, float targetValue)
    {
        audioMixer.GetFloat(paramName, out var start);

        var timeStartedLerping = Time.time;
        var timeSinceStarted = Time.time - timeStartedLerping;
        var percentageComplete = timeSinceStarted / fadeTime;
        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / fadeTime;

            var currentValue = Mathf.Lerp(start, targetValue, percentageComplete);
            audioMixer.SetFloat(paramName, currentValue);

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
