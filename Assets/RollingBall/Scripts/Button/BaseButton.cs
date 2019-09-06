using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BaseButton : MonoBehaviour
{
    [Inject] private readonly SeManager _seManager = default;
    protected Button button { get; private set; }

    protected virtual void Awake()
    {
        button = GetComponent<Button>();

        button
            .OnClickAsObservable()
            .Subscribe(_ => OnPush());
    }

    protected virtual void OnPush()
    {
        _seManager.PlaySe(SeType.Button);
    }
}