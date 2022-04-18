using TMPro;
using UnityEngine;

namespace RectangleTrainer.Compass.Demo
{
    public class FocusDemoController : MonoBehaviour
    {
        [SerializeField] private TriggerOnFocus trackablePF;
        [SerializeField] private int count = 10;
        [SerializeField] private int range = 40;
        [SerializeField] private TextMeshProUGUI triggerCountLabel;
        
        private TriggerOnFocus[] trackables;
        private int triggerCount = 0;

        private void Start() {
            float step = 2 * Mathf.PI / count;
            trackables = new TriggerOnFocus[count];
            
            for (int i = 0; i < count; i++) {
                float angles = step * i;
                
                TriggerOnFocus trackable = Instantiate(trackablePF);
                trackable.transform.position = new Vector3(Random.Range(-40, 40), 1.5f, Random.Range(-40, 40));
                trackable.OnCorrectTrigger += HighlightAndIncrement;
                trackable.OnWrongTrigger += Decrement;
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

        private void HighlightAndIncrement() {
            HighlightNext();
            triggerCountLabel.text = (++triggerCount).ToString();
        }

        private void Decrement() {
            triggerCount = Mathf.Max(--triggerCount, 0);
            triggerCountLabel.text = triggerCount.ToString();
        }
    }
}