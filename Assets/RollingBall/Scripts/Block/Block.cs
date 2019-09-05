using UnityEngine;
using Zenject;

public class Block : MonoBehaviour, IHittable
{
    [Inject] private readonly PlayerController _playerController = default;

    private void Start()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Hit(Vector3 moveDirection)
    {
        _playerController.ActivatePlayerButton();
    }
}