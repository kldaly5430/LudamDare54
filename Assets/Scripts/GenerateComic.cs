using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;

public class GenerateComic : MonoBehaviour
{

    private double[] comicGrade = new double[]{0.5,1.0,1.5,2.0,2.5,3.0,3.5,4.0,4.5,5.0,5.5,6.0,6.5,7.5,8.0,8.5,9.0,9.2,9.4,9.6,9.8,9.9,10.0};
    public TextAsset horrorWordsText;
    public TextAsset mysteryWordsText;
    public TextAsset adventureWordsText;
    public TextAsset scifiWordsText;
    public TextAsset childWordsText;
    public TextAsset superheroWordsText;
    public string horrorWords;
    public string mysteryWords;
    public string adventureWords;
    public string scifiWords;
    public string childWords;
    public string superheroWords;
    private List<string> horror = new List<string>();
    private List<string> mystery = new List<string>();
    private List<string> adventure = new List<string>();
    private List<string> scifi = new List<string>();
    private List<string> children = new List<string>();
    private List<string> superhero = new List<string>();
    public Comic comic;
    public ComicStore comicStore;
    // Start is called before the first frame update
    void Awake()
    {
        comicStore = GameObject.Find("Main Camera").GetComponent<ComicStore>();
        horrorWords = horrorWordsText.text;
        mysteryWords = mysteryWordsText.text;
        adventureWords = adventureWordsText.text;
        scifiWords = scifiWordsText.text;
        childWords = childWordsText.text;
        superheroWords = superheroWordsText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Comic GenerateNewComic(float market)
    {
        int genre;
        string comicName;
        int issueNo;
        double price;
        double grade;
        int rarity;
        bool graded;
        bool cleaned;
        int year;
        int CoverNum = Random.Range(1,6);
        
        genre = Random.Range(0,5);
        comicName = GenerateTitle(genre);
        issueNo = Random.Range(1,500);
        grade = comicGrade[Random.Range(1,23)];
        rarity = Random.Range(1,10000);
        graded = false;
        cleaned = false;
        year = Random.Range(1938,2023);
        price = GeneratePrice(grade,year,rarity,market);
        
        comic = new Comic(genre,comicName, issueNo, price, grade,rarity, graded, cleaned, year, CoverNum);

        return comic;
    }

        public string GenerateTitle(int genre)
    {
        if(genre == 0)
        {
            string[] wordArray = horrorWords.Split('\r');
            foreach(var word in wordArray)
            {
                horror.Add(word);
            }
            
            string horrorTitle = "";
            for (int i = 0; i < 2; i++)
            {
                int randomIndex = Random.Range(0,horror.Count);
                horrorTitle += horror[randomIndex] + " ";
                horrorTitle = horrorTitle.Replace("\n","");
            }
            
            return horrorTitle;
        }
        else if(genre == 1)
        {
            string[] wordArray = mysteryWords.Split('\r');
            foreach(var word in wordArray)
            {
                mystery.Add(word);
            }
            
            string mysteryTitle = "";
            for (int i = 0; i < 2; i++)
            {
                int randomIndex = Random.Range(0,mystery.Count);
                mysteryTitle += mystery[randomIndex] + " ";
                mysteryTitle = mysteryTitle.Replace("\n","");
            }
            
            return mysteryTitle;
        }
        else if(genre == 2)
        {
            string[] wordArray = adventureWords.Split('\r');
            foreach(var word in wordArray)
            {
                adventure.Add(word);
            }
            
            string adventureTitle = "";
            for (int i = 0; i < 2; i++)
            {
                int randomIndex = Random.Range(0,adventure.Count);
                adventureTitle += adventure[randomIndex] + " ";
                adventureTitle = adventureTitle.Replace("\n","");
            }
            
            return adventureTitle;
        }
        else if(genre == 3)
        {
            string[] wordArray = scifiWords.Split('\r');
            foreach(var word in wordArray)
            {
                scifi.Add(word);
            }
            
            string scifiTitle = "";
            for (int i = 0; i < 2; i++)
            {
                int randomIndex = Random.Range(0,scifi.Count);
                scifiTitle += scifi[randomIndex] + " ";
                scifiTitle = scifiTitle.Replace("\n","");
            }
            
            return scifiTitle;
        }
        else if(genre == 4)
        {
            string[] wordArray = childWords.Split('\r');
            foreach(var word in wordArray)
            {
                children.Add(word);
            }
            
            string childrenTitle = "";
            for (int i = 0; i < 2; i++)
            {
                int randomIndex = Random.Range(0,children.Count);
                childrenTitle += children[randomIndex] + " ";
                childrenTitle = childrenTitle.Replace("\n","");
            }
            
            return childrenTitle;
        }
        else if(genre == 5)
        {
            string[] wordArray = superheroWords.Split('\r');
            foreach(var word in wordArray)
            {
                superhero.Add(word);
            }
            
            string superheroTitle = "";
            for (int i = 0; i < 2; i++)
            {
                int randomIndex = Random.Range(0,superhero.Count);
                superheroTitle += superhero[randomIndex] + " ";
                superheroTitle = superheroTitle.Replace("\n","");
            }
            
            return superheroTitle;
        }
        return "";
    }

    public double GeneratePrice(double grade, int year, int key,float market)
    {
        double price = 0.00;
        double rarity = 0.0;
        double gradeMultiplier;
        // int key = Random.Range(1,10000);

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
            gradeMultiplier = 8;
        }
        else if(grade == 9.8)
        {
            gradeMultiplier = 6;
        }
        else if(grade == 9.6)
        {
            gradeMultiplier = 4;
        }
        else if(grade == 9.4)
        {
            gradeMultiplier = 3;
        }
        else if(grade >= 7.0)
        {
            gradeMultiplier = 2;
        }
        else if(grade >= 4.0)
        {
            gradeMultiplier = 1.5;
        }
        else if(grade >= 2.0)
        {
            gradeMultiplier = 1.25;
        }
        else
        {
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
