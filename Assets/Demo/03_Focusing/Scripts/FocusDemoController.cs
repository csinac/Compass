using UnityEngine;

namespace RectangleTrainer.Compass.Demo
{
    public class FocusDemoController : MonoBehaviour
    {
        [SerializeField] private TriggerTrackable trackablePF;
        [SerializeField] private int count = 10;
        [SerializeField] private float radius = 5;
        
        private TriggerTrackable[] trackables;

        private void Start() {
            float step = 2 * Mathf.PI / count;
            trackables = new TriggerTrackable[count];
            
            for (int i = 0; i < count; i++) {
                float angles = step * i;
                
                TriggerTrackable trackable = Instantiate(trackablePF);
                trackable.transform.position = new Vector3(Mathf.Cos(angles) * radius, 1, Mathf.Sin(angles) * radius);
                trackable.OnTrigger += HighlightNext;
                trackables[i] = trackable;
            }
            
            HighlightNext();
        }

        private void HighlightNext() {
            int next = Random.Range(0, count);
            
            for (int i = 0; i < count; i++) {
                trackables[i].Focused = next == i;
            }            
        }
    }
}