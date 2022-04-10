using UnityEngine;

namespace RectangleTrainer.Compass.UI
{
    public abstract class ATrackableIcon : MonoBehaviour
    {
        protected virtual void Start() {
            Initialize();
        }

        protected abstract void Initialize();
        public abstract void UpdateDistance(float distance);
    }
}