using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BaseButton : MonoBehaviour
{
    protected Button button { get; private set; }

    private SeManager _seManager;

    [Inject]
    private void Construct(SeManager seManager)
    {
        _seManager = seManager;

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