using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SoundManager;

public class StageManager : MonoBehaviour
{

    public bool IsPlaying { get => _isPlaying; set => _isPlaying = value; }
    public bool PlaySound { get => _playSound; set => _playSound = value; }

    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private AudioSource _seAudioSource;

    [SerializeField] private GameObject _firstColleagueUI;
    [SerializeField] private GameObject _secondColleagueUI;
    [SerializeField] private GameObject _chiefUI;
    [SerializeField] private GameObject _presidentUI;

    [SerializeField] private GameObject _gameSceneUI;
    [SerializeField] private GameObject _clearUI;
    [SerializeField] private GameObject _failedUI;
    [SerializeField] private GameObject _retryButtonUI;

    [SerializeField] private Button _retryButton;

    [SerializeField] private TextMeshProUGUI _readyStartTimeText;
    [SerializeField] private TextMeshProUGUI _nowTimeText;
    [SerializeField] private TextMeshProUGUI _purposeText;

    private float _readyTime; //初めの待つ時間
    private float _elapsedTime; //経過時間
    private int _hour = 17; //初期時間
    private int _minute = 0; //初期分

    private bool _isPlaying;
    private bool _playSound;

    [SerializeField] private CinemachineVirtualCamera _firstColleagueCinemachine;
    [SerializeField] private CinemachineVirtualCamera _secondColleagueCinemachine;
    [SerializeField] private CinemachineVirtualCamera _chiefCinemachine;
    [SerializeField] private CinemachineVirtualCamera _presidentCinemachine;

    // Start is called before the first frame update
    void Start()
    {
        _firstColleagueUI.SetActive(false);
        _secondColleagueUI.SetActive(false);
        _chiefUI.SetActive(false);
        _presidentUI.SetActive(false);

        _gameSceneUI.SetActive(true);
        _clearUI.SetActive(false);
        _failedUI.SetActive(false);
        _retryButtonUI.SetActive(false);
        _retryButton.onClick.AddListener(RoadScene);

        _readyTime = 2.0f;
        _elapsedTime = 360;

        _isPlaying = false;

        _firstColleagueCinemachine.Priority = 1;
        _secondColleagueCinemachine.Priority = 1;
        _chiefCinemachine.Priority = 1;
        _presidentCinemachine.Priority = 1;

        _readyStartTimeText.text = "定時前に帰ろう";
        _purposeText.text = "プリンターで書類のコピーを取ろう";
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPlaying)
        {
            _readyTime -= Time.deltaTime;
            if(_readyTime <= 0)
            {
                _readyStartTimeText.text = "";
                _isPlaying = true;
            }
        }
        else
        {
            
            _elapsedTime += Time.deltaTime;
            
            if(_elapsedTime >= 1.0f)
            {
                _elapsedTime = 0;
                _minute++;

                if(_minute >= 60)
                {
                    _minute = 0;
                    _hour++;
                }

                if(_hour >= 19)
                {
                    GameOver("PastTheScheduledTime");
                }
            }

            _nowTimeText.text = $"{_hour:D2}:{_minute:D2}";
        }
    }
    public IEnumerator SubmitDocument()
    {
        Debug.Log("");
        _presidentCinemachine.Priority = 20;
        _presidentUI.SetActive(true);
        _isPlaying = false;
        PlaySE("書類を提出");

        yield return new WaitForSeconds(4);

        _presidentUI.SetActive(false);
        _presidentCinemachine.Priority = 1;
        _isPlaying = true;
        _purposeText.text = "自分の机に戻って荷物をまとめよう";

        yield break;
    }
    public void HaveLuggage()
    {
        _purposeText.text = "退社しよう";
        PlaySE("荷物をまとめる");
    }
    public AudioClip GetSE(string musicName)
    {
        foreach (SoundList soundList in _soundManager.soundLists)
        {
            if (soundList.soundName == musicName)
            {
                return soundList.audioClip;
            }
        }
        return null;
    }

    public void PlaySE(string seName)
    {
        AudioClip se = GetSE(seName);
        if ((se != null && _seAudioSource != null))
        {
            _seAudioSource.PlayOneShot(se);
        }
    }

    public void GameClear()
    {
        if (!_playSound)
        {
            PlaySE("ゲームクリア");
            _playSound = true;
        }
        _clearUI.SetActive(true);
        _retryButtonUI.SetActive(true);
        _isPlaying = false;
    }
    public void GameOver(string cause)
    {
        _retryButtonUI.SetActive(true);
        _isPlaying = false;
        if (!_playSound)
        {
            PlaySE("ゲームオーバー");
            _playSound = true;
        }
        switch (cause)
        {
            case "FirstColleague":
                _firstColleagueUI.SetActive(true);
                _failedUI.SetActive(true);
                _gameSceneUI.SetActive(false);
                _firstColleagueCinemachine.Priority = 20;
                break;
            case "SecondColleague":
                _secondColleagueUI.SetActive(true);
                _failedUI.SetActive(true);
                _gameSceneUI.SetActive(false);
                _secondColleagueCinemachine.Priority = 20;
                break;
            case "Chief":
                _chiefUI.SetActive(true);
                _failedUI.SetActive(true);
                _gameSceneUI.SetActive(false);
                _chiefCinemachine.Priority = 20;
                break;
            case "PastTheScheduledTime": //定時過ぎた時
                _failedUI.SetActive(true);
                break;
        }
    }
    private void RoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
