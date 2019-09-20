using DG.Tweening;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    [Inject] private readonly Rigidbody2D _rigidbody = default;

    private bool _isMove;
    [SerializeField] private float moveSpeed = 200f;

    private readonly ReactiveProperty<Vector2> _onComplete = new ReactiveProperty<Vector2>(Vector2.one * -1f);
    public IReadOnlyReactiveProperty<Vector2> OnComplete() => _onComplete;

    private void Start()
    {
        _isMove = false;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //　stageのオブジェクトに当たったら...
        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(hittable =>
            {
                _isMove = false;
                hittable.Hit(transform.up);
                CorrectPosition();
            });
    }

    public async UniTaskVoid Move()
    {
        _isMove = true;

        while (_isMove)
        {
            _rigidbody.velocity = moveSpeed * Time.deltaTime * transform.up;

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