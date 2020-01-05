using UnityEngine;
using Zenject;

public class TransitionSpriteMask : MonoBehaviour
{
    private SpriteMask _spriteMask;

    [Inject]
    private void Construct(SpriteMask spriteMask)
    {
        _spriteMask = spriteMask;
    }

    public void SetAlphaCutOff(float alphaCutOffValue)
    {
        _spriteMask.alphaCutoff = Mathf.Clamp01(alphaCutOffValue);
    }
}