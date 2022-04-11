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
        public abstract void Toggle(bool state);
        public abstract void Scale(float scale);
        public abstract void Translate(Vector3 position);
    }
}