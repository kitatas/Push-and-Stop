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

    private ISeController _seController;

    [Inject]
    private void Construct(ISeController seController)
    {
        _seController = seController;
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
        _seController.PlaySe(SeType.Button);
    }

    public void ActivateButton(bool value)
    {
        button.enabled = value;
    }

    public bool IsInteractable() => button.interactable;
}