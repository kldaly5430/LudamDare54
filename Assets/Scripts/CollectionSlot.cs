using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionSlot : MonoBehaviour
{
    public Comic comic;
    public PlayerCollection playerCollection;
    public SpriteRenderer imgComic;
    public BoxCollider2D boxCol;

    // Start is called before the first frame update
    private void Awake()
    {
        playerCollection = GameObject.Find("Main Camera").GetComponent<PlayerCollection>();
        UpdateComicCollection(null);
    }
     private void Start() 
     {

     }

    public void UpdateComicCollection(Comic comic)
    {
        this.comic = comic;
        if(this.comic != null)
        {
            imgComic.GetComponent<SpriteRenderer>().enabled = true;
            boxCol.GetComponent<BoxCollider2D>().enabled = true;
            imgComic.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Comics/" + comic.genre + "/00" + comic.CoverNum);
        }
        else
        {
            imgComic.GetComponent<SpriteRenderer>().enabled = false;
            boxCol.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void OnComicClick()
    {
        // comicInteraction.AddComic(comic);
        playerCollection.InteractPlayerComic(comic);
        // UpdateComicCollection(null);
    }
}
