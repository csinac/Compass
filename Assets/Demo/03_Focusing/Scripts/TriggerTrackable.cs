using System;
using RectangleTrainer.Compass.World;
using UnityEngine;

namespace RectangleTrainer.Compass.Demo
{
    public class TriggerTrackable : Trackable
    {
        public Action OnTrigger;

        private void OnTriggerEnter(Collider other) {
            OnTrigger?.Invoke();
        }
    }
}
