using UnityEngine;
using UnityEngine.UI;

public class ScrollViewAutoScrollGridLayout : MonoBehaviour
{
[SerializeField] private RectTransform _viewportRectTransform;
    [SerializeField] private RectTransform _content;
    [SerializeField] private float _transitionDuration = 0.2f;

    private transitionHelper _transitionHelper = new transitionHelper();

    private GridLayoutGroup _gridLayoutGroup;

    void Awake()
    {
        _gridLayoutGroup = _content.GetComponent<GridLayoutGroup>();
    }

    void Update()
    {
        if (_transitionHelper.InProgress)
        {
            _transitionHelper.Update();
            _content.transform.localPosition = _transitionHelper.PosCurrent;
        }
    }

    public void HandleOnselectChange(GameObject gObj)
    {
        // Calcular el área del viewport
        float viewportTop = _viewportRectTransform.rect.yMax;
        float viewportBottom = _viewportRectTransform.rect.yMin;
        float viewportLeft = _viewportRectTransform.rect.xMin;
        float viewportRight = _viewportRectTransform.rect.xMax;

        // Obtener la posición en X e Y del borde del elemento
        float elementTop = GetBorderTopYRelative(gObj);
        float elementBottom = GetBorderBottomYRelative(gObj);
        float elementLeft = GetBorderLeftXRelative(gObj);
        float elementRight = GetBorderRightXRelative(gObj);

        // Desplazamiento necesario para mostrar el elemento completamente en el viewport
        float topDiff = elementTop - viewportTop;
        float bottomDiff = elementBottom - viewportBottom;
        float leftDiff = elementLeft - viewportLeft;
        float rightDiff = elementRight - viewportRight;

        // Realizar el movimiento necesario para ajustar la visibilidad del elemento en la cuadrícula
        if (topDiff > 0f) // Desplazamiento vertical hacia abajo
        {
            MoveContentObjectByAmount(0f, topDiff + _gridLayoutGroup.padding.top);
        }
        else if (bottomDiff < 0f) // Desplazamiento vertical hacia arriba
        {
            MoveContentObjectByAmount(0f, bottomDiff - _gridLayoutGroup.padding.bottom);
        }

        if (leftDiff < 0f) // Desplazamiento horizontal hacia la izquierda
        {
            MoveContentObjectByAmount(leftDiff - _gridLayoutGroup.padding.left, 0f);
        }
        else if (rightDiff > 0f) // Desplazamiento horizontal hacia la derecha
        {
            MoveContentObjectByAmount(rightDiff + _gridLayoutGroup.padding.right, 0f);
        }
    }

    private float GetBorderTopYRelative(GameObject gObj)
    {
        RectTransform rectTransform = gObj.GetComponent<RectTransform>();
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);
        Vector3 viewportPoint = _viewportRectTransform.InverseTransformPoint(worldCorners[1]);
        return viewportPoint.y;
    }

    private float GetBorderBottomYRelative(GameObject gObj)
    {
        RectTransform rectTransform = gObj.GetComponent<RectTransform>();
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);
        Vector3 viewportPoint = _viewportRectTransform.InverseTransformPoint(worldCorners[0]);
        return viewportPoint.y;
    }

    private float GetBorderLeftXRelative(GameObject gObj)
    {
        RectTransform rectTransform = gObj.GetComponent<RectTransform>();
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);
        Vector3 viewportPoint = _viewportRectTransform.InverseTransformPoint(worldCorners[0]);
        return viewportPoint.x;
    }

    private float GetBorderRightXRelative(GameObject gObj)
    {
        RectTransform rectTransform = gObj.GetComponent<RectTransform>();
        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);
        Vector3 viewportPoint = _viewportRectTransform.InverseTransformPoint(worldCorners[3]);
        return viewportPoint.x;
    }

    private void MoveContentObjectByAmount(float xAmount, float yAmount)
    {
        Vector2 currentScrollPos = _content.transform.localPosition;
        Vector2 targetScrollPos = new Vector2(currentScrollPos.x - xAmount, currentScrollPos.y - yAmount);

        // Aplicar transición con el helper
        _transitionHelper.TransitionPositionFromTo(currentScrollPos, targetScrollPos, _transitionDuration);
    }

    private class transitionHelper
    {
        private float _duration = 0f;
        private float _timeElapsed = 0f;
        private bool _inProgress = false;
        private Vector2 _posCurrent;
        private Vector2 _posFrom;
        private Vector2 _posTo;

        public bool InProgress => _inProgress;
        public Vector2 PosCurrent => _posCurrent;

        public void Update()
        {
            Tick();
            CalculatePosition();
        }

        public void Clear()
        {
            _duration = 0f;
            _timeElapsed = 0f;
            _inProgress = false;
        }

        public void TransitionPositionFromTo(Vector2 posFrom, Vector2 posTo, float duration)
        {
            Clear();
            _posFrom = posFrom;
            _posTo = posTo;
            _duration = duration;
            _inProgress = true;
        }

        private void CalculatePosition()
        {
            _posCurrent = Vector2.Lerp(_posFrom, _posTo, _timeElapsed / _duration);
        }

        private void Tick()
        {
            if (!_inProgress) return;
            _timeElapsed += Time.fixedDeltaTime;
            if (_timeElapsed >= _duration) TransitionComplete();
        }

        private void TransitionComplete()
        {
            _inProgress = false;
        }
    }
}
