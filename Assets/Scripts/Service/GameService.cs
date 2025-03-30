using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Saem;

public class GameService : Service<GameService>
{
    public ReactiveProperty<int> Score { get; private set; } = new ReactiveProperty<int>(0);
    
    public void AddScore(int amount)
    {
        Score.Value += amount;
    }

    public void ResetScore()
    {
        Score.Value = 0;
    }
}
