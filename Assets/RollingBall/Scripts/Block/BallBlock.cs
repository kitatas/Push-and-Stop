using System.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class BallBlock : BaseBlock
{
    [Inject] private readonly PlayerController _playerController = default;
    private Rigidbody2D _rigidbody;

    private bool _isMove;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isMove = false;

        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(_ =>
            {
                _isMove = false;
                CorrectPosition();
            });
    }

    public override void Hit(Vector3 moveDirection)
    {
        base.Hit(moveDirection);

        _isMove = true;

        Move(moveDirection);
    }

    private async void Move(Vector3 moveDirection)
    {
        while (_isMove)
        {
            _rigidbody.velocity = 200f * Time.deltaTime * moveDirection;

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
                _rigidbody.velocity = Vector2.zero;

                // Button ON
                _playerController.ActivatePlayerButton();
            });
    }
}