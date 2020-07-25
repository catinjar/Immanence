using System.Collections.Generic;
using UnityEngine;

public class MusicMixerSet : MonoBehaviour {
    public List<MusicMixerTrack> tracks;

    private void Awake()
        => MusicMixer.Instance.SetTracks(tracks);
}
