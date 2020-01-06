using DG.Tweening;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public sealed class PlayerMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private bool _isMove;
    private Vector3 _direction;
    [SerializeField] private float moveSpeed = 200f;

    private readonly ReactiveProperty<Vector2> _onComplete = new ReactiveProperty<Vector2>(Vector2.one * -1f);
    public IReadOnlyReactiveProperty<Vector2> OnComplete() => _onComplete;

    [Inject]
    private void Construct(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
    }

    private void Start()
    {
        _isMove = false;
        _direction = Vector3.zero;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //　stageのオブジェクトに当たったら...
        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(hittable =>
            {
                _isMove = false;
                hittable.Hit(_direction);
                CorrectPosition();
            });
    }

    public async UniTaskVoid Move(Vector3 direction)
    {
        _isMove = true;
        _direction = direction;

        while (_isMove)
        {
            _rigidbody.velocity = moveSpeed * Time.deltaTime * _direction;

            await UniTask.Yield();
        }
    }

    private void CorrectPosition()
    {
        var nextPosition = transform.RoundPosition();

        _onComplete.Value = nextPosition;

        transform
            .DOMove(nextPosition, ConstantList.correctTime)
            .OnComplete(() => _rigidbody.velocity = Vector2.zero);
    }
}