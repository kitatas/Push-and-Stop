using UnityEngine;

public sealed class TransitionSpriteMask : MonoBehaviour
{
    private SpriteMask _spriteMask;

    public void Construct(SpriteMask spriteMask)
    {
        _spriteMask = spriteMask;
    }

    public void SetAlphaCutOff(float alphaCutOffValue)
    {
        _spriteMask.alphaCutoff = Mathf.Clamp01(alphaCutOffValue);
    }
}