using UnityEngine;

namespace RectangleTrainer.Compass.UI
{
    public class SimpleUICompass : ACompass
    {
        private float size;
        
        private void Start() {
            size = GetComponent<RectTransform>().rect.width;
            Debug.Log(size);
        }

        protected override float GetIconPosition(float angles) {
            return angles / range * size;
        }

        protected override void FadeIcon(float absoluteAngle, ACompassElement icon, bool forced) {
            float scale = forced ? 1 : Mathf.Clamp01((range/2 - absoluteAngle) / fadeRange);
            icon.Scale(scale);       
        }

        protected override void PositionIcon(float xPos, ACompassElement icon) {
            icon.Translate(new Vector3(xPos, 0, 0));
        }
    }
}