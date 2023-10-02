using UnityEngine;


public class Comic
{
    public Sprite cover;
    public int genre;
    public string comicName;
    public int issueNo;
    public double price;
    public double grade;
    public int rarity;
    public bool graded;
    public bool cleaned;
    public int year;
    public int CoverNum;

    public Comic(int genre,string comicName,int issueNo,double price,double grade,int rarity,bool graded,bool cleaned,int year,int CoverNum)
    {
        this.genre = genre;
        this.comicName = comicName;
        this.issueNo = issueNo;
        this.price = price;
        this.grade = grade;
        this.rarity = rarity;
        this.graded = graded;
        this.cleaned = cleaned;
        this.year = year;
        this.CoverNum = CoverNum;
        this.cover = Resources.Load<Sprite>("Comics/" + genre + "/00" + CoverNum);
    }

    public Comic(Comic comic)
    {
        this.genre = comic.genre;
        this.comicName = comic.comicName;
        this.issueNo = comic.issueNo;
        this.price = comic.price;
        this.grade = comic.grade;
        this.rarity = comic.rarity;
        this.graded = comic.graded;
        this.cleaned = comic.cleaned;
        this.year = comic.year;
        this.CoverNum = comic.CoverNum;
        this.cover = comic.cover;
    }

}
