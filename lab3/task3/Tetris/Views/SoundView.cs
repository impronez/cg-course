using Tetris.Models;
using Tetris.Utilities;

namespace Tetris.Views
{
    public class SoundView
    {
        private readonly AudioManager _audioManager;

        public SoundView(GameModel gameModel)
        {
            _audioManager = new AudioManager();

            gameModel.Board.BoardEvent += (s, e) => ProcessBoardEvent(e);

            gameModel.GameStateChanged += OnGameStateChanged;

            gameModel.LevelChanged += OnLevelChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Running:
                    _audioManager.ResumeBackgroundMusic();
                    break;
                case GameState.Paused:
                    _audioManager.PauseBackgroundMusic();
                    break;
                case GameState.GameOver:
                    _audioManager.PauseBackgroundMusic();
                    _audioManager.Play(AudioType.GameOver);
                    break;
            }
        }

        private void OnLevelChanged()
        {
            _audioManager.Play(AudioType.NextLevel);
        }

        private void ProcessBoardEvent(BoardEventType eventType)
        {
            switch (eventType)
            {
                case BoardEventType.BlockFall:
                    _audioManager.Play(AudioType.BlockFall);
                    break;
                case BoardEventType.LineClearing:
                    _audioManager.Play(AudioType.LineClearing);
                    break;
            }
        }

        public void Dispose()
        {
            _audioManager.Dispose();
        }
    }
}
