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
        { AudioType.Main, "main_theme.mp3" },
        { AudioType.BlockFall, "block_fall.mp3" },
        { AudioType.LineClearing, "line_clearing.mp3" },
        { AudioType.NextLevel, "next_level.mp3" },
        { AudioType.GameOver, "game_over.wav" }
    };

    private const float BackgroundAudioVolume = 0.1f;
    private const float EventAudioVolume = 0.2f;
    
    private readonly WaveOutEvent _backgroundPlayer;
    private readonly WaveOutEvent _eventAudioPlayer;

    private AudioType? _currentEventAudioType;

    private static  readonly Dictionary<AudioType, int> AudioPriorities = new()
    {
        { AudioType.Main, 0 },
        { AudioType.BlockFall, 1 },
        { AudioType.LineClearing, 2 },
        { AudioType.NextLevel, 3 },
        { AudioType.GameOver, 4 }
    };

    public AudioManager()
    {
        _audioLibrary = new Dictionary<AudioType, AudioFileReader>();
        InitializeAudioLibrary();

        _backgroundPlayer = new WaveOutEvent();
        _backgroundPlayer.Init(_audioLibrary[AudioType.Main]);
        _backgroundPlayer.PlaybackStopped += (s, e) => RestartBackgroundMusic();
        
        _eventAudioPlayer = new WaveOutEvent();
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
        if (!IsAcceptableEvent(audioType))
            return;
        
        _currentEventAudioType = audioType;
        
        _eventAudioPlayer.Stop();
        _audioLibrary[audioType].Position = 0;
        _eventAudioPlayer.Init(_audioLibrary[audioType]);
        _eventAudioPlayer.Play();

        Task.Run(async () =>
        {
            await Task.Delay(_audioLibrary[audioType].TotalTime);
            if (_currentEventAudioType == audioType) 
            {
                _currentEventAudioType = null;
            }
        });
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
    
    private bool IsAcceptableEvent(AudioType audioType)
    {
        return _currentEventAudioType switch
        {
            AudioType.GameOver => false,
            null => true,
            _ => AudioPriorities[audioType] >= AudioPriorities[_currentEventAudioType.Value]
        };
    }

    private void InitializeAudioLibrary()
    {
        foreach (AudioType audioType in Enum.GetValues(typeof(AudioType)))
        {
            _audioLibrary[audioType] = new AudioFileReader(GetAudioFilePath(audioType));

            _audioLibrary[audioType].Volume = audioType is AudioType.Main
                ? BackgroundAudioVolume
                : EventAudioVolume;
        }
    }

    private static string GetAudioFilePath(AudioType audioType)
    {
        return AudioFilePath + AudioFileNames[audioType];
    }
}