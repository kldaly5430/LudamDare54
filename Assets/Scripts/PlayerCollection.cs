using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    public GenerateComic genComic;
    public ComicStore comicStore;
    public GameObject slotPrefab;
    public int numOfComics = 0;
    public List<CollectionSlot> slots = new List<CollectionSlot>();
    public InteractPoint point;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < 20; i++)
        {
            if(i <= 9)
            {
                GameObject instance = Instantiate(slotPrefab,new Vector3(-6.5f,1.0f-(i*0.4f),1), Quaternion.identity,GameObject.Find("CollectorBoxLeft").transform);//0.4f
                slots.Add(instance.GetComponentInChildren<CollectionSlot>());
                slots[i].comic = null;
                slots[i].GetComponent<SpriteRenderer>().sortingOrder = i+1;
            }
            else if(i <= 19)
            {
               GameObject instance = Instantiate(slotPrefab,new Vector3(-3.6f,4.8f-(i*0.4f),1), Quaternion.identity,GameObject.Find("CollectorBoxRight").transform);
                slots.Add(instance.GetComponentInChildren<CollectionSlot>());
                slots[i].comic = null;
                slots[i].GetComponent<SpriteRenderer>().sortingOrder = i-9; 
            }
            
        }
        genComic = GetComponent<GenerateComic>();
        point = GameObject.Find("ComicPrepStation").GetComponent<InteractPoint>();
        comicStore = GameObject.Find("Main Camera").GetComponent<ComicStore>();
    }

    void Start()
    {
        // storeComics.Add(genComic.GenerateNewComic());
        // AddNewPlayerComic(genComic.GenerateNewComic());
    }

    public void AddNewPlayerComic(Comic comic)
    {
        UpdatePlayerComic(slots.FindIndex( i => i.comic == null), comic);
        numOfComics++;
    }

    public void RemovePlayerComic(Comic comic)
    {
        UpdatePlayerComic(slots.FindIndex( i => i.comic == comic), null);
        numOfComics--;
    }
    public void UpdatePlayerComic(int slot, Comic comic)
    {
        // Debug.Log(comic.comicName + " " + slot);
        slots[slot].UpdateComicCollection(comic);
    }
    public void InteractPlayerComic(Comic comic)
    {
        point.AddComic(comic);
        RemovePlayerComic(comic);
    }
    public void UpdateComicValues()
    {
        for(int i = 0; i < 20;i++)
        {
            if(slots[i].comic != null)
            {
                slots[i].comic.price = comicStore.RePriceComic(slots[i].comic.grade,slots[i].comic.graded,slots[i].comic.rarity,slots[i].comic.year);
                slots[i].UpdateComicCollection(slots[i].comic);
            }
        }
    }
}
