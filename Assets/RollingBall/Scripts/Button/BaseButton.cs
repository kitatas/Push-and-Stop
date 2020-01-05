using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BaseButton : MonoBehaviour
{
    private SeManager _seManager;
    protected Button button { get; private set; }

    [Inject]
    private void Construct(SeManager seManager)
    {
        _seManager = seManager;
    }

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