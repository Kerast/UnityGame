using UnityEngine;
using System.Collections;
using System;
using Boomlagoon.JSON;
using UnityEngine.UI;

public class WebServices : MonoBehaviour  {

    public JSONObject data = new JSONObject();
    public JSONObject player_data = new JSONObject();
    public int connected_user_id;

    protected string _host = "http://localhost/thegame";
    protected string _checkLogin = "/WebServices/app_login.php";
    protected string _getPlayerData= "/WebServices/app_get_player_data.php";
    protected string _savePlayerItem = "/WebServices/app_save_player_item.php";
    protected string _getUserSkinInventory = "/WebServices/app_get_player_inventory_skins.php";

    protected string _method;


    void Start()
    {
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
    }

    public IEnumerator ConnectionToServer(string username, string password)
    {
        
        SetConnectionPanelMessage("Authenticating...");
        //CHECKCREDENTIALS
        string url = _host + _checkLogin;
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            data = JSONObject.Parse(www.text);

            if (data.GetNumber("success") == 1)
            {
                
                SetConnectionPanelMessage(data.GetString("message"));
                yield return new WaitForSeconds(0.2f);
                SetConnectionPanelMessage("Retriving data from server...");
                //GETPLAYERDATA
                connected_user_id = (int)data.GetNumber("uc_user_id");
                url = _host + _getPlayerData;
                form = new WWWForm();
                form.AddField("uc_user_id", data.GetNumber("uc_user_id").ToString());
                www = new WWW(url, form);
                yield return www;

                if (www.error == null)
                {
                    yield return new WaitForSeconds(2);
                    data = JSONObject.Parse(www.text);
                    if (data.GetNumber("success") == 1)
                    {
                        player_data = data;
                        SetConnectionPanelMessage(data.GetString("message"));
                        yield return new WaitForSeconds(0.6f);
                        float fadeTime = GameObject.Find("WebServices").GetComponent<Fading>().BeginFade(1);
                        yield return new WaitForSeconds(fadeTime);
                        Application.LoadLevel("MainMenu");
                       
                    }
                    else
                    {
                        SetConnectionPanelMessage(data.GetString("message"));
                        SetConnectionPanelButtonActive();
                    }
                }
                else
                {
                    SetConnectionPanelMessage("Couldn't communicate with the server...");
                    SetConnectionPanelButtonActive();
                }
            }
            else
            {
                SetConnectionPanelMessage(data.GetString("message"));
                SetConnectionPanelButtonActive();

            }
        }
        else
        {
            SetConnectionPanelMessage("Couldn't communicate with the server...");
            SetConnectionPanelButtonActive();
        }
        
    }


    

    public IEnumerator SaveEquipmentPlayerChange(int equipment_id, string olditemName, string itemName, string itemSkinName)
    {

        //CHECKCREDENTIALS
        string url = _host + _savePlayerItem;
        WWWForm form = new WWWForm();
        form.AddField("equipment_id", equipment_id.ToString());
        form.AddField("oldItemName", olditemName);
        form.AddField("newItemName", itemName);
        form.AddField("newItemSkinName", itemSkinName);
        
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("Yahoo ca marche!!");
        }
        else
        {
            Debug.Log("Couldnt save equipment!!");
        }

    }

    public IEnumerator GetUserSkinInventory(int user_id)
    {

        //CHECKCREDENTIALS
        string url = _host + _getUserSkinInventory;
        WWWForm form = new WWWForm();
        form.AddField("uc_user_id", user_id.ToString());
       

        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("Yahoo ca marche!!");
        }
        else
        {
            Debug.Log("Couldnt save equipment!!");
        }

    }


    void SetConnectionPanelMessage(string message)
    {
        GameObject.Find("ConnectionMessageText").GetComponent<Text>().text = message;
    }

    void SetConnectionPanelButtonActive()
    {
        GameObject.Find("ConnectButton").GetComponent<Login_ConnectButton>().ConnectionPanelButton.SetActive(true);
    }



}
