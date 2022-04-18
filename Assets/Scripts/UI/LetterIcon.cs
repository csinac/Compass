using UnityEngine;
using TMPro;

namespace RectangleTrainer.Compass.UI
{
    public class LetterIcon : ImageIcon
    {
        [SerializeField] protected char letter;
        [SerializeField] private TextMeshProUGUI text;

        protected override void Initialize() {
            base.Initialize();
            text.text = letter.ToString();
        }

        protected override void HighlightLogic(bool state) {
            base.HighlightLogic(state);
            text.color = state ? highlightColor : normalColor;
        }
    }
}