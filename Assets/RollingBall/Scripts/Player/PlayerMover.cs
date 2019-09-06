using System.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    [Inject] private readonly Rigidbody2D _rigidbody = default;

    private bool _isMove;
    [SerializeField] private float moveSpeed = 300f;

    private readonly ReactiveProperty<Vector2> _onComplete = new ReactiveProperty<Vector2>(Vector2.one * -1f);
    public IReadOnlyReactiveProperty<Vector2> OnComplete() => _onComplete;

    private void Start()
    {
        _isMove = false;

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

    public async void Move()
    {
        _isMove = true;

        while (_isMove)
        {
            _rigidbody.velocity = moveSpeed * Time.deltaTime * transform.up;

            await Task.Yield();
        }
    }

    private void CorrectPosition()
    {
        var x = Mathf.RoundToInt(transform.position.x);
        var y = Mathf.RoundToInt(transform.position.y);
        var nextPosition = new Vector2(x, y);

        _onComplete.Value = nextPosition;

        transform
            .DOMove(nextPosition, 0.3f)
            .OnComplete(() => _rigidbody.velocity = Vector2.zero);
    }
}