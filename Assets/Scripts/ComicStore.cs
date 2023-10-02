using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicStore : MonoBehaviour
{
    public GenerateComic genComic;
    public GameObject slotPrefab;
    public float market;
    public List<Slot> slots = new List<Slot>();
    public List<Comic> storeComics = new List<Comic>();
    // Start is called before the first frame update

    private void Awake()
    {
        for(int i = 0; i < 20; i++)
        {
            GameObject instance = Instantiate(slotPrefab,new Vector3(0,0,12), Quaternion.identity,GameObject.Find("Content").transform);
            slots.Add(instance.GetComponentInChildren<Slot>());
            slots[i].comic = null;
        }
        RollMarket();
    }
    void Start()
    {
        for(int i = 0; i < 20; i++)
        {
            AddNewStoreComic(genComic.GenerateNewComic(market));
        }  
        
    }

    public void ResetShop()
    {
        RollMarket();
        for(int j = 0; j < 20; j++)
        {
            UpdateStoreComic(j,null);
        }
        for(int i = 0; i < 20; i++)
        {
            AddNewStoreComic(genComic.GenerateNewComic(market));
        }
    }

    public void RollMarket()
    {
        market = Random.Range(-0.05f,0.05f);
    }

    public void AddNewStoreComic(Comic comic)
    {
        UpdateStoreComic(slots.FindIndex( i => i.comic == null), comic);
    }

    public void RemoveStoreComic(Comic comic)
    {
        UpdateStoreComic(slots.FindIndex( i => i.comic == comic), null);
    }
    public void UpdateStoreComic(int slot, Comic comic)
    {
        slots[slot].UpdateComic(comic);
    }

    public double RePriceComic(double grade,bool graded,int key,int year)
    {
        double price = 0.00;
        double rarity = 0.0;
        double gradeMultiplier;

        if(year <= 1955)
        {
            price = 0.10;
            rarity = 0.112;
            if(key < 10)
            {
                rarity = 1.2;
            }
            else if(key < 100)
            {
                rarity = 0.23;
            }

        }
        else if(year <= 1970)
        {
            price = 0.15;
            rarity = .08;
            if(key < 10)
            {
                rarity = .26;
            }
            else if(key < 50)
            {
                rarity = .11;
            }

        }
        else if(year <= 1986)
        {
            price = 0.75;
            rarity = .01;
            if(key < 10)
            {
                rarity = .18;
            }
            else if(key < 50)
            {
                rarity = .05;
            }

        }
        else if(year <= 2023)
        {
            price = 2.50;
            rarity = .01256;
            if(key < 10)
            {
                rarity = .4;
            }
            else if(key < 50)
            {
                rarity = .062;
            }

        }
        

        if(grade == 10.0 || grade == 89.9)
        {
            if(graded)
                gradeMultiplier = 10; 
            else
                gradeMultiplier = 8;
        }
        else if(grade == 9.8)
        {
            if(graded)
                gradeMultiplier = 8; 
            else
                gradeMultiplier = 6;
        }
        else if(grade == 9.6)
        {
            if(graded)
                gradeMultiplier = 6; 
            else
                gradeMultiplier = 4;
        }
        else if(grade == 9.4)
        {
            if(graded)
                gradeMultiplier = 4; 
            else
                gradeMultiplier = 3;
        }
        else if(grade >= 7.0)
        {
            if(graded)
                gradeMultiplier = 3; 
            else
                gradeMultiplier = 2;
        }
        else if(grade >= 4.0)
        {
            if(graded)
                gradeMultiplier = 2; 
            else
                gradeMultiplier = 1.5;
        }
        else if(grade >= 2.0)
        {
            if(graded)
                gradeMultiplier = 1.5; 
            else
                gradeMultiplier = 1.25;
        }
        else
        {
            if(graded)
                gradeMultiplier = 1.15; 
            else
                gradeMultiplier = 1;
        }
        
        double newPrice = (year*(rarity*gradeMultiplier/price)/10)+(100*market);
        if(newPrice <= 0.0)
        {
            return 0.25;
        }
        else
        {
            return newPrice;
        }
    }
}
