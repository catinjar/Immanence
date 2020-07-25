using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzles/ServerPuzzle", order = 0)]
public class ServerPuzzle : ScriptableObject {
    public enum Stage {
        DisableMainServer,
        EnableGenerator,
        GeneratorPower,
        EnableCommutator,
        OpenPorts,
        EnableBigServer,
        EnableBoundServers,
        EnableMainServer,
        Complete
    }

    [Header("Stage")]
    public Stage stage;
    public bool enteredManCorridor;

    [Header("Power")]
    public int generatorPower;

    [Header("Ports")]
    public bool firstPort;
    public bool secondPort;
    public bool thirdPort;

    [Header("Servers")]
    public bool leftServer;
    public bool rightServer;

    [Header("MusixMixerTracks")]
    public MusicMixerTrack enterManCorridorTrack;
    public MusicMixerTrack enableGeneratorTrack;
    public MusicMixerTrack enableGeneratorPowerTrack;
    public MusicMixerTrack enableCommutatorTrack;
    public MusicMixerTrack openPort1Track;
    public MusicMixerTrack openPort2Track;
    public MusicMixerTrack openPort3Track;
    public MusicMixerTrack enableBigServerTrack;
    public MusicMixerTrack enableLeftServerTrack;
    public MusicMixerTrack enableRightServerTrack;
    public MusicMixerTrack enableMainServerTrack;

    public ServerPuzzle() {
        stage = Stage.DisableMainServer;
        enteredManCorridor = false;
        generatorPower = 0;
        firstPort   = false;
        secondPort  = false;
        thirdPort   = false;
        leftServer  = false;
        rightServer = false;
    }

    public void EnterManCorridor() {
        if (!enteredManCorridor) {
            MusicMixer.Instance.SetTracks(new List<MusicMixerTrack>() { enterManCorridorTrack });
            enteredManCorridor = true;
        }
    }

    public void DisableMainServer() {
        ++stage;
        MessageManager.Instance.PushMessages("DisabledMainServer");
        MusicMixer.Instance.SetTracks(new List<MusicMixerTrack>() {
            enableGeneratorTrack,
            enableGeneratorPowerTrack,
            enableCommutatorTrack,
            openPort1Track,
            openPort2Track,
            openPort3Track,
            enableBigServerTrack,
            enableLeftServerTrack,
            enableRightServerTrack,
            enableMainServerTrack,
        });

        MusicMixer.Instance.SetTrackVolume(enableGeneratorTrack, 0.0f);
        MusicMixer.Instance.SetTrackVolume(enableGeneratorPowerTrack, 0.0f);
        MusicMixer.Instance.SetTrackVolume(enableCommutatorTrack, 0.0f);
        MusicMixer.Instance.SetTrackVolume(openPort1Track, 0.0f);
        MusicMixer.Instance.SetTrackVolume(openPort2Track, 0.0f);
        MusicMixer.Instance.SetTrackVolume(openPort3Track, 0.0f);
        MusicMixer.Instance.SetTrackVolume(enableBigServerTrack, 0.0f);
        MusicMixer.Instance.SetTrackVolume(enableLeftServerTrack, 0.0f);
        MusicMixer.Instance.SetTrackVolume(enableRightServerTrack, 0.0f);
        MusicMixer.Instance.SetTrackVolume(enableMainServerTrack, 0.0f);
    }

    public void EnableGenerator() {
        ++stage;
        MessageManager.Instance.PushMessages("EnabledGenerator");
        MusicMixer.Instance.SetTrackVolume(enableGeneratorTrack, 0.4f);
    }

    public void PowerUp() {
        if (generatorPower < 10) {
            ++generatorPower;
            MusicMixer.Instance.SetTrackVolume(enableGeneratorTrack, 0.4f + 0.06f * generatorPower);
        }
        else {
            MessageManager.Instance.PushMessages("CantPowerUp");
        }
    }

    public void PowerDown() {
        if (generatorPower > 0) {
            --generatorPower;
            MusicMixer.Instance.SetTrackVolume(enableGeneratorTrack, 0.4f + 0.06f * generatorPower);
        }
        else {
            MessageManager.Instance.PushMessages("CantPowerDown");
        }
    }

    public void EnableGeneratorPower() {
        if (generatorPower == 6) {
            ++stage;
            MessageManager.Instance.PushMessages("EnabledGeneratorPower");
            ContactsManager.Instance.Send("Girl", "EnabledGeneratorPower");
            MusicMixer.Instance.SetTrackVolume(enableGeneratorPowerTrack, 1.0f);
        }
        else if (generatorPower > 6) {
            --stage;
            generatorPower = 0;
            MessageManager.Instance.PushMessages("TooMuchPower");
            MusicMixer.Instance.SetTrackVolume(enableGeneratorPowerTrack, 0.0f);
        }
        else if (generatorPower < 6) {
            MessageManager.Instance.PushMessages("NotEnoughPower");
        }
    }

    public void EnableCommutator() {
        ++stage;
        MessageManager.Instance.PushMessages("EnabledCommutator");
        MusicMixer.Instance.SetTrackVolume(enableCommutatorTrack, 1.0f);
    }

    public void OpenFirstPort() {
        MessageManager.Instance.PushMessages(firstPort ? "OpenedPortAlready" : "OpenedPort");
        firstPort = true;
        CheckPorts();
        MusicMixer.Instance.SetTrackVolume(openPort1Track, 1.0f);
    }

    public void OpenSecondPort() {
        MessageManager.Instance.PushMessages(secondPort ? "OpenedPortAlready" : "OpenedPort");
        secondPort = true;
        CheckPorts();
        MusicMixer.Instance.SetTrackVolume(openPort2Track, 1.0f);
    }

    public void OpenThirdPort() {
        MessageManager.Instance.PushMessages(thirdPort ? "OpenedPortAlready" : "OpenedPort");
        thirdPort = true;
        CheckPorts();
        MusicMixer.Instance.SetTrackVolume(openPort3Track, 1.0f);
    }

    public void CheckPorts() {
        if (firstPort &&
            secondPort &&
            thirdPort) {
            ++stage;
            MessageManager.Instance.PushMessages("OpenedAllPorts");
            ContactsManager.Instance.Send("Girl", "CheckedPorts");
        }
    }

    public void EnableBigServer() {
        ++stage;
        MessageManager.Instance.PushMessages("EnabledBigServer");
        MusicMixer.Instance.SetTrackVolume(enableBigServerTrack, 1.0f);
    }

    public void EnableLeftServer() {
        leftServer = true;
        CheckServers();
        MusicMixer.Instance.SetTrackVolume(enableLeftServerTrack, 1.0f);
    }

    public void EnableRightServer() {
        rightServer = true;
        CheckServers();
        MusicMixer.Instance.SetTrackVolume(enableRightServerTrack, 1.0f);
    }

    public void CheckServers() {
        if (leftServer &&
            rightServer) {
            ++stage;
            MessageManager.Instance.PushMessages("EnabledBoundServers");
            ContactsManager.Instance.Send("Girl", "CheckedServers");
        }
    }

    public void EnableMainServer() {
        ++stage;
        MusicMixer.Instance.SetTrackVolume(enableMainServerTrack, 1.0f);
    }
}
