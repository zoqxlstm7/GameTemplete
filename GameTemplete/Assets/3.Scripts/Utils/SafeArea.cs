using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public class SafeArea : MonoBehaviour
    {
        public RectTransform[] _rects;

        public Canvas _canvas;

        private Rect lastSafeArea;

        private void Awake()
        {
            ApplySafeArea();
        }

        private void Update()
        {
            if (lastSafeArea != Screen.safeArea)
            {
                ApplySafeArea();
            }
        }

        [ContextMenu("Apply")]
        public void ApplySafeArea()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= _canvas.pixelRect.width;
            anchorMin.y /= _canvas.pixelRect.height;
            anchorMax.x /= _canvas.pixelRect.width;
            anchorMax.y /= _canvas.pixelRect.height;

            for (int i = 0; i < _rects.Length; ++i)
            {
                _rects[i].anchorMin = anchorMin;
                _rects[i].anchorMax = anchorMax;
            }

            lastSafeArea = safeArea;
        }
    }
}
