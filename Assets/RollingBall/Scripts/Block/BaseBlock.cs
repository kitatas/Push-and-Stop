using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBlock : MonoBehaviour, IHittable
{
    private SeManager _seManager;
    private PlayerController _playerController;

    [Inject]
    private void Construct(SeManager seManager, PlayerController playerController)
    {
        _seManager = seManager;
        _playerController = playerController;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public virtual void Hit(Vector3 moveDirection)
    {
        _seManager.PlaySe(SeType.Hit);
    }

    protected void ActivatePlayerButton()
    {
        _playerController.ActivateButton(true);
    }
}