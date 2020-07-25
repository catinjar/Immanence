using System.Collections.Generic;
using UnityEngine;

public class RandomAudioSource : MonoBehaviour {
    [SerializeField]
    private AudioSource m_source;
    [SerializeField]
    private List<AudioClip> m_clips;

    private int lastIndex = -1;

    public void Play() {
        int index = lastIndex;

        if (m_clips.Count > 1) {
            while (lastIndex == index) {
                index = Random.Range(0, m_clips.Count - 1);
            }
        }
        else if (m_clips.Count == 1) {
            index = 0;
        }
        else {
            return;
        }

        m_source.clip = m_clips[index];
        m_source.Play();

        lastIndex = index;
    }
}
