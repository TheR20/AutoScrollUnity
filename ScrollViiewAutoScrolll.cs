using UnityEngine;
using UnityEngine.UI;

public class ScrollViiewAutoScrolll : MonoBehaviour
{
    [SerializeField] private RectTransform _viewportRectTransform;
    [SerializeField] private RectTransform _content;
    [SerializeField] private float _transitionDuration = 0.2f;

    private transitionHelper _transitionHelper = new transitionHelper();

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
        float viewportTop = _viewportRectTransform.rect.yMax;
        float viewportBottom = _viewportRectTransform.rect.yMin;

        // Obtener la posición en Y del borde superior e inferior del elemento
        float elementTop = GetBorderTopYRelative(gObj);
        float elementBottom = GetBorderBottomYRelative(gObj);

        // Desplazamiento necesario para mostrar el elemento completamente en el viewport
        float topDiff = elementTop - viewportTop;
        float bottomDiff = elementBottom - viewportBottom;

        if (topDiff > 0f) // Si el borde superior está fuera de la vista, desplazamos hacia abajo
        {
            MoveContentObjectYByAmount(topDiff + GetVerticalLayoutGroup().padding.top);
        }
        else if (bottomDiff < 0f) // Si el borde inferior está fuera de la vista, desplazamos hacia arriba
        {
            MoveContentObjectYByAmount(bottomDiff - GetVerticalLayoutGroup().padding.bottom);
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

    private void MoveContentObjectYByAmount(float amount)
    {
        Vector2 currentScrollPos = _content.transform.localPosition;
        Vector2 targetScrollPos = new Vector2(currentScrollPos.x, currentScrollPos.y - amount);

        // Aplicar transición con el helper
        _transitionHelper.TransitionPositionFromTo(currentScrollPos, targetScrollPos, _transitionDuration);
    }

    private VerticalLayoutGroup GetVerticalLayoutGroup()
    {
        return _content.GetComponent<VerticalLayoutGroup>();
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
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _duration) TransitionComplete();
        }

        private void TransitionComplete()
        {
            _inProgress = false;
        }
    }
}


