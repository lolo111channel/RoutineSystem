using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManagerUI : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private TMP_Text _textMesh;

    private void Update()
    {
        if (_timeManager != null && _textMesh != null)
            _textMesh.text = SetClockText(_timeManager.Hours, _timeManager.Minutes);
    }

    string SetClockText(int hours, int minutes)
    {
        string textHours = hours.ToString();
        string textMinutes = minutes.ToString();

        if (textHours.Length < 2)
            textHours = "0" + textHours;

        if (textMinutes.Length < 2)
            textMinutes = "0" + textMinutes;

        return $"{ textHours }:{ textMinutes }";
    }


}
