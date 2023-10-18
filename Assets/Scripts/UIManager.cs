using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _ammoCountText;

    [SerializeField]
    private int _score;

    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _playerLivesList;

    [SerializeField]
    private bool _isRestartActive = false;

    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
        _gameOverText.gameObject.SetActive(false);
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
        _ammoCountText.text = "Ammo Count: " + 15;
    }

    public void DisplayAmmoCount()
    {
        _ammoCountText.text = "Ammo Count: ";
    }

    public void AddScore(int _killPoint)
    {
        _score += _killPoint;
        _scoreText.text = "Score: " + _score.ToString();
        //_scoreText.text = "Score: " + _score; works as well.
    }

    public void UpdateLives(int currentlives)
    {
        _livesImage.sprite = _playerLivesList[currentlives];
        if (currentlives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();

        _restartText.gameObject.SetActive(true);
        StartCoroutine(FlickeringGameOverText());
    }

    IEnumerator FlickeringGameOverText()
    {
        while (_isRestartActive == false)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
