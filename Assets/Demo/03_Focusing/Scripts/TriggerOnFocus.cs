using System;
using RectangleTrainer.Compass.World;
using UnityEngine;

namespace RectangleTrainer.Compass.Demo
{
    public class TriggerOnFocus : Trackable
    {
        public Action OnCorrectTrigger;
        public Action OnWrongTrigger;

        private void OnTriggerEnter(Collider other) {
            if(Focused)
                OnCorrectTrigger?.Invoke();
            else
                OnWrongTrigger?.Invoke();
        }
    }
}