using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MusicMixerTrack {
    public AudioClip clip;
    public float maxVolume = 1.0f;
    public float fadeSpeed = 1.0f;
    public bool playOnce = false;
}

public class MusicMixerSource {
    public enum State {
        Stopped,
        FadeIn,
        FadeOut,
        Playing
    }

    public readonly MusicMixerTrack track;

    public State state;
    public AudioSource audioSource;

    public MusicMixerSource(MusicMixerTrack track) {
        this.track = track;
    }

    public void Update() {
        if (state == State.FadeIn || state == State.FadeOut) {
            float newVolume = audioSource.volume + track.fadeSpeed * Time.deltaTime * (state == State.FadeIn ? 1.0f : -1.0f);
            audioSource.volume = Mathf.Clamp(newVolume, 0.0f, track.maxVolume);

            if (state == State.FadeIn && audioSource.volume >= track.maxVolume) {
                state = State.Playing;
            }

            if (state == State.FadeOut && audioSource.volume <= 0.0f) {
                state = State.Stopped;
            }
        }
    }
}

public class MusicMixer : MonoBehaviour {
    private static MusicMixer s_instance;

    public static MusicMixer Instance {
        get {
            if (s_instance == null) {
                s_instance = new GameObject("MusicMixer").AddComponent<MusicMixer>();
                DontDestroyOnLoad(s_instance);
            }
            return s_instance;
        }
    }

    private List<MusicMixerSource> m_sources = new List<MusicMixerSource>();

    private void Update() {
        foreach (var source in m_sources) {
            source.Update();
        }

        m_sources.RemoveAll(source => {
            bool stopped = source.state == MusicMixerSource.State.Stopped;

            if (stopped) {
                Destroy(source.audioSource);
                return true;
            }

            return false;
        });
    }

    public void SetTracks(List<MusicMixerTrack> tracks) {
        // Fade out tracks that are missing
        foreach (var source in m_sources) {
            bool clipIsMissing = tracks.Where(t => t.clip == source.track.clip).Count() == 0;

            if (clipIsMissing) {
                source.state = MusicMixerSource.State.FadeOut;
            }
        }

        // And add clips that are not yet playing
        foreach (var track in tracks) {
            AddTrack(track);
        }
    }

    public void AddTrack(MusicMixerTrack track) {
        if (track.clip == null) {
            return;
        }

        bool clipIsPlaying = m_sources.Where(s => s.track.clip == track.clip).Count() != 0;

        if (!clipIsPlaying) {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = track.clip;
            audioSource.volume = 0.0f;
            audioSource.loop = !track.playOnce;
            audioSource.Play();

            var source = new MusicMixerSource(track);
            source.audioSource = audioSource;
            source.state = MusicMixerSource.State.FadeIn;

            m_sources.Add(source);
        }
    }

    public void SetTrackVolume(MusicMixerTrack track, float volume) {
        var source = m_sources.Find(s => s.track == track);
        if (source != null) {
            source.state = MusicMixerSource.State.Playing;
            source.audioSource.volume = Mathf.Clamp(volume, 0.0f, source.track.maxVolume);
        }
    }
}
