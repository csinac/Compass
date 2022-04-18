using UnityEngine;

namespace RectangleTrainer.Compass.UI
{
    public abstract class ACompassElement : MonoBehaviour
    {
        protected virtual void Awake() {
            Initialize();
        }
        protected abstract void Initialize();
        public abstract void Toggle(bool state);
        public abstract void Scale(float scale);
        public abstract void Translate(Vector3 position);
    }
}