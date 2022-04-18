using UnityEngine;

namespace RectangleTrainer.Compass.UI
{
    public abstract class ATrackableIcon : ACompassElement
    {
        protected bool highlighted;
        
        public void Highlight(bool state) {
            if (highlighted == state)
                return;

            highlighted = state;
            HighlightLogic(state);
        }

        public abstract void UpdateDistance(float distance, bool visible);
        protected abstract void HighlightLogic(bool state);
    }
}