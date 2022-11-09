using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private List<AudioSource> _audioSources = new List<AudioSource>();
    private List<AudioClip> _sounds = new List<AudioClip>();

    private int _currentAudioSource;

    private void OnEnable()
    {
        var resSounds = Resources.LoadAll("Sounds", typeof(AudioClip));
        foreach (var sound in resSounds)
        {
            _sounds.Add((AudioClip)sound);

            for(int i = 0; i < 4; i++)
                AddAudioSource();
        }
    }

    public void PlaySound()
    {
        var soundIndex = Random.Range(0, _sounds.Count);

        if (_audioSources[_currentAudioSource].isPlaying)
            _audioSources[_currentAudioSource].Stop();

        _audioSources[_currentAudioSource].PlayOneShot(_sounds[soundIndex]);

        _currentAudioSource++;
        if (_currentAudioSource >= _audioSources.Count)
            _currentAudioSource = 0;
    }

    private void AddAudioSource()
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        _audioSources.Add(audioSource);
    }
}
