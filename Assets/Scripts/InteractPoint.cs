using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractPoint : MonoBehaviour
{
    public Comic comic;
    public PlayerCollection playerCollection;
    public Player player;
    public ComicStore comicStore;

    public GameObject contentPanel;
    public SpriteRenderer imgComic;
    // public Image imgComic;
    public TextMeshProUGUI txtComicName;
    public TextMeshProUGUI txtComicIssueNo;
    public TextMeshProUGUI txtComicYear;
    public TextMeshProUGUI txtComicPrice;
    public TextMeshProUGUI txtComicGrade;
    public Image imgComicGraded;
    public Button btnClean;
    public Button btnGrade;
    public Button btnSell;

    public List<Comic> sentAwayComics = new List<Comic>();
    public List<int> days = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        comicStore = GameObject.Find("Main Camera").GetComponent<ComicStore>();
        player = GameObject.Find("txtPlayerMoney").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddComic(Comic comic)
    {
        if(this.comic != null && comic != null)
        {
            playerCollection.AddNewPlayerComic(this.comic);
        }
        this.comic = comic;
        if(this.comic != null)
        {
            if(contentPanel.activeInHierarchy == false)
            {
                contentPanel.SetActive(true);
                imgComic.enabled = true;
            }
            
            imgComic.sprite = Resources.Load<Sprite>("Comics/" + comic.genre + "/00" + comic.CoverNum);
            // imgComic.sprite = Resources.Load<Sprite>("Comics/" + comic.genre + "/00" + comic.CoverNum);
            txtComicName.text = comic.comicName;
            txtComicIssueNo.text = comic.issueNo.ToString();
            txtComicYear.text = comic.year.ToString();
            txtComicPrice.text = "$" + comic.price.ToString("F");
            txtComicGrade.text = comic.grade.ToString();
            if(comic.cleaned)
            {
                btnClean.enabled = false;
            }
            if(comic.graded || comic.grade == 10.0)
            {
                imgComicGraded.GetComponent<Image>().sprite = Resources.Load<Sprite>("greenCheck");
                btnGrade.enabled = false;
            }
            else
            {
                imgComicGraded.sprite = Resources.Load<Sprite>("redX");
                btnGrade.enabled = true;
            }

        }
        else
        {
            contentPanel.SetActive(false);
            imgComic.enabled = false;
        }
    }

    public void AdvanceSentAwayComicDays()
    {
        Debug.Log("Day advanced");
        if(days.Count != 0)
        {
            for(int i = 0; i < days.Count; i++)
            {
                days[i] = days[i]-1;
                if(days[i] == 0)
                {
                    playerCollection.AddNewPlayerComic(sentAwayComics[i]);
                    sentAwayComics.RemoveAt(i);
                    days.RemoveAt(i);
                }
                Debug.Log(days[i] + " " + i);
            }
        }
    }

    public void ClosePrep()
    {
        if(comic != null)
            playerCollection.AddNewPlayerComic(comic);
        AddComic(null);
        contentPanel.SetActive(false);
        imgComic.enabled = false;
    }

    public void SellComic()
    {
        player.money = player.money + comic.price;
        player.UpdateMoney(player.money);
        AddComic(null);
    }

    public void CleanComic()
    {
        player.money = player.money - 30;
        player.UpdateMoney(player.money);
        comic.cleaned = true;
        sentAwayComics.Add(comic);
        days.Add(3);
        AddComic(null);
    }

    public void GradeComic()
    {
        player.money = player.money - 60;
        player.UpdateMoney(player.money);
        comic.grade = ReGradeComic(comic.grade,comic.cleaned);
        comic.graded = true;
        comic.price = comicStore.RePriceComic(comic.grade,comic.graded, comic.rarity, comic.year);
        sentAwayComics.Add(comic);
        days.Add(3);
        AddComic(null);
    }

    public double ReGradeComic(double grade, bool cleaned)
    {
        int chance = Random.Range(1,100);
        int bump = Random.Range(1,10);
        if(cleaned != true)
        {
            //0.5 - 5.0 10% drop grade, 15% grade bump
            if(grade <= 5.0)
            {
                if(chance <= 10)
                {
                    grade = grade - 0.5 * bump;
                    if(grade <= 0)
                        grade = 0.5;
                    return grade;
                }
                else if(chance > 85)
                {
                    return grade + 0.5 * bump;
                }
                else
                {
                    return grade;
                }
            }
            //5.5 - 7.5 12% drop grade, 12% grade bump
            else if(grade <= 7.5)
            {
                if(chance <= 12)
                {
                    return grade - 0.5 * bump;
                }
                else if(chance > 88)
                {
                    return grade + 0.5 * bump;
                }
                else
                {
                    return grade;
                }
            }
            //8.0 - 9.0 15% drop grade, 10% grade bump
            else if(grade <= 9.0)
            {
                if(chance <= 15)
                {
                    return grade - 0.5 * bump;
                }
                else if(chance > 90)
                {
                    if(grade == 9.0 && bump == 2)
                    {
                        return 9.4;
                    }
                    else if(grade == 9.0 && bump == 1 || grade == 8.5 && bump == 2)
                    {
                        return 9.2;
                    }
                    else
                    {
                        return grade + 0.5 * bump;
                    }
                }
                else
                {
                    return grade;
                }
            }
            //9.2 - 9.6 20% drop grade, 5% grade bump
            else if(grade <= 9.6)
            {
                if(chance <= 20)
                {
                    return grade - 0.2;
                }
                else if(chance > 95)
                {
                    return grade + 0.2;
                }
                else
                {
                    return grade;
                }
            }
            //9.8 - 9.9 20% drop grade, 1% grade bump
            else if(grade <= 9.9)
            {
                if(chance <= 20)
                {
                    if(grade == 9.8)
                    {
                        return grade - 0.2;
                    }
                    else
                    {
                        return grade - 0.1;
                    }
                    
                }
                else if(chance > 99)
                {
                    
                    return grade + 0.1;
                }
                else
                {
                    return grade;
                }
            }
        }
        else if(cleaned == true)
        {
            //0.5 - 5.0 8% drop grade, 20% grade bump
            if(grade <= 5.0)
            {
                if(chance <= 8)
                {
                    grade = grade - 0.5 * bump;
                    if(grade <= 0)
                        grade = 0.5;
                    return grade;
                }
                else if(chance > 80)
                {
                    return grade + 0.5 * bump;
                }
                else
                {
                    return grade;
                }
            }
            //5.5 - 7.5 10% drop grade, 15% grade bump
            else if(grade <= 7.5)
            {
                if(chance <= 10)
                {
                    return grade - 0.5 * bump;
                }
                else if(chance > 85)
                {
                    return grade + 0.5 * bump;
                }
                else
                {
                    return grade;
                }
            }
            //8.0 - 9.0 13% drop grade, 12% grade bump
            else if(grade <= 9.0)
            {
                if(chance <= 13)
                {
                    return grade - 0.5 * bump;
                }
                else if(chance > 88)
                {
                    if(grade == 9.0 && bump == 2)
                    {
                        return 9.4;
                    }
                    else if(grade == 9.0 && bump == 1 || grade == 8.5 && bump == 2)
                    {
                        return 9.2;
                    }
                    else
                    {
                        return grade + 0.5 * bump;
                    }
                }
                else
                {
                    return grade;
                }
            }
            //9.2 - 9.6 18% drop grade, 7% grade bump
            else if(grade <= 9.6)
            {
                if(chance <= 18)
                {
                    return grade - 0.2;
                }
                else if(chance > 93)
                {
                    return grade + 0.2;
                }
                else
                {
                    return grade;
                }
            }
            //9.8 - 9.9 19% drop grade, 2% grade bump
            else if(grade <= 9.9)
            {
                if(chance <= 19)
                {
                    if(grade == 9.8)
                    {
                        return grade - 0.2;
                    }
                    else
                    {
                        return grade - 0.1;
                    }
                    
                }
                else if(chance > 98)
                {
                    
                    return grade + 0.1;
                }
                else
                {
                    return grade;
                }
            }
        }
        return grade;
    }
}
