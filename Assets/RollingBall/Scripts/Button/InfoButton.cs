using DG.Tweening;
using UnityEngine;
using Zenject;

public class InfoButton : BaseButton
{
    [Inject] private readonly RectTransform _infoObj = default;
    [SerializeField] private bool isActive = default;
    private float _originHeight;

    protected override void Awake()
    {
        base.Awake();

        _originHeight = _infoObj.localPosition.y;
    }

    protected override void OnPush()
    {
        base.OnPush();

        var h = isActive ? 0 : _originHeight;

        _infoObj
            .DOLocalMoveY(h, ConstantList.uiAnimationTime / 2f);
    }
}