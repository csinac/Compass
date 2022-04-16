using System.Collections.Generic;
using RectangleTrainer.Compass.UI;
using RectangleTrainer.Compass.World;
using UnityEngine;

namespace RectangleTrainer.Compass.Demo
{
    public class CollectibleController : MonoBehaviour
    {
        [SerializeField] private Rect area = new Rect(-10, -10, 20, 20);
        [SerializeField] private GameObject collectiblePF;
        [SerializeField] private ATrackableIcon iconPF;

        private List<GameObject> collectibles;
        
        private void Start() {
            CreateCollectible();
            collectibles = new List<GameObject>();
        }

        private void BurstCollectibles(int count) {
            for (int i = 0; i < count; i++) {
                GameObject collectible = CreateCollectible();
                collectibles.Add(collectible);
            }
        }
        
        private GameObject CreateCollectible() {
            GameObject collectible = Instantiate(collectiblePF);
            CollectibleTrackable trackable = collectible.GetComponent<CollectibleTrackable>();
            trackable.OnCollected += CollectAndCheck;

            if (trackable == null)
                return null;
            
            trackable.SetIconPF(iconPF);

            float x = Random.Range(0, area.width) + area.x;
            float z = Random.Range(0, area.height) + area.y;

            z = Mathf.Sign(z) * (2 * Mathf.Floor(Mathf.Abs(z) / 2) + 1);
            
            collectible.transform.position = new Vector3 (x, 1, z);

            return collectible;
        }

        private void CollectAndCheck(GameObject sender) {
            if (collectibles.Contains(sender))
                collectibles.Remove(sender);

            if (collectibles.Count == 0)
                BurstCollectibles(5);
        }
    }
}
