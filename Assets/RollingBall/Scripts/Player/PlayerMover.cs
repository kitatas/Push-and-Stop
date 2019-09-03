using System.Collections;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    [Inject] private readonly PlayerController _playerController = default;
    [Inject] private readonly Rigidbody2D _rigidbody = default;

    private bool _isMove;
    [SerializeField] private float moveSpeed = 300f;
    public Button moveButton = null;

    private void Start()
    {
        _isMove = false;

        // ボタンによる移動
        moveButton
            .OnClickAsObservable()
            .Subscribe(_ => StartCoroutine(Move()));

        //　stageのオブジェクトに当たったら...
        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Subscribe(hittable =>
            {
                _isMove = false;

                hittable?.Hit(transform.up);

                CorrectPosition();
            });
    }

    private async void Move()
    {
        // Button Off
        _playerController.DeactivatePlayerButton();
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

        transform
            .DOMove(nextPosition, 0.3f)
            .OnComplete(() =>
            {
                _rigidbody.velocity = Vector3.zero;
                //if  goal position => game clear
            });
    }
}