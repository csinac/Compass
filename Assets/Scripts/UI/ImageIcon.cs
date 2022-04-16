using UnityEngine;
using TMPro;

namespace RectangleTrainer.Compass.UI
{
    public class ImageIcon : ATrackableIcon
    {
        [SerializeField] protected TextMeshProUGUI distanceText;

        private RectTransform rt;
        
        protected override void Initialize() {
            rt = GetComponent<RectTransform>();
        }

        public override void UpdateDistance(float distance, bool visible) {
            distanceText.gameObject.SetActive(visible);
            if(visible)
                distanceText.text = $"{distance:N0}m";
        }

        public override void Toggle(bool state) {
            gameObject.SetActive(state);
        }

        public override void Scale(float scale) {
            rt.localScale = new Vector3(scale, scale, scale);
        }

        public override void Translate(Vector3 position) {
            rt.anchoredPosition = position;
        }
    }
}