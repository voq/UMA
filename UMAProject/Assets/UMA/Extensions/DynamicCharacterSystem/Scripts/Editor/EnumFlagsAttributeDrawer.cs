// from http://www.sharkbombs.com/2015/02/17/unity-editor-enum-flags-as-toggle-buttons/
// Extended by Tassim 

using UnityEditor;
using UnityEngine;

namespace UMACharacterSystem
{
	[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
	public class EnumFlagsAttributeDrawer : PropertyDrawer
	{

		public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
		{

			bool[] buttons = new bool[prop.enumNames.Length - 1];
			
			if (label != GUIContent.none)
			{
				EditorGUI.LabelField(new Rect(pos.x, pos.y, EditorGUIUtility.labelWidth, pos.height), label);
			}
			EditorGUI.indentLevel++;
			// Handle button value
			EditorGUI.BeginChangeCheck();

			int buttonsValue = 0;
            for (int i = 0; i < buttons.Length; i++)
			{

				// Check if the button is/was pressed 
				if ((prop.intValue & (1 << i)) == (1 << i))
				{
					buttons[i] = true;
				}
				
				buttons[i] = EditorGUILayout.ToggleLeft(prop.enumNames[i + 1].BreakupCamelCase(), buttons[i]);
				if (buttons[i])
				{
					buttonsValue += 1 << i;
				}			
			}

			// This is set to true if a control changed in the previous BeginChangeCheck block
			if (EditorGUI.EndChangeCheck())
			{
				prop.intValue = buttonsValue;
			}
			EditorGUI.indentLevel--;
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 0f;
		}
	}
}