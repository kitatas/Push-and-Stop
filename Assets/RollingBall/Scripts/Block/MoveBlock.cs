using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class MoveBlock : MonoBehaviour, IHittable
{
    [Inject] private readonly PlayerController _playerController = default;
    [Inject] private readonly Rigidbody2D _rigidbody = default;

    private bool _isMove;

    private void Start()
    {
        _isMove = false;

        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(_ =>
            {
                _isMove = false;
                _rigidbody.velocity = Vector3.zero;
                CorrectPosition();
            });
    }

    public async void Hit(Vector3 moveDirection)
    {
        _isMove = true;

        while (_isMove)
        {
            _rigidbody.velocity = 300f * Time.deltaTime * moveDirection;

            await Task.Yield();
        }
    }

    private void CorrectPosition()
    {
        var x = Mathf.RoundToInt(transform.position.x);
        var y = Mathf.RoundToInt(transform.position.y);
        var nextPosition = new Vector2(x, y);

        transform
            .DOMove(nextPosition, 0.3f)
            .OnComplete(() =>
            {
                // Button ON
                _playerController.ActivatePlayerButton();
            });
    }
}