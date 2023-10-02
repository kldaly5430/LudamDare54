using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Comic comic;
    public PlayerCollection playerCollection;
    public Player player;
    public GameObject UIContainer;
    public GameObject comicPanelBack;
    public Image imgComic;
    public TextMeshProUGUI txtComicName;
    public TextMeshProUGUI txtComicPrice;
    public TextMeshProUGUI txtComicGrade;
    public Button btnBuy;
    // Start is called before the first frame update
    private void Awake() {
        playerCollection = GameObject.Find("Main Camera").GetComponent<PlayerCollection>();
        player = GameObject.Find("txtPlayerMoney").GetComponent<Player>();
        UpdateComic(null);
    }

    // Update is called once per frame
    public void UpdateComic(Comic comic)
    {
        this.comic = comic;
        if(this.comic != null)
        {
            comicPanelBack.GetComponent<Image>().enabled = true;
            UIContainer.SetActive(true);
            imgComic.sprite = Resources.Load<Sprite>("Comics/" + comic.genre + "/00" + comic.CoverNum);
            txtComicName.text = comic.comicName + " - #" + comic.issueNo;
            txtComicPrice.text = "$" + comic.price.ToString("F");
            txtComicGrade.text = comic.grade.ToString();
            if(comic.price > player.money || playerCollection.numOfComics == 20)
            {
                btnBuy.interactable = false;
            }
            else
            {
                btnBuy.interactable = true;
            }
            // Debug.Log(comic.CoverNum);
        }
        else
        {
            comicPanelBack.GetComponent<Image>().enabled = false;
            txtComicName.text = "";
            txtComicPrice.text = "";
            txtComicGrade.text = "";
            btnBuy.interactable = false;
            UIContainer.SetActive(false);
        }
    }

    public void btnBuyComic()
    {
        player.money = player.money - comic.price;
        player.UpdateMoney(player.money);
        playerCollection = GameObject.Find("Main Camera").GetComponent<PlayerCollection>();
        playerCollection.AddNewPlayerComic(comic);
        UpdateComic(null);
    }
}
