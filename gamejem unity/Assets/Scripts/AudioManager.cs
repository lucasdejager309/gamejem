using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEffect {
    public AudioMixerGroup mixer;
    
    public string name;
    public AudioClip audioClip;

    [Header("Volume/Duration")]
    public bool fadeIn;
    public float fadeInTime;
    public float playFor;
    public bool loop;

    [Header("Dialogue")]
    public bool IsDialogue;
    [TextArea(3, 5)]
    public string Dialoguetext;
    public string speakerName;
    public AudioSource source;
    public float pauseAfter;
}

[System.Serializable]
    public class Conversation {
        public string name;
        public bool onlyPlayOnce;
        public bool played;
        public SoundEffect[] audio;
        public bool triggerOnEnd;
        public EventType trigger;

        [Header("trigger animation")]
        public bool triggerAnimationTriggerOnInteract;
        public string animTrigger;

    }

public class AudioManager : Singleton<AudioManager>
{
    [Header("mixers")]
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;
    public AudioMixer dialogueMixer;
    [Header("subtitles")]
    public Text subtitles;

    private AudioSource audioSource;

    void Awake() {
        Instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (subtitles == null) {
            subtitles = GameObject.FindGameObjectWithTag("Subtitles").GetComponent<Text>();
        }

        // if (PlayerPrefs.GetInt("subtitlesBool") == 0) {
        //     subtitles.enabled = false;
        // } else {
        //     subtitles.enabled = true;
        // }

        musicMixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume"))*20);
        soundMixer.SetFloat("soundVolume", Mathf.Log10(PlayerPrefs.GetFloat("soundVolume"))*20);
        dialogueMixer.SetFloat("dialogueVolume", Mathf.Log10(PlayerPrefs.GetFloat("soundVolume"))*20);
    }

    public IEnumerator PlaySound(SoundEffect sound) {
        Debug.Log("playing " + sound.name);
        
        if (sound.source == null) {
            GameObject source = new GameObject("audioSource");
            audioSource = source.AddComponent<AudioSource>();
        } else {
            if (sound.fadeIn) {
                GameObject source = new GameObject("audioSource");
                audioSource = source.AddComponent<AudioSource>();
                audioSource.spatialBlend = 0.9f;
                source.transform.position = sound.source.transform.position;
            } else {
                audioSource = sound.source;
            }
        }
        audioSource.outputAudioMixerGroup = sound.mixer;
        Play(sound, audioSource);
        
        if (sound.IsDialogue) {
            if (sound.playFor > 0) {
                yield return new WaitForSeconds(sound.playFor);
            } else {
                yield return new WaitForSeconds(sound.audioClip.length);
            }
            
        }
    }

    void Play(SoundEffect sound, AudioSource audioSource) {
        if (sound.fadeIn) {
            audioSource.volume = 0;
            StartCoroutine(FadeAudioSource.StartFade(audioSource, sound.fadeInTime, 1));
        }
        audioSource.clip = sound.audioClip;
        audioSource.Play();

        if (sound.playFor != 0) {
            StartCoroutine(Stop(audioSource, sound.playFor));
        }
        if (sound.loop) {
            StartCoroutine(Loop(sound, audioSource));
        }
    }

    IEnumerator Loop(SoundEffect sound, AudioSource source) {
        yield return new WaitForSeconds(sound.audioClip.length);
        if (source != null) Play(sound, audioSource);
    }
    
    IEnumerator Stop(AudioSource audioSource, float duration) {
        yield return new WaitForSeconds(duration);
        if (audioSource != null) {
            audioSource.Stop();
        }
    }

    public IEnumerator PlayConversation(SoundEffect[] voicelines) {
        
        foreach(SoundEffect voiceline in voicelines) {
            subtitles.text = voiceline.speakerName + ": " + voiceline.Dialoguetext;
            StartCoroutine(PlaySound(voiceline));

            if (voiceline.playFor > 0) {
                yield return new WaitForSeconds(voiceline.playFor + voiceline.pauseAfter);
            } else {
                yield return new WaitForSeconds(voiceline.audioClip.length + voiceline.pauseAfter);
            }
            subtitles.text = " ";
        }
    }
}
