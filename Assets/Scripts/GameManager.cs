using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPaddlePrefab;

    public TextMeshProUGUI livesCount;
    public TextMeshProUGUI scoreCount;
    public TextMeshProUGUI levelCount;
    public GameObject levelCompleted;
    public GameObject levelBox;
    public GameObject gameOverBox;

    public GameObject[] levels;
    public static GameManager Instance { get; private set; }

    public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER}
    State _state;
    GameObject _currentBall;
    GameObject _currentLevel;
    bool _isSwitchingState;

    private int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value;
            scoreCount.text = _score.ToString();
        }
    }

    private int _level;

    public int Level
    {
        get { return _level; }
        set { _level = value;
            levelCount.text = _level.ToString();
        }
    }

    private int _lives;

    public int Lives
    {
        get { return _lives; }
        set { _lives = value;
            livesCount.text = _lives.ToString();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SwitchState(State.INIT);
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitchingState = false;
    }

    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                break;
            case State.INIT:
                Score = 0;
                Level = 0;
                Lives = 3;
                levelCompleted.SetActive(false);
                gameOverBox.SetActive(false);
                Instantiate(playerPaddlePrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                Destroy(_currentBall);
                Destroy(_currentLevel);

                // add display for level completed
                levelBox.SetActive(false);
                levelCompleted.SetActive(true);
                Level++;
                SwitchState(State.LOADLEVEL, 2f);
                break;
            case State.LOADLEVEL:
                if(Level >= levels.Length)
                {
                    levelBox.SetActive(false);
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    levelCompleted.SetActive(false);
                    _currentLevel = Instantiate(levels[Level]);
                    levelBox.SetActive(true);
                    SwitchState(State.PLAY);
                }
                break;
            case State.GAMEOVER:
                levelBox.SetActive(false);
                gameOverBox.SetActive(true);
                Invoke("ReturnToMenu", 2.0f);
                break;
        }
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(_currentBall == null)
                {
                    if (Lives > 0)
                    {
                        _currentBall = Instantiate(ballPrefab);
                    }
                    else
                    {
                        SwitchState(State.GAMEOVER);
                    }    
                }

                if (_currentLevel != null && _currentLevel.transform.GetChild(0).transform.childCount == 0) //Level/Blocks/Block
                {  
                    if (!_isSwitchingState)
                        SwitchState(State.LEVELCOMPLETED);
                } 
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                break;
        }
    }
}
