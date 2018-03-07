using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(MenuControl))]
public class MenuControlEditor : Editor {

	private MenuControl menuControl;
	private SerializedObject script;

	// Objects Settings
	public SerializedProperty mask;
	public SerializedProperty mainScreen;
	public SerializedProperty creditsScreen;
	public SerializedProperty modesScreen;
	public SerializedProperty optionsScreen;
	public SerializedProperty creditsTextBack;

	// Global Menu Settings
	public SerializedProperty menu;
	public SerializedProperty useCustomCursor;
	public SerializedProperty cursor;
	public SerializedProperty spaceMenu;
	public SerializedProperty xSpace;
	public SerializedProperty yAdjust;
	ReorderableList menu2;

	// Play Settings
	public SerializedProperty noModes;
	public SerializedProperty resetAnim;
	public SerializedProperty nameLoadLevel;
	public SerializedProperty spaceModes;
	public SerializedProperty xStart;
	public SerializedProperty modes;
	ReorderableList modes2;

	// Credits Settings
	public SerializedProperty animCreditsScale;
	public SerializedProperty animDirectionCredits;
	public SerializedProperty YCreditsEnd;
	public SerializedProperty dirCredits;
	public SerializedProperty initScale;
	public SerializedProperty maxScale;
	public SerializedProperty speedAnimScaleCredits;
	public SerializedProperty speedAnimDirCredits;

	public void OnEnable() {
		menuControl = (MenuControl)target;
		script = new SerializedObject (target);

		// Objects Settings
		mask = script.FindProperty ("mask");
		mainScreen = script.FindProperty ("mainScreen");
		creditsScreen = script.FindProperty ("creditsScreen");
		modesScreen = script.FindProperty ("modesScreen");
		optionsScreen = script.FindProperty ("optionsScreen");
		creditsTextBack = script.FindProperty ("creditsTextBack");

		// Global Menu Settings
		useCustomCursor = script.FindProperty ("useCustomCursor");
		cursor = script.FindProperty ("cursor");
		menu = script.FindProperty ("menu");
		spaceMenu = script.FindProperty ("spaceMenu");
		xSpace = script.FindProperty ("xSpace");
		yAdjust = script.FindProperty ("yAdjust");
		this.menu2 = new ReorderableList (script, menu);

		// Play Settings
		noModes = script.FindProperty ("noModes");
		resetAnim = script.FindProperty ("resetAnim");
		nameLoadLevel = script.FindProperty ("nameLoadLevel");
		spaceModes = script.FindProperty ("spaceModes");
		xStart = script.FindProperty ("xStart");
		modes = script.FindProperty ("modes");
		this.modes2 = new ReorderableList (script, modes);

		// Credits Settings
		animCreditsScale = script.FindProperty ("animCreditsScale");
		animDirectionCredits = script.FindProperty ("animDirectionCredits");
		YCreditsEnd = script.FindProperty ("YCreditsEnd");
		dirCredits = script.FindProperty ("dirCredits");
		initScale = script.FindProperty ("initScale");
		maxScale = script.FindProperty ("maxScale");
		speedAnimScaleCredits = script.FindProperty ("speedAnimScaleCredits");
		speedAnimDirCredits = script.FindProperty ("speedAnimDirCredits");

		this.menu2.drawElementCallback = RectNewMenu;
		this.menu2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Menus");
		};

		this.modes2.drawElementCallback = RectNewModes;
		this.modes2.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField (rect, "Modes");
		};
	}

	public override void OnInspectorGUI() {
		//DrawDefaultInspector ();

		script.Update ();
		EditorGUI.BeginChangeCheck ();

		menuControl.currentTab = GUILayout.Toolbar(menuControl.currentTab, new string[] {"Objects Settings", "Menu Settings", "Play Settings", "Credits Settings"});

		if (menuControl.currentTab == 0) {
			EditorGUILayout.PropertyField (mask);
			EditorGUILayout.PropertyField (mainScreen);
			EditorGUILayout.PropertyField (creditsScreen);
			EditorGUILayout.PropertyField (modesScreen);
			EditorGUILayout.PropertyField (optionsScreen);
			EditorGUILayout.PropertyField (creditsTextBack);
			if (menuControl.currentTab != menuControl.previousTab) {
				GUI.FocusControl (null);
				menuControl.previousTab = menuControl.currentTab;
			}
		} else if (menuControl.currentTab == 1) {
			EditorGUILayout.PropertyField (useCustomCursor);
			EditorGUILayout.PropertyField (cursor);
			menu2.DoLayoutList ();
			EditorGUILayout.PropertyField (spaceMenu);
			EditorGUILayout.PropertyField (xSpace);
			EditorGUILayout.PropertyField (yAdjust);
			if (GUILayout.Button ("Organize Menus")) {
				GUI.FocusControl (null);
				menuControl.OrganizeMenus ();
			}

			if (menuControl.currentTab != menuControl.previousTab) {
				GUI.FocusControl (null);
				menuControl.previousTab = menuControl.currentTab;
			}
		} else if (menuControl.currentTab == 2) {
			EditorGUILayout.PropertyField (noModes);
			EditorGUILayout.PropertyField (resetAnim);
			EditorGUILayout.PropertyField (nameLoadLevel);
			modes2.DoLayoutList ();
			EditorGUILayout.PropertyField (spaceModes);
			EditorGUILayout.PropertyField (xStart);
			if (GUILayout.Button ("Organize Modes")) {
				GUI.FocusControl (null);
				menuControl.OrganizeModes ();
			}

			if (menuControl.currentTab != menuControl.previousTab) {
				GUI.FocusControl (null);
				menuControl.previousTab = menuControl.currentTab;
			}
		} else if (menuControl.currentTab == 3) {
			EditorGUILayout.PropertyField (animCreditsScale);
			EditorGUILayout.PropertyField (animDirectionCredits);
			EditorGUILayout.PropertyField (YCreditsEnd);
			EditorGUILayout.PropertyField (dirCredits);
			EditorGUILayout.PropertyField (initScale);
			EditorGUILayout.PropertyField (maxScale);
			EditorGUILayout.PropertyField (speedAnimScaleCredits);
			EditorGUILayout.PropertyField (speedAnimDirCredits);
			if (menuControl.currentTab != menuControl.previousTab) {
				GUI.FocusControl (null);
				menuControl.previousTab = menuControl.currentTab;
			}
		}

		if (GUILayout.Button ("Set Default Values")) {
			GUI.FocusControl (null);
			menuControl.SetDefaultValues ();
		}

		if (EditorGUI.EndChangeCheck ()) {
			script.ApplyModifiedProperties ();
			//GUI.FocusControl (null);
		}

	}

	private void RectNewMenu(Rect rect, int index, bool active, bool focus){
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), menu.GetArrayElementAtIndex (index));
	}

	private void RectNewModes(Rect rect, int index, bool active, bool focus){
		EditorGUI.PropertyField (new Rect(rect.x, rect.y, rect.width, 16), modes.GetArrayElementAtIndex (index));
	}

}
