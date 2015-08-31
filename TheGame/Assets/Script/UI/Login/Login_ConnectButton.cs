using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Login_ConnectButton : MonoBehaviour {

    public Button ConnectButton;
    public Text UsernameText;
    public InputField PasswordInputField;
    public Text ConnectionPanelMessageText;

    public GameObject ConnectionPanel;
    public GameObject ConnectionPanelButton;

    // Use this for initialization
    void Start () {
        ConnectButton.onClick.AddListener(ConnectToServer);
        
    }
	
	// Update is called once per frame
	void Update () {
        //GameObject.Find("DebugText").GetComponent<Text>().text = GameObject.Find("WebServices").GetComponent<WebServices>().data.ToString();
    }

    void ConnectToServer()
    {

        ConnectionPanel.SetActive(true);
        ConnectionPanelButton.SetActive(false);

        WebServices webservices = GameObject.Find("WebServices").GetComponent<WebServices>();
        string username = UsernameText.text;
        string password = PasswordInputField.text;
        StartCoroutine(webservices.ConnectionToServer(username, password));
        //StartCoroutine(webservices.ConnectionToServer("kerast", "jbbb1565"));


        

    }


}
