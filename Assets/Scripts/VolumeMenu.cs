using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    [SerializeField] private Text[] _options;
    [SerializeField] private Menu _mainMenuPrefab;
    [SerializeField] private Text _focusArrows;

    private int _focused;
    private const int MaxVolume = 20;

    private void Start()
    {
        UpdateFocusArrows();
        UpdateVolumeBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            GoUpOnce();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            GoDownOnce();
        if (Input.GetKeyDown(KeyCode.Space))
            Confirm();
        if (_focused != 0) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            VolumeDown();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            VolumeUp();
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
            case (0): break;
            case (1): Back(); break;
        }
    }

    private void VolumeDown()
    {
        StateController.CurrentVolume = Mathf.Clamp(StateController.CurrentVolume - 1, 0, 20);
        UpdateVolumeBar();
    }

    private void VolumeUp()
    {
        StateController.CurrentVolume = Mathf.Clamp(StateController.CurrentVolume + 1, 0, 20);
        UpdateVolumeBar();
    }

    private void Back()
    {
        Instantiate(_mainMenuPrefab, transform.parent);
        Destroy(gameObject);
    }

    private void UpdateFocusArrows()
    {
        var position = _focusArrows.transform.position;
        position.y = _options[_focused].gameObject.transform.position.y;
        _focusArrows.transform.position = position;
    }

    private void UpdateVolumeBar()
    {
        var currentVolume = StateController.CurrentVolume;
        var stringBuilder = new StringBuilder();
        for (var i = 1; i <= MaxVolume; i++)
            stringBuilder.Append(i <= currentVolume ? "|" : ".");
        _options[0].text = stringBuilder.ToString();
    }
}