using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public InteractPoint interactPoint;
    public ComicStore comicStore;
    public PlayerCollection playerCollection;
    public GameObject shoppingWindow;
    public GameObject shoppingPanel;
    public GameObject scrollview;
    public Button btnComputer;
    public Button btnNextDay;
    public AudioSource audioSource;
    public AudioClip singleClick;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        interactPoint = GameObject.Find("ComicPrepStation").GetComponent<InteractPoint>();
        comicStore = GameObject.Find("Main Camera").GetComponent<ComicStore>();
        playerCollection = GameObject.Find("Main Camera").GetComponent<PlayerCollection>();
        CloseComputer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenComputer()
    {
        audioSource.PlayOneShot(singleClick);
        shoppingWindow.GetComponent<Image>().enabled= true;
        shoppingPanel.SetActive(true);
        scrollview.SetActive(true);
        btnComputer.interactable = false;
    }

    public void CloseComputer()
    {
        shoppingWindow.GetComponent<Image>().enabled= false;
        shoppingPanel.SetActive(false);
        scrollview.SetActive(false);
        btnComputer.interactable = true;
    }

    public void NextDay()
    {
        comicStore.ResetShop();
        playerCollection.UpdateComicValues();
        interactPoint.AdvanceSentAwayComicDays();
    }
}
