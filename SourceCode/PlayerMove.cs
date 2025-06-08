using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] StageManager _stageManager;

    [SerializeField] private GameObject _rotateBall;　//目印のボール
    [SerializeField] private GameObject _centerPlayer;　//中心のプレイヤー
   
    [SerializeField] private GameObject _document1;　//コピー機に触れたら出現させる
    [SerializeField] private GameObject _document2;
    [SerializeField] private GameObject _playerLuggage;

    private bool _moveOk;
    private bool _isPrintFinished;
    private bool _isCheckFinished;
    private bool _haveluggage;

    private Rigidbody _playerRb;

    private float _moveTimer;　//移動した時間

    private Vector3 _savedDirection;

    // Start is called before the first frame update
    void Start()
    {
        _moveOk = true;
        _isPrintFinished = false;
        _isCheckFinished = false;
        _haveluggage = false;

        _document1.SetActive(false);
        _document2.SetActive(false);
        _playerLuggage.SetActive(false);

        _playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_stageManager.IsPlaying)
        {
            _rotateBall.transform.RotateAround(_centerPlayer.transform.position, _rotateBall.transform.up, _playerData.RotateSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_moveOk)
                {
                    SetDirection();
                }
            }
            if (!_moveOk)
            {
                Move();
            }
        }
    }

    /// <summary>
    /// 移動する方向を決める
    /// </summary>
    private void SetDirection()
    {
        _savedDirection = (_rotateBall.transform.position - _playerRb.position).normalized;
        _moveOk = false;
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        _moveTimer += Time.deltaTime;
        _playerRb.AddForce(_savedDirection * _playerData.MovePower);

        if(_moveTimer > _playerData.CanMoveTimer)
        {
            _moveTimer = 0;
            _playerRb.velocity = Vector3.zero;
            _moveOk = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isPrintFinished)
        {
            if (collision.gameObject.CompareTag("FirstPrinter"))
            {
                _document1.SetActive(true);
                _stageManager.PlaySE("コピーする");
            }
            if (collision.gameObject.CompareTag("SecondPrinter"))
            {
                _document2.SetActive(true);
                _stageManager.PlaySE("コピーする");
                _isPrintFinished = true;
            }
        }
        if (collision.gameObject.CompareTag("President"))
        {
            if (_isPrintFinished)
            {
                _isCheckFinished = true;
                _document1.SetActive(false);
                _document2.SetActive(false);
                StartCoroutine(_stageManager.SubmitDocument());
            }
        }
        if (collision.gameObject.CompareTag("MyDesk"))
        {
            if (_isCheckFinished)
            {
                _haveluggage = true;
                _playerLuggage.SetActive(true);
                _stageManager.HaveLuggage();
            }
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (_haveluggage)
            {
                _stageManager.GameClear();
            }
        }
        if (collision.gameObject.CompareTag("FirstColleague"))
        {
            _stageManager.GameOver("FirstColleague");
        }
        if (collision.gameObject.CompareTag("SecondColleague"))
        {
            _stageManager.GameOver("SecondColleague");
        }
        if (collision.gameObject.CompareTag("Chief"))
        {
            _stageManager.GameOver("Chief");
        }
    }
}
