using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Text _focusArrows;
    [SerializeField] private HighScore _highScorePrefab;
    [SerializeField] private Text _nameText;
    [SerializeField] private GameObject _pointer;

    private bool _focusingNext;
    private string _name = "AAAA";
    private int _focusedLetterPosition;
    private const float LetterOffset = 32f;
    private const float DefaultOffset = -147.5f;

    private void Update()
    {
        if (_focusingNext && Input.GetKeyDown(KeyCode.Space))
            Next();

        if (Input.GetKeyDown(KeyCode.UpArrow)) UpPressed();
        if (Input.GetKeyDown(KeyCode.DownArrow)) DownPressed();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) LeftPressed();
        if (Input.GetKeyDown(KeyCode.RightArrow)) RightPressed();
    }

    private void UpPressed()
    {
        if (_focusingNext) return;
        CycleName(true);
    }

    private void DownPressed()
    {
        if (_focusingNext) return;
        CycleName(false);
    }

    private void LeftPressed()
    {
        if (_focusingNext)
        {
            _focusingNext = false;
            UpdateFocusArrows();
        }
        else if (_focusedLetterPosition > 0)
        {
            _focusedLetterPosition--;
            UpdatePointer();
        }   
    }

    private void RightPressed()
    {
        if (_focusingNext) return;
        if (_focusedLetterPosition < 3)
        {
            _focusedLetterPosition++;
            UpdatePointer();
        }
        else
        {
            _focusingNext = true;
            UpdateFocusArrows();
        }
    }

    private void Next()
    {
        StateController.ArchiveHighScoreAs(_name);
        var highScorePanel = Instantiate(_highScorePrefab, transform.parent);
        highScorePanel.IsEndOfGame = true;
        Destroy(gameObject);
    }

    private void UpdateFocusArrows()
    {
        _focusArrows.gameObject.SetActive(_focusingNext);
        _pointer.SetActive(!_focusingNext);
    }

    private void UpdatePointer()
    {
        var pointerPosition = _pointer.transform.position;
        pointerPosition.x = transform.position.x + DefaultOffset + _focusedLetterPosition * LetterOffset;
        _pointer.transform.position = pointerPosition;
    }

    private void CycleName(bool shouldIncrease)
    {
        var chars = _name.ToCharArray();
        var focusedCharInt = Convert.ToInt32(chars[_focusedLetterPosition]);
        if (shouldIncrease) focusedCharInt++;
        else focusedCharInt--;
        chars[_focusedLetterPosition] = Convert.ToChar(FindLink(focusedCharInt));
        _name = chars.ArrayToString();
        _nameText.text = _name;
    }

    private int FindLink(int link)
    {
        switch (link)
        {
            case (64): return 90;
            case (91): return 65;
            default: return link;
        }
    }
}