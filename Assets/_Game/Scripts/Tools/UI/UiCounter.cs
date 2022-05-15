using System;
using TMPro;
using UnityEngine;

namespace Scripts.Tools.UI
{
    public class UiCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float _value;
        [SerializeField] private string format = "#.##";

        private void Update()
        {
            //todo: listen events
            UpdateText();
        }

        public float Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;
                _value = value;
                UpdateText();
            }
        }

        private void UpdateText()
        {
            text.text = _value.ToString(format);
        }
    }
}