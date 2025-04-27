using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Saem;

public class GameService : Service<GameService>
{
    // 게임 상태 열거형
    public enum InGameState
    {
        Ready,      // 게임 시작 전
        Playing,    // 게임 진행 중
        Paused,     // 게임 일시 정지
        GameOver    // 게임 종료
    }
    
    // 점수 관리
    public ReactiveProperty<int> Score { get; private set; } = new ReactiveProperty<int>(0);
    
    // 게임 상태 관리
    private ReactiveProperty<InGameState> _gameState = new ReactiveProperty<InGameState>(InGameState.Ready);
    public IReadOnlyReactiveProperty<InGameState> GameState => _gameState;
    
    // 게임 시간 관리 (1에서 0으로 줄어듦)
    private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>(1f);
    public IReadOnlyReactiveProperty<float> GameTime => _gameTime;
    
    // 게임 제한 시간 (초)
    private float _timeLimit = 15f;
    public float TimeLimit
    {
        get => _timeLimit;
        set
        {
            _timeLimit = value;
            // 시간 제한이 변경되면 게임 시간을 리셋
            if (_gameState.Value == InGameState.Playing)
            {
                _gameTime.Value = 1f;
            }
        }
    }
    
    // 게임 시간 업데이트를 위한 IDisposable
    private IDisposable _timeUpdateDisposable;
    
    // 상태 변화 스트림
    public IObservable<InGameState> OnGameStateChanged => _gameState.AsObservable();
    
    // 점수 변화 스트림
    public IObservable<int> OnScoreChanged => Score.AsObservable();
    
    // 시간 변화 스트림
    public IObservable<float> OnTimeChanged => _gameTime.AsObservable();
    
    // 게임 시작 시 초기화
    public void InitializeGame()
    {
        Score.Value = 0;
        _gameTime.Value = 1f;
        _gameState.Value = InGameState.Ready;
    }
    
    // 게임 시작
    public void StartGame()
    {
        if (_gameState.Value == InGameState.Ready || _gameState.Value == InGameState.Paused)
        {
            _gameState.Value = InGameState.Playing;
            
            // 게임 시간 업데이트 시작
            _timeUpdateDisposable?.Dispose();
            _timeUpdateDisposable = Observable.EveryUpdate()
                .Where(_ => _gameState.Value == InGameState.Playing)
                .Subscribe(_ =>
                {
                    float elapsedTime = (1f - _gameTime.Value) * _timeLimit + Time.deltaTime;
                    _gameTime.Value = Mathf.Max(0f, 1f - (elapsedTime / _timeLimit));
                    
                    // 시간이 0이 되면 게임 오버
                    if (_gameTime.Value <= 0f)
                    {
                        EndGame();
                    }
                });
        }
    }
    
    // 게임 일시 정지
    public void PauseGame()
    {
        if (_gameState.Value == InGameState.Playing)
        {
            _gameState.Value = InGameState.Paused;
            _timeUpdateDisposable?.Dispose();
        }
    }
    
    // 게임 재개
    public void ResumeGame()
    {
        if (_gameState.Value == InGameState.Paused)
        {
            StartGame();
        }
    }
    
    // 게임 종료
    public void EndGame()
    {
        if (_gameState.Value == InGameState.Playing)
        {
            _gameState.Value = InGameState.GameOver;
            _timeUpdateDisposable?.Dispose();
        }
    }
    
    // 게임 재시작
    public void RestartGame()
    {
        InitializeGame();
        StartGame();
    }
    
    // 점수 추가
    public void AddScore(int amount)
    {
        Score.Value += amount;
    }

    // 점수 초기화
    public void ResetScore()
    {
        Score.Value = 0;
    }
    
    // 게임 시간 초기화
    public void ResetGameTime()
    {
        _gameTime.Value = 1f;
    }
    
    // 남은 시간 계산 (초)
    public float GetRemainingTime()
    {
        return _gameTime.Value * _timeLimit;
    }
    
    // 게임 진행률 계산 (1~0)
    public float GetGameProgress()
    {
        return _gameTime.Value;
    }
    
    // 게임이 진행 중인지 확인
    public bool IsPlaying()
    {
        return _gameState.Value == InGameState.Playing;
    }
    
    // 게임이 일시 정지 상태인지 확인
    public bool IsPaused()
    {
        return _gameState.Value == InGameState.Paused;
    }
    
    // 게임이 종료되었는지 확인
    public bool IsGameOver()
    {
        return _gameState.Value == InGameState.GameOver;
    }
    
    // 게임이 준비 상태인지 확인
    public bool IsReady()
    {
        return _gameState.Value == InGameState.Ready;
    }
    
    // 특정 상태로 변경될 때 스트림
    public IObservable<Unit> OnGameStateChangedTo(InGameState state)
    {
        return _gameState
            .Where(s => s == state)
            .AsUnitObservable();
    }
    
    // 게임 시작 스트림
    public IObservable<Unit> OnGameStarted => OnGameStateChangedTo(InGameState.Playing);
    
    // 게임 일시 정지 스트림
    public IObservable<Unit> OnGamePaused => OnGameStateChangedTo(InGameState.Paused);
    
    // 게임 종료 스트림
    public IObservable<Unit> OnGameOver => OnGameStateChangedTo(InGameState.GameOver);
    
    // 게임 준비 스트림
    public IObservable<Unit> OnGameReady => OnGameStateChangedTo(InGameState.Ready);
    
    // 남은 시간 스트림
    public IObservable<float> OnRemainingTimeChanged => _gameTime
        .Select(_ => GetRemainingTime())
        .DistinctUntilChanged();
    
    // 게임 진행률 스트림
    public IObservable<float> OnGameProgressChanged => _gameTime
        .Select(_ => GetGameProgress())
        .DistinctUntilChanged();
    
    // 서비스 종료 시 정리
    private void OnDestroy()
    {
        _timeUpdateDisposable?.Dispose();
    }
}
