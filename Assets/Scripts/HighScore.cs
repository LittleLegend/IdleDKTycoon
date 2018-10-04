using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HighScore : MonoBehaviour
{
    [SerializeField] private Text _namesText;
    [SerializeField] private Text _scoresText;
    [SerializeField] private Menu _mainMenuPrefab;

    public bool IsEndOfGame = false;

    private void Start()
    {
        var highScore = StateController.HighScore;
        var nameStringBuilder = new StringBuilder();
        var scoreStringBuilder = new StringBuilder();
        for (var i = 0; i < highScore.Length; i++)
        {
            var scoreEntry = highScore[i];
            nameStringBuilder.Append($"{scoreEntry.Name}:");
            scoreStringBuilder.Append(scoreEntry.Score);
            if (i <= highScore.Length - 1)
            {
                nameStringBuilder.Append("\n");
                scoreStringBuilder.Append("\n");
            }
        }
        _namesText.text = nameStringBuilder.ToString();
        _scoresText.text = scoreStringBuilder.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Back();

        if (StateController.GameIsOver && Input.GetKey(KeyCode.Space))
        {
            StateController.CurrentScore = 0;
            SceneManager.LoadScene(0);
        }
    }

    private void Back()
    {
        if (IsEndOfGame)
        {
            StateController.GameIsOver = true;
        }
        else
        {
            Instantiate(_mainMenuPrefab, transform.parent);
            Destroy(gameObject);
        }
    }
}