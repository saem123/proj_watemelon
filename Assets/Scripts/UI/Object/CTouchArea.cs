using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Saem;

public class CTouchArea : MonoBehaviour
{
    private Vector2 touchStart;
    private Vector2 touchEnd;
    private bool isDrawing = false;
    private List<CApple> selectedApples = new List<CApple>();
    private RectTransform canvasRect;
    private Image selectionBox;
    private Image selectionBorder; // 테두리용 이미지
    private Canvas canvas;
    private GameService gameService;

    private Color normalColor = new Color(1, 0, 0, 0.2f); // 빨간색 (기본)
    private Color validColor = new Color(1, 1, 0, 0.2f);   // 노란색 (합이 10일 때)
    private Color borderColor = new Color(1, 1, 1, 0.8f);  // 테두리 색상 (흰색)

    void Start()
    {
        gameService = GameService.instance;
        
        // EventSystem이 없으면 생성
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        canvasRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        
        // 테두리용 UI Image 생성
        GameObject borderObj = new GameObject("SelectionBorder");
        borderObj.transform.SetParent(transform, false);
        selectionBorder = borderObj.AddComponent<Image>();
        selectionBorder.color = borderColor;
        selectionBorder.enabled = false;
        
        // 선택 영역을 그릴 UI Image 생성
        GameObject selectionBoxObj = new GameObject("SelectionBox");
        selectionBoxObj.transform.SetParent(transform, false);
        selectionBox = selectionBoxObj.AddComponent<Image>();
        selectionBox.color = normalColor;
        selectionBox.enabled = false;
    }

    void Update()
    {
        // 게임이 진행 중이 아니면 터치 무시
        if (!gameService.IsPlaying()) return;

        // 터치 입력 처리
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, touch.position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera, out touchPos);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touchPos;
                    isDrawing = true;
                    selectedApples.Clear();
                    UpdateSelection();
                    break;

                case TouchPhase.Moved:
                    if (isDrawing)
                    {
                        touchEnd = touchPos;
                        DrawSelectionBox();
                        UpdateSelection();
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDrawing)
                    {
                        isDrawing = false;
                        CheckSelectedApples();
                        ClearSelection();
                    }
                    break;
            }
        }
        // 마우스 입력 처리
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera, out mousePos);

            if (Input.GetMouseButtonDown(0))
            {
                touchStart = mousePos;
                isDrawing = true;
                selectedApples.Clear();
                UpdateSelection();
            }
            else if (isDrawing)
            {
                touchEnd = mousePos;
                DrawSelectionBox();
                UpdateSelection();
            }
        }
        else if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            isDrawing = false;
            CheckSelectedApples();
            ClearSelection();
        }
    }

    void UpdateSelection()
    {
        CApple[] allApples = GetComponentsInChildren<CApple>();
        selectedApples.Clear(); // 선택 목록을 초기화

        foreach (CApple apple in allApples)
        {
            Vector2 applePos = apple.GetComponent<RectTransform>().anchoredPosition;
            bool isInBox = IsInSelectionBox(applePos);
            apple.SetSelected(isInBox);
            
            if (isInBox)
            {
                selectedApples.Add(apple);
            }
        }

        // 선택된 사과들의 합이 10인지 확인하고 색상 변경
        int sum = 0;
        foreach (CApple apple in selectedApples)
        {
            sum += apple.number;
        }

        // 합이 10이면 노란색, 아니면 빨간색
        selectionBox.color = (sum == 10) ? validColor : normalColor;
    }

    void ClearSelection()
    {
        CApple[] allApples = GetComponentsInChildren<CApple>();
        foreach (CApple apple in allApples)
        {
            apple.SetSelected(false);
        }
        selectedApples.Clear();
    }

    void DrawSelectionBox()
    {
        float width = Mathf.Abs(touchEnd.x - touchStart.x);
        float height = Mathf.Abs(touchEnd.y - touchStart.y);
        float x = Mathf.Min(touchStart.x, touchEnd.x);
        float y = Mathf.Min(touchStart.y, touchEnd.y);

        // 선택 영역 설정
        selectionBox.rectTransform.sizeDelta = new Vector2(width, height);
        selectionBox.rectTransform.anchoredPosition = new Vector2(x + width/2, y + height/2);
        selectionBox.enabled = true;

        // 테두리 설정 (선택 영역보다 약간 크게)
        float borderWidth = 5f; // 테두리 두께
        selectionBorder.rectTransform.sizeDelta = new Vector2(width + borderWidth * 2, height + borderWidth * 2);
        selectionBorder.rectTransform.anchoredPosition = new Vector2(x + width/2, y + height/2);
        selectionBorder.enabled = true;
    }

    void CheckSelectedApples()
    {
        int sum = 0;
        foreach (CApple apple in selectedApples)
        {
            sum += apple.number;
        }

        if (sum == 10)
        {
            foreach (CApple apple in selectedApples)
            {
                apple.DestroyApple();
            }
            gameService.AddScore(5);
        }

        selectionBox.enabled = false;
        selectionBorder.enabled = false;
    }

    bool IsInSelectionBox(Vector2 position)
    {
        float minX = Mathf.Min(touchStart.x, touchEnd.x);
        float maxX = Mathf.Max(touchStart.x, touchEnd.x);
        float minY = Mathf.Min(touchStart.y, touchEnd.y);
        float maxY = Mathf.Max(touchStart.y, touchEnd.y);

        return position.x >= minX && position.x <= maxX &&
               position.y >= minY && position.y <= maxY;
    }
} 