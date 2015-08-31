using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Boomlagoon.JSON;
using UnityEngine.UI;
using TheGame.ItemSystem;

public class WebServices : MonoBehaviour  {

    public JSONObject data = new JSONObject();

    public JSONObject player_data = new JSONObject();

    public int connected_user_id;

    public int pp;
    public int pg;
    public int xp;


    public List<InventoryItem> Items = new List<InventoryItem>();
    public List<InventoryItem> Skins = new List<InventoryItem>();
    public List<InventoryItem> Crates = new List<InventoryItem>();

    public List<InventoryItem> ShopItems = new List<InventoryItem>();
    public List<InventoryItem> ShopSkins = new List<InventoryItem>();
    public List<InventoryItem> ShopCrates = new List<InventoryItem>();



    protected string _host = "http://localhost/thegame";
    protected string _checkLogin = "/WebServices/app_login.php";
    protected string _getPlayerData= "/WebServices/app_get_player_data.php";
    protected string _getCharacterData = "/WebServices/app_get_character_data.php";
    protected string _savePlayerItem = "/WebServices/app_save_player_item.php";
    protected string _getUserItemInventory = "/WebServices/app_get_player_inventory.php";
    protected string _addUserInventoryItem = "/WebServices/app_add_player_inventory_item.php";
    protected string _removeUserInventoryItem = "/WebServices/app_remove_player_inventory_item.php";
    protected string _getShopInventory = "/WebServices/app_get_shop_items.php";
    protected string _buyItem= "/WebServices/app_buy_item_with_gold.php";

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
                    
                    data = JSONObject.Parse(www.text);
                    if (data.GetNumber("success") == 1)
                    {
                        player_data = data;
                        GetUserInfo(player_data);
                        SetConnectionPanelMessage(data.GetString("message"));
                       
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

    public IEnumerator GetUserInventory()
    {
  
        string url = _host + _getUserItemInventory;
        WWWForm form = new WWWForm();
        form.AddField("uc_user_id", connected_user_id.ToString());

        
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            GetUserItems(JSONObject.Parse(www.text));
           
        }
        else
        {
            Debug.Log("Couldnt load inventory!!");
        }

    }


    public IEnumerator GetShopInventory()
    {

        string url = _host + _getShopInventory;
 


        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            GetShopItems(JSONObject.Parse(www.text));

        }
        else
        {
            Debug.Log(www.error.ToString());
        }

    }

    public IEnumerator GetUserData()
    {

        string url = _host + _getPlayerData;
        WWWForm form = new WWWForm();
        form.AddField("uc_user_id", connected_user_id.ToString());


        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            player_data = JSONObject.Parse(www.text);
            GetUserInfo(player_data);

        }
        else
        {
            Debug.Log("Couldnt load inventory!!");
        }

    }

    public IEnumerator GetCharacterEquipment(int character_id)
    {

        string url = _host + _getCharacterData;
        WWWForm form = new WWWForm();
        form.AddField("character_id", character_id.ToString());


        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            data = JSONObject.Parse(www.text);

        }
        else
        {
            Debug.Log("Couldnt load inventory!!");
        }

    }

    public IEnumerator BuyItemWithGold(string item_name, string item_type)
    {

        string url = _host + _buyItem;
        WWWForm form = new WWWForm();
        form.AddField("uc_user_id", connected_user_id.ToString());
        form.AddField("item_name", item_name.ToString());
        form.AddField("item_type", item_type.ToString());


        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {

        }
        else
        {
            Debug.Log("Couldnt buy inventory!!");
        }

    }

    public IEnumerator AddUserItem(string item_name, string item_type)
    {

        string url = _host + _addUserInventoryItem;
        WWWForm form = new WWWForm();
        form.AddField("uc_user_id", connected_user_id.ToString());
        form.AddField("item_name", item_name);
        form.AddField("item_type", item_type);


        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("item added!!");

        }
        else
        {
            Debug.Log("Couldnt add item!!");
        }

    }


    public IEnumerator RemoveUserItem( int item_id)
    {

        string url = _host + _removeUserInventoryItem;
        WWWForm form = new WWWForm();
        form.AddField("uc_user_id", connected_user_id.ToString());
        form.AddField("item_id", item_id.ToString());

        Debug.Log("azeza");
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("item removed!!");

        }
        else
        {
            Debug.Log("Couldnt remove item!!");
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


    void GetUserItems(JSONObject data)
    {
        Items.Clear();
        Skins.Clear();
        Crates.Clear();
        JSONArray itemInventory = data.GetArray("itemInventory");
        if(itemInventory != null)
        {
            foreach (var it in itemInventory)
            {
                InventoryItem item = new InventoryItem();
                item.ItemId = (int)it.Obj.GetNumber("inventory_item_id");
                item.Identity = it.Obj.GetString("item_name");
                item.Type = it.Obj.GetString("item_type");

                if (item.Type == "Item")
                {
                    ISItem AssetItem = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items.Find(i => i.Skins.Find(j => j.GetComponent<SkinInfo>().Identity == item.Identity) != null);
                    ISItem itemINV = new ISItem(AssetItem);
                    itemINV.SelectedSkin = itemINV.Skins[0];

                    item.Item = itemINV;


                    Items.Add(item);
                }

                else if (item.Type == "Skin")
                {

                    ISItem AssetItem = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items.Find(i => i.Skins.Find(j => j.GetComponent<SkinInfo>().Identity == item.Identity) != null);
                    ISItem itemINV = new ISItem(AssetItem);
                    itemINV.SelectedSkin = itemINV.Skins.Find(j => j.GetComponent<SkinInfo>().Identity == item.Identity);

                    item.Item = itemINV;
                    Skins.Add(item);
                }


                else if (item.Type == "Crate")
                {

                    ISItem AssetItem = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items.Find(i => i.Skins.Find(j => j.GetComponent<SkinInfo>().Identity == item.Identity) != null);
                    ISItem itemINV = new ISItem(AssetItem);
                    itemINV.SelectedSkin = itemINV.Skins[0];

                    item.Item = itemINV;


                    Crates.Add(item);
                }

            }
        }
        


    }


    void GetShopItems(JSONObject data)
    {
        ShopItems.Clear();
        ShopSkins.Clear();
        ShopCrates.Clear();
        JSONArray itemInventory = data.GetArray("shopInventory");
        if (itemInventory != null)
        {
            foreach (var it in itemInventory)
            {
                InventoryItem item = new InventoryItem();
                item.ItemId = (int)it.Obj.GetNumber("shop_item_id");
                item.Identity = it.Obj.GetString("shop_item_name");
                item.Type = it.Obj.GetString("shop_item_type");
                item.IsNew = (bool)it.Obj.GetBoolean("shop_item_is_new");
                item.Promo = (int) it.Obj.GetNumber("shop_item_promo");
                item.PriceGold = (int)it.Obj.GetNumber("shop_item_price_gold");
                item.PriceEuro = (float)it.Obj.GetNumber("shop_item_price_euro");

                if (item.Type == "Item")
                {
                    ISItem AssetItem = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items.Find(i => i.Skins.Find(j => j.GetComponent<SkinInfo>().Identity == item.Identity) != null);
                    ISItem itemINV = new ISItem(AssetItem);
                    itemINV.SelectedSkin = itemINV.Skins[0];

                    item.Item = itemINV;


                    ShopItems.Add(item);
                }

                else if (item.Type == "Skin")
                {

                    ISItem AssetItem = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items.Find(i => i.Skins.Find(j => j.GetComponent<SkinInfo>().Identity == item.Identity) != null);
                    ISItem itemINV = new ISItem(AssetItem);
                    itemINV.SelectedSkin = itemINV.Skins.Find(j => j.GetComponent<SkinInfo>().Identity == item.Identity);

                    item.Item = itemINV;
                    ShopSkins.Add(item);
                }


                else if (item.Type == "Crate")
                {

                    ISItem AssetItem = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items.Find(i => i.Skins.Find(j => j.GetComponent<SkinInfo>().Identity == item.Identity) != null);
                    ISItem itemINV = new ISItem(AssetItem);
                    itemINV.SelectedSkin = itemINV.Skins[0];

                    item.Item = itemINV;


                    ShopCrates.Add(item);
                }

            }
        }



    }

    void GetUserInfo(JSONObject data)
    {
        pp = (int)data.GetNumber("pp");
        pg = (int)data.GetNumber("pg");
        xp = (int)data.GetNumber("xp");
    }

    void GetCharacterData()
    {

    }

    public void LoadMenus()
    {
        if (GameObject.Find("InventoryMenu") != null)
            GameObject.Find("InventoryMenu").GetComponent<Inventory_Menu>().DisplayItems();

        if (GameObject.Find("CharacterMenu") != null)
            GameObject.Find("CharacterMenu").GetComponent<Character_Menu>().DisplayItems();


    }


}
