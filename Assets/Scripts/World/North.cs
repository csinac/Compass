using System;
using System.Collections.Generic;
using RectangleTrainer.Compass;
using RectangleTrainer.Compass.UI;
using UnityEngine;

namespace RectangleTrainer.Compass.World
{
    public class North : MonoBehaviour
    {
        [SerializeField] private ADirectionIcon directionIconPF;
        [SerializeField] private DirectionCount directionCount;
        [SerializeField, Range(0, 360)] private float offset;

        private static North instance;
        public static float Offset => instance ? instance.offset : 0;
        
        private void Awake() {
            if (instance && instance != this) {
                Destroy(this);
                return;
            }

            instance = this;
        }

        private void Start() {
            int stride = directionCount == DirectionCount.FourDirections ? 2 : 1;

            for (int i = 0; i < CardinalDirections.directions.Length; i += stride) {
                ACompass.AddCardinalDirection(directionIconPF, CardinalDirections.directions[i]);
            }
        }

        private enum DirectionCount
        {
            FourDirections,
            EightDirections
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawMesh(Mesh2Script.ScriptMesh.Cardinal_ScriptMesh.Mesh, Vector3.zero, Quaternion.Euler(0, offset, 0));
        }
    }
}