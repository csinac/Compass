using RectangleTrainer.Compass.World;
using UnityEditor;

namespace RectangleTrainer.Compass.Inspector
{
    [CustomEditor(typeof(Trackable))]
    public class TrackableEditor : Editor
    {
        private Trackable trackable;
        private SerializedProperty iconProperty;
        private Editor iconEditor;
        
        private void Awake() {
            trackable = target as Trackable;
        }

        private void OnEnable() {
            ResetIconEditor();
        }

        public override void OnInspectorGUI() {
            var icon = serializedObject.FindProperty("icon").objectReferenceValue;
            base.OnInspectorGUI();
            var newIcon = serializedObject.FindProperty("icon").objectReferenceValue;

            if (icon != newIcon)
                ResetIconEditor();

            if (iconEditor) {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Icon Settings", EditorStyles.boldLabel);
                iconEditor.OnInspectorGUI();
            }
        }

        private void ResetIconEditor() {
            iconProperty = serializedObject.FindProperty("icon");
            var iconObj = iconProperty.objectReferenceValue;

            if (iconObj == null)
                iconEditor = null;
            else
                iconEditor = CreateEditor(iconObj);
        }
    }
}