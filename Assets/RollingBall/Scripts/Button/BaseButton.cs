using UniRx;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
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

    }
}