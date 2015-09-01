using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Networking.Match;

public class CustomPlayerLobby : NetworkLobbyPlayer {

    public GameObject CanvasReadyPrefab;
    public GameObject CanvasReady;
    public int playerCount = 1;
    bool checker= true;


    void Start()
    {
        playerCount = 1;
    }
    void Update()
    {
        if(isLocalPlayer)
        {
            
            if (GameObject.FindGameObjectsWithTag("PlayerLobby").Length == playerCount && checker)
            {
                if(CanvasReady != null)
                {
                    CanvasReady.SetActive(true);
                    DontDestroyOnLoad(CanvasReady);
                    StartCoroutine(Timer());
                    checker = false;
                }
            }
        }
    }

    IEnumerator Timer()
    {
       
        RectTransform rect = CanvasReady.transform.GetChild(4).GetComponent<RectTransform>();
        
        while (rect.rect.width > 0)
        {
            RectTransformExtensions.SetWidth(rect, rect.rect.width - 1);
            yield return new WaitForSeconds(0.05f);
        }
        checker = true;
        Debug.Log("bbbb");
        Destroy(CanvasReady.gameObject);
        if (!GetComponent<NetworkLobbyPlayer>().readyToBegin)
        {
            CmdDestroyMatchAndHost();
        }


    }

    public void OnCaca(BasicResponse res)
    {

    }


    public override void OnStartLocalPlayer()
    {

        CanvasReady = Instantiate(CanvasReadyPrefab);
        CanvasReady.SetActive(false);
        CanvasReady.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(SetReadyPlayer);
       


    }

    
    public void SetReadyPlayer()
    {
        if (isLocalPlayer)
        {

            StopAllCoroutines();
            GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
            CanvasReady.transform.GetChild(2).GetComponent<Button>().image.color = new Color(51,255,0,255) ;

        }

        

    }

    

    [Command]
    public void CmdDestroyMatchAndHost()
    {
        if(isServer)
        {
                       
            NetworkManager.singleton.StopHost();
            CustomNetworkLobbyManager temp = GameObject.Find("CustomNetworkLobbyManager").GetComponent<CustomNetworkLobbyManager>();
            temp.networkMatch.DestroyMatch(temp.actualMatch.networkId, OnCaca);
            
        }
        
    }









}



public static class RectTransformExtensions
{
    public static void SetDefaultScale(this RectTransform trans)
    {
        trans.localScale = new Vector3(1, 1, 1);
    }
    public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
    {
        trans.pivot = aVec;
        trans.anchorMin = aVec;
        trans.anchorMax = aVec;
    }

    public static Vector2 GetSize(this RectTransform trans)
    {
        return trans.rect.size;
    }
    public static float GetWidth(this RectTransform trans)
    {
        return trans.rect.width;
    }
    public static float GetHeight(this RectTransform trans)
    {
        return trans.rect.height;
    }

    public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
    }

    public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }
    public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }
    public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }
    public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }

    public static void SetSize(this RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }
    public static void SetWidth(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(newSize, trans.rect.size.y));
    }
    public static void SetHeight(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }
}