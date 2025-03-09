using NAudio.Wave;

namespace Tetris.Utilities;

public enum AudioType
{
    Main,
    BlockFall,
    LineClearing,
    NextLevel,
    GameOver
}

public class AudioManager
{
    private readonly Dictionary<AudioType, AudioFileReader> _audioLibrary;
    
    private const string AudioFilePath = @"Data\Sounds\";

    private static readonly Dictionary<AudioType, string> AudioFileNames = new()
    {
        { AudioType.Main, "main_theme_1.mp3" },
        { AudioType.BlockFall, "block_fall.mp3" },
        { AudioType.LineClearing, "line_clearing.mp3" },
        { AudioType.NextLevel, "next_level.mp3" },
        { AudioType.GameOver, "game_over.wav" }
    };

    private readonly IWavePlayer _backgroundPlayer;
    private readonly IWavePlayer _eventAudioPlayer;

    public AudioManager()
    {
        _audioLibrary = new Dictionary<AudioType, AudioFileReader>();
        InitializeAudioLibrary();

        _backgroundPlayer = new WaveOutEvent();
        _backgroundPlayer.Init(_audioLibrary[AudioType.Main]);
        _backgroundPlayer.Volume = 0.1f;
        _backgroundPlayer.PlaybackStopped += (s, e) => RestartBackgroundMusic();
        
        _eventAudioPlayer = new WaveOutEvent();
        _eventAudioPlayer.Volume = 1.0f;
    }
    
    public void PlayBackgroundMusic()
    {
        _eventAudioPlayer.Stop();
        _audioLibrary[AudioType.Main].Position = 0;
        _backgroundPlayer.Play();
    }

    public void ResumeBackgroundMusic()
    {
        _backgroundPlayer.Play();
    }

    private void RestartBackgroundMusic()
    {
        _backgroundPlayer.Stop();
        PlayBackgroundMusic();
    }

    public void PauseBackgroundMusic()
    {
        _backgroundPlayer.Pause();
    }

    public void Play(AudioType audioType)
    {
        _eventAudioPlayer.Stop();
        _audioLibrary[audioType].Position = 0;
        _eventAudioPlayer.Init(_audioLibrary[audioType]);
        _eventAudioPlayer.Play();
    }

    public void Dispose()
    {
        _backgroundPlayer.Stop();
        _eventAudioPlayer.Stop();
        
        _backgroundPlayer.Dispose();
        _eventAudioPlayer.Dispose();
        
        foreach (var audio in _audioLibrary)
        {
            audio.Value.Dispose();
        }
    }

    private void InitializeAudioLibrary()
    {
        foreach (AudioType audioType in Enum.GetValues(typeof(AudioType)))
        {
            _audioLibrary[audioType] = new AudioFileReader(GetAudioFilePath(audioType));
        }
    }

    private string GetAudioFilePath(AudioType audioType)
    {
        return AudioFilePath + AudioFileNames[audioType];
    }
}