using RectangleTrainer.Compass.UI;
using UnityEngine;

namespace RectangleTrainer.Compass.World
{
    public class Trackable : MonoBehaviour
    {
        [SerializeField] private ATrackableIcon icon;
        public Vector3 Position => transform.position;
        public ATrackableIcon Icon => icon;

        private void Start() {
            Tracker.AddTrackable(this);
        }

        private void OnDestroy() {
            Tracker.RemoveTrackable(this);
        }
    }
}