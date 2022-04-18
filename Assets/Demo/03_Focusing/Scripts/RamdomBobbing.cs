using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RectangleTrainer.Compass.Demo
{
    public class RamdomBobbing : MonoBehaviour
    {
        [SerializeField] private float range = 0.2f;

        private float speed;
        private float t;
        private Vector3 initialPos;
        
        private void Start() {
            speed = Random.Range(0.9f, 1.1f);
            t = Random.Range(0, 2 * Mathf.PI);
            initialPos = transform.position;
        }

        private void Update() {
            t += Time.deltaTime * speed;
            transform.position = new Vector3(initialPos.x, initialPos.y + Mathf.Sin(t) * range, initialPos.z);
        }
    }
}