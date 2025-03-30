using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using Saem;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private GameService gameService;

    void Start()
    {
        gameService = GameService.instance;
        
        // 스코어 변경을 구독하여 UI 업데이트
        gameService.Score.Subscribe(score =>
        {
            scoreText.text = $"Score: {score}";
        }).AddTo(this);
    }
} 