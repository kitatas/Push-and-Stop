using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BaseButton : MonoBehaviour
{
    private Button _button;

    protected Button button
    {
        get
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }

            return _button;
        }
    }

    private SeManager _seManager;

    [Inject]
    private void Construct(SeManager seManager)
    {
        _seManager = seManager;
    }

    protected virtual void Awake()
    {
        button
            .OnClickAsObservable()
            .Subscribe(_ => OnPush())
            .AddTo(this);
    }

    protected virtual void OnPush()
    {
        _seManager.PlaySe(SeType.Button);
    }

    public void ActivateButton(bool value)
    {
        button.enabled = value;
    }
}