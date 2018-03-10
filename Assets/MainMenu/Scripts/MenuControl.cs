using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DirectionCredits{
	up, down
}

public class MenuControl : MonoBehaviour {

	[Header("Objects Settings")]
	[Space]
	public GameObject mask;								// Mask to make non clicable and dark effect
	public GameObject mainScreen;						// Object of the main screen
	public GameObject creditsScreen;					// Object of the credits screen
	public GameObject modesScreen;
	public GameObject optionsScreen;
	public GameObject creditsTextBack;					// Object of text credits "Back to go main screen"

	[Header("Global Menu Settings")]
	[Space]
	
	//public bool disableLine;							// Underline in the menu items
	public bool useCustomCursor=true;   				// Enable custom cursor?
	public Texture2D cursor;							// Custom Cursor Image
	public Menu[] menu = new Menu[0];									// List of all menu item

	[Header("Play Settings")]
	[Space]
	[Tooltip("Pick through modes, the baby mode or the hard mode!")]
	public bool noModes = true;							// There are game modes
	[Tooltip("Reminder: If you turn on every time you click play, the initial animation in the mode screen is active")]
	public bool resetAnim = true;						// Do you want to enable / disable animation of the modes screen whenever you click play
	public string nameLoadLevel;						// Name of the game level you want to load
	public Mode[] modes;								// List of all modes of game

	[Header("Credits Settings")]
	[Space]
	public bool animCreditsScale = true;				// Enable scaling animation
	public bool animDirectionCredits = true;			// Enable direction animation
	public float YCreditsEnd;							// Y axis where the direction animation ends
	public DirectionCredits dirCredits;					// Direction where credits go
	public float initScale = 0.2f;						// Initial Scale
	public float maxScale = 1f;							// Maximum scale
	public float speedAnimScaleCredits = 0.75f;			// Scale animation speed
	public float speedAnimDirCredits = 50f;				// Steering animation speed


	// Variables that the user does not need to change
	private UITransition _uitransition;

	// Cursor
	private Vector2 _hotSpot = Vector2.zero;

	// Credits
	private Vector3 _initCreditsPos;					// Variable responsible for the initial position of creditScreen
	private RectTransform _rectCredits;					// Variable responsible for the RectTransform component
	private float _currentScale;						// Variable responsible for the current scale of Animation Credits Scale
	private bool _startScaleAnimCredits = false;		// Responsible variable if scaling animation can start
	private bool _startDirAnimCredits = false;			// Responsible variable if the steering animation can start
	private bool _inCreditsScreen = false;				// Responsible variable if the credits screen is active or not
	private bool _inModesScreen = false;				// Responsible variable if the modes screen is active or not
	private bool _inOptionsScreen = false;				// Responsible variable if the options screen is active or not

	//Editor
	[HideInInspector]
	public int currentTab;
	[HideInInspector]
	public int previousTab=-1;

	[Header("Organize Menu")]
	public int spaceMenu=45;
	public int xSpace=100;
	public int yAdjust=0;

	[Header("Organize Modes")]
	public int spaceModes=250;
	public int xStart=250;


	// Use this for initialization
	void Start () {
		basicSettings ();
		firstCreditsSettings ();
		reportErrors ();
	}

	// Update is called once per frame
	void Update () {
		//-------------------------------------------------------------------------START MODES: METHODS CALL---------------------------------------------------------------------\\
		quitModesScreen ();
		//-------------------------------------------------------------------------END MODES: METHODS CALL-----------------------------------------------------------------------\\



		//-------------------------------------------------------------------------START OPTIONS: METHODS CALL-------------------------------------------------------------------\\
		quitOptionsCredits ();
		//-------------------------------------------------------------------------END OPTIONS: METHODS CALL---------------------------------------------------------------------\\



		//-------------------------------------------------------------------------START CREDITS: METHODS CALL-------------------------------------------------------------------\\
		updateAnimScaleCredits ();     // Animation of scale for the Credits
		updateAnimDirectionCredits (); // Animation of direction for the Credits
		quitScreenCredits ();		   // Exit credit screen
		//-------------------------------------------------------------------------END CREDITS: METHODS CALL---------------------------------------------------------------------\\
	}


	//-----------------------------------------------------------------------------START METHODS MENU CONTROL--------------------------------------------------------------------\\
	// Set the basics settings
	void basicSettings(){
		// Active or deactivated first objets in scene
		mainScreen.SetActive (true);
		creditsScreen.SetActive (false);
		creditsTextBack.SetActive (false);
		modesScreen.SetActive (false);
		optionsScreen.SetActive (false);

		setAlphaMask (0.5f);

		// Get all components
		_rectCredits = creditsScreen.GetComponent<RectTransform>();
		_uitransition = this.GetComponent<UITransition> ();

		// Set Custom Cursor
		if(useCustomCursor==true)
			Cursor.SetCursor(cursor, _hotSpot, CursorMode.ForceSoftware);

	}// END

	// some fucking debugging ffs
	void reportErrors(){
		if (mask == null) {Debug.LogError ("MAINMENU: Variable 'mask' is not declared");}
		if (mainScreen == null) {Debug.LogError ("MAINMENU: Variable 'mainScreen' is not declared");}
		if (creditsScreen == null) {Debug.LogError ("MAINMENU: Variable 'creditsScreen' is not declared");}
		if (modesScreen == null) {Debug.LogError ("MAINMENU: Variable 'modesScreen' is not declared");}
		if (creditsTextBack == null) {Debug.LogError ("MAINMENU: Variable 'creditsTextBack' is not declared");}
	}// END

	// RESET ALL MENUS TO STANDARD STATE 
	void resetMenu(){
		foreach (Menu m in menu) {											// Scrolls through the entire menu list
	//		m.menuDisable ();												// Become standard
		}
	}// END

	// RESET ALL MODE TO STANDARD STATE 
	void resetMode(){
		foreach (Mode m in modes) {											// Scrolls through the entire modes list
			m.mouseExit ();													// Become standard
		}
	}// END

	// Set Alpha Mask with value
	public void setAlphaMask(float value){
		mask.GetComponent<CanvasRenderer>().SetAlpha(value); // Set Alpha of CanvasRenderer
	}
	//-------------------------------------------------------------------------END METHODS MENU CONTROL----------------------------------------------------------------------------\\


	//-------------------------------------------------------------------------START METHODS PLAY/MODES SCREEN---------------------------------------------------------------------\\
	// Play Button
	public void StartGame(){
		if (noModes) {																// if we dont do the modes
			fadeInEffect ();														// Load Level Game
		} else {																	// ELSE
			setModesScreen (true);													// The mode screen is active
		}
	}// END

	// Fade Effect
	void fadeInEffect(){
		// Start Fade Effect
		mask.SetActive (true);
		_uitransition.Play();
	}// END

	// Loads game level
	public void loadLevelGame(){
		if (nameLoadLevel != null) {                                                // If the name of the game level to be loaded is not empty
            UnityEngine.SceneManagement.SceneManager.LoadScene (nameLoadLevel);									// Load game level
		} else {																	// ELSE
			Debug.LogWarning ("Variable nameLoadLevel is "+nameLoadLevel);			// shits empty son
		}
	}// END

	// Updating the Modes Exit Button
	void quitModesScreen(){
		// If you press the "esc" key and the modes screen is active
		if (_inModesScreen == true) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				setModesScreen (false); // Disable credit screen
			}
		}
	} // END

	// Changes between the main screen and the modes screen
	void setModesScreen(bool value){
		if (value) {
			mainScreen.SetActive (false);											// Disables the Main Screen
			modesScreen.SetActive (true);											// Active the Mode Screen
			_inModesScreen = true; 													// In modeScreen
			resetMode();															// Reset all modes for the standart state
			if (resetAnim) {														// If the option to enable / reset the initial animation of the modes screen
				foreach (Mode mode in modes) {										// It passes through all modes modes within the variable "modes"
					mode.resetAnim ();												// And call the method by resetting position and time
				}
			}
		} else {
			modesScreen.SetActive (false);											// Disables the Credits Screen
			mainScreen.SetActive (true);											// Active the Main Screen
			_inModesScreen = false;													// Quit mode screen
			resetMenu();															// Reset all menus to standart state
		}
	}// END
	//-------------------------------------------------------------------------END METHODS PLAY/MODES SCREEN---------------------------------------------------------------------\\


	//-------------------------------------------------------------------------START METHODS CREDITS SCREEN----------------------------------------------------------------------\\
	// Starts initial credit settings
	void firstCreditsSettings(){
		_initCreditsPos = _rectCredits.localPosition; 					 				// Get First Position of init Credits
		if (animCreditsScale == true) { 
			_rectCredits.localScale = new Vector2 (initScale, initScale); 				// Set scale for initScale
			_currentScale = initScale;
		}
	} // END

	// Update the scale animation of credits
	void updateAnimScaleCredits(){
		if (animCreditsScale == true) { 												// If a scale animation to active
			if (_currentScale <= maxScale && _startScaleAnimCredits == true) {			// If the scale is smaller than the maximum scale and you can start the scale animation
				_currentScale += speedAnimScaleCredits * Time.deltaTime;				// The "currentScale" variable begins to increase
				_rectCredits.localScale = new Vector2 (_currentScale, _currentScale);	// The scale of the credits object is updated with the variable "currentScale"
			}

			if (_currentScale >= maxScale) {											// If the scale of the object is already in the defined size
				_startDirAnimCredits = true;											// Move animation can start
			}
		}
	} // END

	// Updating the Credits Exit Button
	void quitScreenCredits(){
		// If you press the "esc" key or the credits finish the direction animation and the credit screen is active
		if (_inCreditsScreen == true) {
			if (Input.GetKeyDown (KeyCode.Escape) || _rectCredits.localPosition.y >= YCreditsEnd) {
				setCreditsScreen (false); // Disable credit screen
			}
		}
	} // END


	// Update the direction animation of credits
	void updateAnimDirectionCredits(){
		if (animDirectionCredits == true && _startDirAnimCredits == true) {				// If the steering animation is active and it can start
			if (dirCredits == DirectionCredits.up) {									// If the animation is up
				// Rises with speed defined in variable speedAnimDirCredits
				_rectCredits.localPosition = new Vector2 (_rectCredits.localPosition.x, _rectCredits.localPosition.y + (speedAnimDirCredits * Time.deltaTime));
			} else {																	// If the animation is down
				// Descends with the velocity defined in the variable speedAnimDirCredits
				_rectCredits.localPosition = new Vector2 (_rectCredits.localPosition.x, _rectCredits.localPosition.y - (speedAnimDirCredits * Time.deltaTime));
			}
		}
	} // END

	// Changes between the main screen and the credits screen
	public void setCreditsScreen(bool value){
		// Go to Credits Screen
		if (value == true) {
			// All back to the initial configuration
			_rectCredits.localPosition = _initCreditsPos; 								// Return to starting position
			if (animCreditsScale == true) {												// If scale animation is active
				_rectCredits.localScale = new Vector2 (initScale, initScale);			// Back to credits scale for the initial
				_currentScale = initScale;												// The current scale has the same value as the initial
				_startScaleAnimCredits = true;											// Scaling animation starts
			} else {																	// ELSE
				_startDirAnimCredits = true;											// Direction animation starts
			}

			// Active or deactivated the objects
			creditsTextBack.SetActive (true); 											// Active text "back to go main screen"
			mainScreen.SetActive (false);												// Disables the Main Screen
			creditsScreen.SetActive (true);												// Active the Credits Screen
			setAlphaMask(0.5f);
			mask.SetActive (true);														// Active the Mask

			_inCreditsScreen = true;													// Credits screen is active
		}else{ // Go to Main Screen
			// Animations are stopped
			_startScaleAnimCredits = false;												// Scaling animation is disabled
			_startDirAnimCredits = false;												// Direction animation is disabled

			// Active or deactivated the objects
			creditsTextBack.SetActive (false);											// Disables text "back to go main screen"
			creditsScreen.SetActive (false);											// Disables the Credits Screen
			mainScreen.SetActive (true);												// Active the Main Screen
			mask.SetActive (false);														// Disables the Mask

			_inCreditsScreen = false;													// Credits screen is disables
			resetMenu();																// Reset all menus to standart state
		}
	}// END
	//-------------------------------------------------------------------------END METHODS CREDITS SCREEN------------------------------------------------------------------------\\

	//-------------------------------------------------------------------------START METHODS OPTIONS SCREENS---------------------------------------------------------------------\\
	public void setOptionsScreen(bool value){
		if (value) {											// If true
			mainScreen.SetActive (false);						// Disable main screen
			optionsScreen.SetActive (true);						// Active Options Screen
			_inOptionsScreen = true;							// On in Options Screen
		} else {												// ELSE
			optionsScreen.SetActive (false);					// Disable Options Screen
			mainScreen.SetActive (true);						// Active Main Screen
			_inOptionsScreen = false;							// Exits of Options Screen
			resetMenu ();										// 
		}
	}// END

	// Updating the Credits Exit Button
	void quitOptionsCredits(){
		// If you press the "esc" key or the credits finish the direction animation and the credit screen is active
		if (_inOptionsScreen == true) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				setOptionsScreen (false); // Disable credit screen
			}
		}
	}// END
	//-------------------------------------------------------------------------END METHODS OPTIONS SCREENS-----------------------------------------------------------------------\\

	public void SetDefaultValues(){
		useCustomCursor = true;
		noModes = true;
		resetAnim = false;
		nameLoadLevel = "YourGame";
		animCreditsScale = false;
		animDirectionCredits = true;
		YCreditsEnd = 400;
		dirCredits = DirectionCredits.up;
		initScale = 0f;
		maxScale = 1f;
		speedAnimDirCredits = 0.75f;
		speedAnimDirCredits = 50f;

		spaceMenu=45;
		xSpace=100;
		yAdjust=0;

		spaceModes=250;
		xStart=250;
	}

	public void OrganizeMenus(){
		for (int i = 0; i < menu.Length; i++){ 
			float a = -spaceMenu * i;
			menu [i].transform.localPosition = new Vector3 (xSpace, yAdjust+a, menu[i].transform.localPosition.z);
		}
		Debug.Log ("Menus Organized!");
	}

	public void OrganizeModes(){
		for (int i = 0; i < modes.Length; i++){ 
			float a = spaceModes * i;
			modes [i].transform.localPosition = new Vector3 (-xStart+a, modes[i].transform.localPosition.y, modes[i].transform.localPosition.z);
		}
		Debug.Log ("Modes Organized!");
	}

	void OnDrawGizmosSelected(){
		
	}
}