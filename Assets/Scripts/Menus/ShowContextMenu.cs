using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowContextMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _contextMenu;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Vector2 _offset;

    public event Action<DraggableItem> DraggableObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouseScreenPosition = Input.mousePosition;

            Vector2 localMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                mouseScreenPosition,
                _canvas.worldCamera,
                out localMousePos
            );

            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = mouseScreenPosition
            };

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            //Debug.Log(raycastResults.Count);

            if (raycastResults.Count == 3)
            {
                _contextMenu.gameObject.SetActive(false);
            }

            if (raycastResults.Count > 0)
            {
                GameObject clickedObj = raycastResults[0].gameObject;
                DraggableItem draggableComponent = clickedObj.GetComponent<DraggableItem>();

                if (draggableComponent != null)
                {
                    DraggableObject?.Invoke(draggableComponent);
                    _contextMenu.gameObject.SetActive(true);

                    _contextMenu.anchoredPosition = localMousePos + _offset;
                }
            }
        }
    }
}
