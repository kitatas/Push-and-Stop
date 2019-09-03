using UnityEngine;
using Zenject;

public class Block : MonoBehaviour, IHittable
{
    [Inject] private readonly PlayerController _playerController = default;
    
    public void Hit(Vector3 moveDirection)
    {
        _playerController.ActivatePlayerButton();
    }
}