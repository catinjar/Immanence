using System.Collections.Generic;

public class SetTracksReaction : Reaction {
    public List<MusicMixerTrack> tracks;

    public override void React()
        => MusicMixer.Instance.SetTracks(tracks);
}
