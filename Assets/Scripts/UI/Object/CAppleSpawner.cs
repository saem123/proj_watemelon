using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CAppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    public float appleSize = 100f;  // 사과 크기
    public float padding = 20f;     // 간격을 좀 더 줄임
    private RectTransform canvasRect;

    // 숫자별 가중치 (높을수록 더 자주 나옴)
    private Dictionary<int, int> numberWeights = new Dictionary<int, int>()
    {
        {1, 5},  // 1은 적게
        {2, 10}, // 2는 많이
        {3, 10}, // 3은 많이
        {4, 10}, // 4는 많이
        {5, 10}, // 5는 많이
        {6, 10}, // 6은 많이
        {7, 10}, // 7은 많이
        {8, 5},  // 8은 적게
        {9, 5}   // 9는 적게
    };

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        SpawnApples();
    }

    // 가중치 기반 랜덤 숫자 생성
    private int GetWeightedRandomNumber()
    {
        int totalWeight = 0;
        foreach (var weight in numberWeights.Values)
        {
            totalWeight += weight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (var pair in numberWeights)
        {
            currentWeight += pair.Value;
            if (randomValue < currentWeight)
            {
                return pair.Key;
            }
        }

        return 5; // 기본값
    }

    void SpawnApples()
    {
        float width = canvasRect.rect.width;
        float height = canvasRect.rect.height;

        // 가로, 세로에 들어갈 수 있는 사과 개수 계산
        int cols = Mathf.FloorToInt(width / (appleSize + padding));
        int rows = Mathf.FloorToInt(height / (appleSize + padding));

        // 전체 영역의 중앙에서 시작하도록 위치 계산
        float totalWidth = cols * (appleSize + padding) - padding;
        float totalHeight = rows * (appleSize + padding) - padding;
        float startX = -totalWidth / 2 + appleSize / 2;
        float startY = totalHeight / 2 - appleSize / 2;

        Debug.Log($"Width: {width}, Height: {height}, Cols: {cols}, Rows: {rows}");

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector2 position = new Vector2(
                    startX + j * (appleSize + padding),
                    startY - i * (appleSize + padding)
                );

                GameObject apple = Instantiate(applePrefab, transform);
                RectTransform appleRect = apple.GetComponent<RectTransform>();
                appleRect.anchoredPosition = position;
                appleRect.sizeDelta = new Vector2(appleSize, appleSize);

                // 가중치 기반 랜덤 숫자 생성
                int randomNumber = GetWeightedRandomNumber();
                apple.GetComponent<CApple>().SetNumber(randomNumber);
            }
        }
    }
}