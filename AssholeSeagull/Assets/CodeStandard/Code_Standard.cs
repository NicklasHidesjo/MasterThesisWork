using System;
using UnityEngine;

// Disable warning about unused fields and variables hidden
// (you do not need to care about this).
#pragma warning disable 0414
#pragma warning disable 0108

public class Code_Standard: MonoBehaviour
{
	#region Naming Convention
	[Header("Naming Convention")]
	// Private and [SerializeField] variables are camelCase
	// Properties and Methods are PascalCase

	// Do
	[SerializeField] private string name = "Default";
	private string anotherName = "Default";

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}
	public bool HasName()
	{
		return name != "Default";
	}

	// Don't
	[SerializeField] private string MyName = "Default";
	private string MyOtherName = "Default";

	public string myName
	{
		get
		{
			return MyName;
		}
		set
		{
			MyName = value;
		}
	}
	public bool hasName()
	{
		return MyName != "Default";
	}

	#endregion

	#region Order Convention
	[Header("Order Convention")]
	// [SerializeField] variables above normal variables 
	[SerializeField] int serializeFieldNumber = 10;
	
	// normal variables above properties
	private int variableNumber = 10;
	
	// properties above methods
	public int PropertyNumber 
	{ 
		get 
		{ 
			return variableNumber; 
		} 
		set 
		{ 
			variableNumber = value; 
		} 
	}

	// methods last
	public void MyMethod()
	{

	}

	#endregion
	
	#region Method Order Convention
	// Methods should be written in the order they are called.
	// public methods should be at the bottom of the script.

	// Unitys own methods (awake,start,update,onEnable,onDisable)
	// should be in their execution order and all other methods inbetween.
	// Link to the docs on order of execution https://docs.unity3d.com/Manual/ExecutionOrder.html

	// Do
	private void Awake()
	{
		AwakeCalledMethod();
	}
	private void AwakeCalledMethod()
	{
		CalledFromMany();
	}

	private void OnEnable()
	{
		OnEnableCalledMethod();
	}
	private void OnEnableCalledMethod()
	{
		CalledFromMany();
	}

	private void Start()
	{
		OnStartCalledMethod();
	}
	private void OnStartCalledMethod()
	{

	}

	private void Update()
	{
		OnUpdateCalledMethod();
	}
	private void OnUpdateCalledMethod()
	{
		CalledFromMany();
	}

	private void OnDisable()
	{
		OnDisableCalledMethod();
	}
	private void OnDisableCalledMethod()
	{
		CalledFromMany();
	}

	private void OnDestroy()
	{
		OnDestroyCalledMethod();
	}
	private void OnDestroyCalledMethod()
	{

	}

	private void CalledFromMany()
	{

	}

	public void MyPublicMethod()
	{

	}

	// Don't
	private void OnEnableCalledMethod(int value)
	{

	}
	private void AwakeCalledMethod(int value)
	{

	}
	private void OnStartCalledMethod(int value)
	{

	}
	private void OnUpdateCalledMethod(int value)
	{

	}
	private void OnDisableCalledMethod(int value)
	{

	}
	private void OnDestroyCalledMethod(int value)
	{

	}
	public void MyPublicMethod(int value)
	{

	}

	private void Awake(int value)
	{
		AwakeCalledMethod(10);
	}

	private void OnEnable(int value)
	{
		OnEnableCalledMethod(10);
	}

	private void Start(int value)
	{
		OnStartCalledMethod(10);
	}

	private void Update(int value)
	{
		OnUpdateCalledMethod(10);
	}

	private void OnDisable(int value)
	{
		OnDisableCalledMethod(10);
	}

	private void OnDestroy(int value)
	{
		OnDestroyCalledMethod(10);
	}


	#endregion

	#region Accessibility Convention
	[Header("Accessibility Convention")]
	// Write access level on everything

	// Do 
	[SerializeField] private int aInt = 1;
	private int bInt = 1;
	public int AInt
	{
		get
		{
			return aInt;
		}
		set
		{
			aInt = value;
		}
	}
	private void AMethod()
	{

	}
	public void BMethod()
	{

	}

	// Don't
	[SerializeField] int cInt = 1;
	int dInt = 1;
	public int CInt
	{
		get
		{
			return cInt;
		}
		set
		{
			cInt = value;
		}
	}
	void CMethod()
	{

	}
	public void DMethod()
	{

	}
	#endregion

	#region Variable Convention
	[Header("Variable Convention")]
	// Always set a variables default value
	// Do
	[SerializeField] private float aFloat = 1f;
	private float bFloat = 1f;

	// Don't 
	[SerializeField] private float cFloat;
	private float dFloat;

	// group variables togheter
	// Do
	private float velocity = 10f;
	private float velocityIncrease = 1f;
	private float terminalVelocity = 20f;

	private float hunger = 40f;
	private float maxSatiation = 100f;
	private float starveThreshold = 10f;

	// Don't
	private float _velocity = 10f;
	private float _maxSatiation = 100f;
	private float _velocityIncrease = 1f;
	private float _hunger = 40f;
	private float _terminalVelocity = 20f;
	private float _starveThreshold = 10f;


	// When having multiple [SerializeFields] group them and use a header,
	// to sort them and make it more readable
	[Header("Header Usage")]

	[Header("Do")]

	[Header("Speed Settings")]
	[SerializeField] private float myVelocity = 10f;
	[SerializeField] private float myVelocityIncrease = 1f;
	[SerializeField] private float myTerminalVelocity = 20f;
	[Header("Food Settings")]
	[SerializeField] private float myHunger = 40f;
	[SerializeField] private float myMaxSatiation = 100f;
	[SerializeField] private float myStarveThreshold = 10f;

	[Header("Don't")]
	[SerializeField] private float _myVelocity = 10f;
	[SerializeField] private float _myVelocityIncrease = 1f;
	[SerializeField] private float _myTerminalVelocity = 20f;
	[SerializeField] private float _myHunger = 40f;
	[SerializeField] private float _myMaxSatiation = 100f;
	[SerializeField] private float _myStarveThreshold = 10f;


	// if a variable needs it use [ToolTip("")] to describe what the variable does.
	// have the ToolTip placed above the variable, to make sure the line wont get to long.
	// The tooltip is great as that will allow the designer to hover over a variable
	// and get information about what it does. This also allows us to not have to long 
	// variable names.

	[Header("ToolTip usage")]

	[Header("Do")]
	[Tooltip("This is the increase in speed(m/s) that the ball gets")]
	[SerializeField] private float speedIncrease = 0.5f;

	[Header("Don't")]
	[SerializeField] private float ballSpeedIncreaseInMeterPerSecond = 0.5f;
	#endregion

	#region Properties Convention
	// Make properties instead of Get/Set methods
	// Do
	private int myPropertyNumber = 10;
	public int MyPropertyNumber
	{
		get
		{
			return myPropertyNumber;
		}
		set
		{
			myPropertyNumber = value;
		}
	}

	// Don't
	private int myNumber = 10;
	public int GetMyNumber()
	{
		return myNumber;
	}
	public void SetMyNumber(int value)
	{
		myNumber = value;
	}

	// Make properties instead of public variables.

	/// <Reasons>
	/// 1. Properties allow for some protection. For example you can write your own 
	/// code into the get and set method (controlling what happens and when)
	/// you can also make a readonly, having only the get inside or having a private set.
	/// 
	/// 2. A property won't show in the inspector, meaning a game designer can't 
	/// accidentaly change it thinking it's something they should touch. 
	/// </Reasons>

	// Do 
	private float myPropertyFloat = 10f;
	public float MyPropertyFloat
	{
		get
		{
			return myPropertyFloat;
		}
		set
		{
			myPropertyFloat = value;
		}
	}

	// Don't
	public float myPublicFloat = 10f;
	#endregion
}
