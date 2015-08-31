using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory_TabPanel : MonoBehaviour {

    public GameObject SkinsInventoryPanel;
    public GameObject ItemInventoryPanel;
    public GameObject CrateInventoryPanel;


    public Button SkinsTab;
    public Button ItemsTab;
    public Button CratesTab;

    public Button SelectedTab;

    // Use this for initialization
    void Start () {
        SkinsTab.onClick.AddListener(DisplaySkins);
        ItemsTab.onClick.AddListener(DisplayItems);
        CratesTab.onClick.AddListener(DisplayCrates);
        DisplayItems();

    }
	
	// Update is called once per frame
	void Update () {
	   
	}

    public void DisplaySkins()
    {
        SkinsInventoryPanel.SetActive(true);
        ItemInventoryPanel.SetActive(false);
        CrateInventoryPanel.SetActive(false);

        SelectedTab.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        SelectedTab = SkinsTab;
        SelectedTab.GetComponent<Image>().color = new Color(122, 122, 122, 255);
    }

    public void DisplayItems()
    {
        SkinsInventoryPanel.SetActive(false);
        ItemInventoryPanel.SetActive(true);
        CrateInventoryPanel.SetActive(false);

        SelectedTab.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        SelectedTab = ItemsTab;
        SelectedTab.GetComponent<Image>().color = new Color(122, 122, 122, 255);

    }

    public void DisplayCrates()
    {
        SkinsInventoryPanel.SetActive(false);
        ItemInventoryPanel.SetActive(false);
        CrateInventoryPanel.SetActive(true);

        SelectedTab.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        SelectedTab = CratesTab;
        SelectedTab.GetComponent<Image>().color = new Color(122, 122, 122, 255);

    }
}
