using System.Collections;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class MoveBlock : MonoBehaviour, IHittable
{
    [Inject] private readonly PlayerController _playerController = default;
    
    private Rigidbody2D _rigidbody;

    private bool _isMove;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isMove = false;
    }

    private void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(_ =>
            {
                _isMove = false;
                CorrectPosition();
            });
    }

    public void Hit(Vector3 moveDirection)
    {
        StartCoroutine(Move(moveDirection));
    }

    private IEnumerator Move(Vector3 moveDirection)
    {
        _isMove = true;

        while (_isMove)
        {
            _rigidbody.velocity = 300f * Time.deltaTime * moveDirection;

            yield return null;
        }
    }

    private void CorrectPosition()
    {
        var x = (int) transform.position.x;
        var y = (int) transform.position.y;
        var nextPosition = new Vector2(x, y);

        transform
            .DOMove(nextPosition, 0.3f)
            .OnComplete(() =>
            {
                _rigidbody.velocity = Vector3.zero;

                // Button ON
                _playerController.ActivatePlayerButton();
            });
    }
}