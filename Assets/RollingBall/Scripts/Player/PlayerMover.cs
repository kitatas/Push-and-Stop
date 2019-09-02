﻿using System.Collections;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    [Inject] private readonly PlayerController _playerController = default;

    private Rigidbody2D _rigidbody;

    private bool _isMove;
    private bool _isHit;
    [SerializeField] private float moveSpeed = 300f;
    public Button moveButton = null;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
                CorrectPosition();
                
                if (hittable != null)
                {
                    hittable.Hit(transform.up);
                    _isHit = true;
                }
            });
    }

    private IEnumerator Move()
    {
        // Button Off
        _playerController.DeactivatePlayerButton();
        _isMove = true;

        while (_isMove)
        {
            _rigidbody.velocity = moveSpeed * Time.deltaTime * transform.up;

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

                //if  goal position => game clear

                // else  one more
                // Button ON
                if (_isHit == false)
                {
                    _playerController.ActivatePlayerButton();
                }
            });
    }
}