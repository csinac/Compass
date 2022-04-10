using UnityEngine;

namespace RectangleTrainer.Compass.UI
{
    public class Compass : ACompass
    {
        [SerializeField] private float range = 90;
        private float size;
        
        private void Start() {
            size = GetComponent<RectTransform>().rect.width;
            Debug.Log(size);
        }
        
        override protected void UpdateIconPosition(ATrackableIcon icon, float angles) {
            bool visible = Mathf.Abs(angles) < range / 2;
            icon.gameObject.SetActive(visible);

            if (visible) {
                RectTransform rt = icon.GetComponent<RectTransform>();
                float compassPosition = (angles + range / 2) / range * size;
                
                rt.anchoredPosition = new Vector2(compassPosition, 0);
            }
        }
    }
}