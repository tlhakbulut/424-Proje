using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListeHazırlama : MonoBehaviour
{
    // Start is called before the first frame update

    public TMPro.TMP_Text name_text_1;
    public TMPro.TMP_Text ssn_text_1;
    public TMPro.TMP_Text sıra_text_1;

    public TMPro.TMP_Text name_text_2;
    public TMPro.TMP_Text ssn_text_2;
    public TMPro.TMP_Text sıra_text_2;

    public TMPro.TMP_Text name_text_3;
    public TMPro.TMP_Text ssn_text_3;
    public TMPro.TMP_Text sıra_text_3;

    public TMPro.TMP_Text name_text_4;
    public TMPro.TMP_Text ssn_text_4;
    public TMPro.TMP_Text sıra_text_4;

    public TMPro.TMP_Text name_text_5;
    public TMPro.TMP_Text ssn_text_5;
    public TMPro.TMP_Text sıra_text_5;

    public TMPro.TMP_Text name_text_6;
    public TMPro.TMP_Text ssn_text_6;
    public TMPro.TMP_Text sıra_text_6;

    public TMPro.TMP_Text name_text_7;
    public TMPro.TMP_Text ssn_text_7;
    public TMPro.TMP_Text sıra_text_7;

    public TMPro.TMP_Text name_text_8;
    public TMPro.TMP_Text ssn_text_8;
    public TMPro.TMP_Text sıra_text_8;

    public Button SayfaDeğiştirmeButton;

    public int page = 1;
    //string[] names_list;
    //string[] surnames_list;
    //string[] ssns_list;

    void Start()
    {
        //string names = PlayerPrefs.GetString("names");
        //string surnames = PlayerPrefs.GetString("surnames");

        //names_list = names.Split('#');
        //surnames_list = surnames.Split('#');

        name_text_1.text = "Sevda Çetin";
        ssn_text_1.text = (PlayerPrefs.GetInt("ssn"+ "Sevda Çetin")).ToString();
        sıra_text_1.text = "1";

        name_text_2.text = "Elmas Samdereli";
        ssn_text_2.text = (PlayerPrefs.GetInt("ssn" + "Elmas Samdereli")).ToString();
        sıra_text_2.text = "2";

        name_text_3.text = "Açelya Yıldırım";
        ssn_text_3.text = (PlayerPrefs.GetInt("ssn" + "Açelya Yıldırım")).ToString();
        sıra_text_3.text = "3";

        name_text_4.text = "Sevinç Aydemir";
        ssn_text_4.text =  (PlayerPrefs.GetInt("ssn" + "Sevinç Aydemir")).ToString();
        sıra_text_4.text = "4";

        name_text_5.text = "Yıldırım Uğurlu";
        ssn_text_5.text = (PlayerPrefs.GetInt("ssn" + "Yıldırım Uğurlu")).ToString();
        sıra_text_5.text = "5";

        name_text_6.text = "Öykü Bal";
        ssn_text_6.text = (PlayerPrefs.GetInt("ssn" + "Öykü Bal")).ToString();
        sıra_text_6.text = "6";

        name_text_7.text = "Ceyda Durak";
        ssn_text_7.text = (PlayerPrefs.GetInt("ssn" + "Ceyda Durak")).ToString();
        sıra_text_7.text = "7";

        name_text_8.text = "Binnaz Taner";
        ssn_text_8.text = (PlayerPrefs.GetInt("ssn" + "Binnaz Taner")).ToString();
        sıra_text_8.text = "8";

        SayfaDeğiştirmeButton.onClick.AddListener(Sayfa);
    }

    public void Sayfa()
    {
        if(page == 1)
        {
            name_text_1.text = "Kuzey Tekinoğlu";
            ssn_text_1.text = PlayerPrefs.GetInt("ssn" + "Kuzey Tekinoğlu").ToString();
            sıra_text_1.text = "9";

            name_text_2.text = "Halil Birkan";
            ssn_text_2.text = PlayerPrefs.GetInt("ssn" + "Halil Birkan").ToString();
            sıra_text_2.text = "10";

            name_text_3.text = "Murat Taşdelen";
            ssn_text_3.text = PlayerPrefs.GetInt("ssn" + "Murat Taşdelen").ToString();
            sıra_text_3.text = "11";

            name_text_4.text = "Burçin Mumcu";
            ssn_text_4.text = PlayerPrefs.GetInt("ssn" + "Burçin Mumcu").ToString();
            sıra_text_4.text = "12";

            name_text_5.text = "Adnan Çetinkaya";
            ssn_text_5.text = PlayerPrefs.GetInt("ssn" + "Adnan Çetinkaya").ToString();
            sıra_text_5.text = "13";

            name_text_6.text = "Ahmet Demirci";
            ssn_text_6.text = PlayerPrefs.GetInt("ssn" + "Ahmet Demirci").ToString();
            sıra_text_6.text = "14";

            name_text_7.text = "Mücahit Köprülü";
            ssn_text_7.text = PlayerPrefs.GetInt("ssn" + "Mücahit Köprülü").ToString();
            sıra_text_7.text = "15";

            name_text_8.text = "Bilge Akyüz";
            ssn_text_8.text = PlayerPrefs.GetInt("ssn" + "Bilge Akyüz").ToString();
            sıra_text_8.text = "16";

            page = 2;
        }
        else
        {
            name_text_1.text = "Sevda Çetin";
            ssn_text_1.text = (PlayerPrefs.GetInt("ssn"+ "Sevda Çetin")).ToString();
            sıra_text_1.text = "1";

            name_text_2.text = "Elmas Samdereli";
            ssn_text_2.text = (PlayerPrefs.GetInt("ssn" + "Elmas Samdereli")).ToString();
            sıra_text_2.text = "2";

            name_text_3.text = "Açelya Yıldırım";
            ssn_text_3.text = (PlayerPrefs.GetInt("ssn" + "Açelya Yıldırım")).ToString();
            sıra_text_3.text = "3";

            name_text_4.text = "Sevinç Aydemir";
            ssn_text_4.text =  (PlayerPrefs.GetInt("ssn" + "Sevinç Aydemir")).ToString();
            sıra_text_4.text = "4";

            name_text_5.text = "Yıldırım Uğurlu";
            ssn_text_5.text = (PlayerPrefs.GetInt("ssn" + "Yıldırım Uğurlu")).ToString();
            sıra_text_5.text = "5";

            name_text_6.text = "Öykü Bal";
            ssn_text_6.text = (PlayerPrefs.GetInt("ssn" + "Öykü Bal")).ToString();
            sıra_text_6.text = "6";

            name_text_7.text = "Ceyda Durak";
            ssn_text_7.text = (PlayerPrefs.GetInt("ssn" + "Ceyda Durak")).ToString();
            sıra_text_7.text = "7";

            name_text_8.text = "Binnaz Taner";
            ssn_text_8.text = (PlayerPrefs.GetInt("ssn" + "Binnaz Taner")).ToString();
            sıra_text_8.text = "8";

            page = 1;
        }
    }
}
