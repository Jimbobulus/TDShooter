using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarToggle : MonoBehaviour {

	public Toggle toggle;

	private Image background;

	void Start () 
	{
		background = toggle.transform.FindChild( "Background" ).GetComponent<Image>() as Image;
		OnTogglePressed();
	}

	public void OnTogglePressed()
	{
		if( toggle.isOn )
		{
			background.color = Color.red;
		}
		else
		{
			background.color = Color.blue;
		}
	}

	void Update () 
	{
	
	}
}
