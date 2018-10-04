using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text[] _options;
    [SerializeField] private VolumeMenu _volumePrefab;
    [SerializeField] private HighScore _highScorePrefab;
    [SerializeField] private Text _focusArrows;

    private int _focused;

    private void Start()
    {
        UpdateFocusArrows();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            GoUpOnce();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            GoDownOnce();
        if (Input.GetKeyDown(KeyCode.Space))
            Confirm();
        if (Input.GetKeyDown(KeyCode.Escape))
            Resume();
    }

    private void GoUpOnce()
    {
        if (_focused <= 0) return;
        _focused--;
        UpdateFocusArrows();
    }

    private void GoDownOnce()
    {
        if (_focused >= _options.Length - 1) return;
        _focused++;
        UpdateFocusArrows();
    }

    private void Confirm()
    {
        switch (_focused)
        {
            case (0): Resume(); break;
            case (1): Volume(); break;
            case (2): HighScore(); break;
            case (3): Application.Quit(); break;
        }
    }

    private void Resume()
    {
        StateController.IsPaused = false;
        Destroy(gameObject);
    }

    private void Volume()
    {
        Instantiate(_volumePrefab, transform.parent);
        Destroy(gameObject);
    }

    private void HighScore()
    {
        Instantiate(_highScorePrefab, transform.parent);
        Destroy(gameObject);
    }

    private void UpdateFocusArrows()
    {
        var position = _focusArrows.transform.position;
        position.y = _options[_focused].gameObject.transform.position.y;
        _focusArrows.transform.position = position;
    }
}