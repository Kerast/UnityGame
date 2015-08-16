using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TheGame.ItemSystem;
public class Character_ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public EquipmentSlot equipmentSlot;
    public GameObject ToolTipPanel;
    public GameObject StatListElementPrefab;

    private bool showTooltip = false;



    // Use this for initialization
    void Start()
    {

    }


    void Update()
    {
       
        if (showTooltip)
        {
            ToolTipPanel.transform.position = Input.mousePosition;
            ToolTipPanel.transform.Translate(new Vector3((ToolTipPanel.GetComponent<RectTransform>().rect.width / 4) + 10, (-ToolTipPanel.GetComponent<RectTransform>().rect.height / 4) - 10, 0));

        }

    }


    public void OnPointerEnter(PointerEventData data)
    {
        if(!showTooltip)
        {          
            ISItem equipedItem =  GetItemStats();

            foreach (var stat in equipedItem.Stats)
            {
                GameObject statElement = Instantiate(StatListElementPrefab);
                statElement.transform.GetChild(0).GetComponent<Text>().text = stat.Name;
                statElement.transform.GetChild(1).GetComponent<Text>().text = stat.ValueInt.ToString();
                statElement.transform.SetParent(ToolTipPanel.transform);
                statElement.transform.localScale = new Vector3(1, 1, 1);
                showTooltip = true;
            }
           

            showTooltip = true;
            ToolTipPanel.SetActive(true);

        }
       

    }

    public void OnPointerExit(PointerEventData data)
    {
        if (showTooltip)
        {
            for(int i = 0; i < ToolTipPanel.transform.childCount; i++)
            {
                Destroy(ToolTipPanel.transform.GetChild(i).gameObject);
            }
            ToolTipPanel.SetActive(false);
            showTooltip = false;
        }

            
    }


    public ISItem GetItemStats()
    {
        List<ISItem> PlayerItems = new List<ISItem>();
        Player_Equipment playerequipment = GameObject.Find("Player_Dummy").GetComponent<Player_Equipment>();
        PlayerItems.Add(playerequipment.Weapon);
        PlayerItems.Add(playerequipment.Helmet);
        PlayerItems.Add(playerequipment.Torse);
        PlayerItems.Add(playerequipment.Belt);
        PlayerItems.Add(playerequipment.ShoulderR);
        PlayerItems.Add(playerequipment.ShoulderL);
        PlayerItems.Add(playerequipment.GloveR);
        PlayerItems.Add(playerequipment.GloveL);
        PlayerItems.Add(playerequipment.Legs);

        foreach(var item in PlayerItems)
        {
            if (item.EquipmentSlot == equipmentSlot)
            {
                return item;
            }
        }

        return new ISItem();
    }

}
