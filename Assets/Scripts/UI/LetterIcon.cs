using UnityEngine;
using UnityEngine.UI;

namespace RectangleTrainer.Compass.UI
{
    public class LetterIcon : ATrackableIcon
    {
        [SerializeField] private char letter;
        [SerializeField] private Text text;
        [SerializeField] private Text distanceText;
        
        protected override void Initialize() {
            text.text = letter.ToString();
        }

        public override void UpdateDistance(float distance) {
            distanceText.text = $"{distance:N0}m";
        }
    }
}