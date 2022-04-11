using UnityEngine;
using UnityEngine.UI;

namespace RectangleTrainer.Compass.UI
{
    public class LetterIcon : ATrackableIcon
    {
        [SerializeField] private char letter;
        [SerializeField] private Text text;
        [SerializeField] private Text distanceText;

        private RectTransform rt;
        
        protected override void Initialize() {
            text.text = letter.ToString();
            rt = GetComponent<RectTransform>();
        }

        public override void UpdateDistance(float distance) {
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