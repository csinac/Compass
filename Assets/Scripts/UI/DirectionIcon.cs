using UnityEngine;
using UnityEngine.UI;

namespace RectangleTrainer.Compass.UI
{
    public class DirectionIcon : ADirectionIcon
    {
        [SerializeField] private Text text;
        private RectTransform rt;
        private RectTransform textRT;
        
        public override void Initialize(CardinalDirections.Direction dir) {
            text.text = dir.Name;
            degrees = dir.Degrees;
        }
        
        protected override void Initialize() {
            rt = GetComponent<RectTransform>();
            textRT = text.GetComponent<RectTransform>();
        }
        
        public override void Toggle(bool state) {
            gameObject.SetActive(state);
        }

        public override void Scale(float scale) {
            textRT.localScale = new Vector3(scale, scale, scale);
        }

        public override void Translate(Vector3 position) {
            rt.anchoredPosition = position;
        }
    }
}