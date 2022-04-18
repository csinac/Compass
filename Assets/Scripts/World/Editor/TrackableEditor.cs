using RectangleTrainer.Compass.World;
using UnityEditor;

namespace RectangleTrainer.Compass.Inspector
{
    [CustomEditor(typeof(Trackable), true)]
    public class TrackableEditor : Editor
    {
        private SerializedProperty iconProperty;
        private Editor iconEditor;

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