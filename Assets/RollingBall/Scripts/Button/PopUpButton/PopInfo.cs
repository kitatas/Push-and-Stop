using DG.Tweening;
using UnityEngine;

public struct PopInfo
{
    public readonly bool isBlocksRaycasts;
    public readonly float targetAlpha;
    public readonly Vector3 targetScale;
    public readonly Ease ease;

    public PopInfo(bool isBlocksRaycasts, float targetAlpha, Vector3 targetScale, Ease ease)
    {
        this.isBlocksRaycasts = isBlocksRaycasts;
        this.targetAlpha = targetAlpha;
        this.targetScale = targetScale;
        this.ease = ease;
    }
}