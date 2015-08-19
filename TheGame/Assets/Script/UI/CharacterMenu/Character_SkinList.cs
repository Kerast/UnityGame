using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character_SkinList : MonoBehaviour
{

    public GameObject Skin;
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeItemSkin);
    }

    public void ChangeItemSkin()
    {
        GameObject item = GameObject.FindGameObjectWithTag("SelectedItem_ItemsListContent");
        item.GetComponent<Character_ItemList>().Item.SelectedSkin = Skin;


      



        //MODEL 3D DU PANNEAU
        GameObject ItemPreview = GameObject.Find("ItemPreview");


        for (int i = 0; i < ItemPreview.transform.childCount; i++)
        {
            Destroy(ItemPreview.transform.GetChild(i).gameObject);
        }

        GameObject temp = Instantiate(Skin);
        temp.transform.SetParent(ItemPreview.transform);
        temp.transform.position = ItemPreview.transform.position;
        temp.transform.rotation = ItemPreview.transform.rotation;
        temp.transform.localScale = new Vector3(1, 1, 1);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
