using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Readme)), CanEditMultipleObjects]
public class TextArea : Editor
{
		public SerializedProperty textProp;

		void OnEnable()
		{
				textProp = serializedObject.FindProperty("text");
		}

		public override void OnInspectorGUI()
		{
				serializedObject.Update();
				textProp.stringValue = EditorGUILayout.TextArea(textProp.stringValue, GUILayout.MaxHeight(75));
				serializedObject.ApplyModifiedProperties();
		}
}
