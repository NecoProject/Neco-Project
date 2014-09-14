using UnityEngine;
using UnityEditor;

/// <summary>
/// Allows the Readme script to have a String property shown as a TextArea instead of a single-line skill
/// </summary>
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
				textProp.stringValue = EditorGUILayout.TextArea(textProp.stringValue, GUILayout.MaxHeight(125));
				serializedObject.ApplyModifiedProperties();
		}
}
