using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarToggle : MonoBehaviour {

	public Toggle toggle;

	private Image background;
    Text textField;

	void Start () 
	{
		background = toggle.transform.Find( "Background" ).GetComponent<Image>() as Image;
        textField = toggle.transform.Find("Text").GetComponent<Text>() as Text;
		OnTogglePressed();
	}

	public void OnTogglePressed()
	{
		if( toggle.isOn )
		{
			background.color = Color.red;
            textField.text = "Shotgun";
            textField.color = Color.black;
		}
		else
		{
			background.color = Color.blue;
            textField.text = "Rifle";
            textField.color = Color.white;
        }
	}

	void Update () 
	{
	
	}
}
