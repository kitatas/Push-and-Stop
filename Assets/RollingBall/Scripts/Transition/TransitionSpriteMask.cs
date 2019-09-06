using UnityEngine;
using Zenject;

public class TransitionSpriteMask : MonoBehaviour
{
    [Inject] private readonly SpriteMask _spriteMask = default;

    public void SetAlphaCutOff(float alphaCutOffValue)
    {
        _spriteMask.alphaCutoff = Mathf.Clamp01(alphaCutOffValue);
    }
}