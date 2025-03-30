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

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        SpawnApples();
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

                int randomNumber = Random.Range(1, 10);
                apple.GetComponent<CApple>().SetNumber(randomNumber);
            }
        }
    }
}