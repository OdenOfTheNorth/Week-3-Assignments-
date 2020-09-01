using System;
using UnityEngine;
using UnityEngine.UI;

namespace FG
{
    public class Crosshair : MonoBehaviour
    {
        public enum State
        {
            Default,        //0
            Interactable,   //1
        }

        [SerializeField]private Color _defaultStateColor;
        [SerializeField]private Color _stateColorStateColor;
        
        private State _currentState = State.Default;

        private Image _crosshair;

        private void Awake()
        {
            _crosshair = GetComponent<Image>();
            SetState();
        }

        public void SetState(State newState = State.Default)
        {
            switch (newState)
            {
                case State.Default:
                    _crosshair.color = _defaultStateColor;
                    break;
                case State.Interactable:
                    _crosshair.color = _stateColorStateColor;
                    break;
                default:
                    _crosshair.color = _defaultStateColor;
                    break;
            }
        }
    }
}