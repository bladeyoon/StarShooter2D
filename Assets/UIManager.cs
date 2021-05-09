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
    private int _score;

    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _playerLivesList;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
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
            StartCoroutine(FlickeringText());
        }
    }

    IEnumerator FlickeringText()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
