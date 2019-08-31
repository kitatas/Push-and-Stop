using System.Collections;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private bool _canInput;
    private bool _canRotate;
    private bool _isMove;

    private Vector3 _rotateVector;
    private Vector3 _addRotateVector;

    [SerializeField] private float moveSpeed = 300f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _canInput = true;
        _canRotate = true;
        _isMove = false;

        _rotateVector = Vector3.zero;
        _addRotateVector = new Vector3(0f, 0f, 90f);
    }

    private void Start()
    {
        // 入力（移動）
        this.UpdateAsObservable()
            .Where(_ => _canInput)
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ => StartCoroutine(Move()));

        // 入力（回転）
        this.UpdateAsObservable()
            .Where(_ => _canRotate)
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .Subscribe(_ => Rotate());

        // stage内のオブジェクトにぶつかる
        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(hittable =>
            {
                _isMove = false;
                hittable.Hit();
                CorrectPosition();
            });
    }

    private IEnumerator Move()
    {
        _canInput = false;
        _canRotate = false;
        _isMove = true;

        while (_isMove)
        {
            _rigidbody.velocity = moveSpeed * Time.deltaTime * transform.up;

            yield return null;
        }
    }

    private void Rotate()
    {
        _canRotate = false;

        _rotateVector += _addRotateVector;

        transform
            .DORotate(_rotateVector, 0.3f)
            .OnComplete(() => _canRotate = true);
    }

    private void CorrectPosition()
    {
        var x = (int) transform.position.x;
        var y = (int) transform.position.y;
        // Debug.Log($"x : {x}, y : {y}");
        var nextPosition = new Vector2(x, y);

        transform
            .DOMove(nextPosition, 0.3f)
            .OnComplete(() =>
            {
                // nextPosition is goal position -> game clear

                _canInput = true;
                _canRotate = true;
            });
    }
}