using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Names : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        int ind = Random.Range(0, 10);

        string[] possible_name_female = {"Fatma", "Ayşe", "Emine", "Hatice", "Zeynep", "Elif", "Meryem", "Merve", "Zehra" , "Esra"};
        string[] possible_name_male = {"Mehmet", "Mustafa", "Ahmet", "Ali", "Hüseyin", "Hasan", "Murat", "İbrahim", "Yusuf" , "İsmail"};
        string[] possible_surnames = {"Yılmaz", "Kaya", "Demir", "Çelik" , "Şahin", "Arslan", "Koç", "Kurt", "Keskin", "Kara"};

        // Randomly select a name and surname from list to assign to the person

        string name = "";
        string surname = "";

        name = possible_name_female[ind];
        surname = possible_surnames[ind]; 

        string full_name = name + " " + surname;

        int ssn = (int) Random.Range(100000000, 999999999);

        // Save these values to playerprefs

        PlayerPrefs.SetString("name", full_name);
        PlayerPrefs.SetInt("ssn", ssn);

        // Repeat the process above 16 times

        string names = "";
        string surnames = "";
        int ssn1 = (int) Random.Range(100000000, 999999999);
        int ssn2 = (int) Random.Range(100000000, 999999999);
        int ssn3 = (int) Random.Range(100000000, 999999999);
        int ssn4 = (int) Random.Range(100000000, 999999999);
        int ssn5 = (int) Random.Range(100000000, 999999999);
        int ssn6 = (int) Random.Range(100000000, 999999999);
        int ssn7 = (int) Random.Range(100000000, 999999999);
        int ssn8 = (int) Random.Range(100000000, 999999999);
        int ssn9 = (int) Random.Range(100000000, 999999999);
        int ssn10 = (int) Random.Range(100000000, 999999999);
        int ssn11 = (int) Random.Range(100000000, 999999999);
        int ssn12 = (int) Random.Range(100000000, 999999999);
        int ssn13 = (int) Random.Range(100000000, 999999999);
        int ssn14 = (int) Random.Range(100000000, 999999999);
        int ssn15 = (int) Random.Range(100000000, 999999999);
        int ssn16 = (int) Random.Range(100000000, 999999999);

        for (int i = 0; i < 16; i++)
        {
            
            int ind2 = Random.Range(0,2);
            int ind3 = Random.Range(0,10);
            
            if(ind2 == 0)
            {   
                if ( i != 0 )
                {
                    names = names + "#" + possible_name_female[ind3];
                    surnames = surnames + "#" + possible_surnames[ind3];
                }
                else 
                {
                    names = possible_name_female[ind3];
                    surnames = possible_surnames[ind3];
                }
            } 
            else
            {
                if (i != 0)
                {
                    names = names + "#" + possible_name_male[ind3];
                    surnames = surnames + "#" + possible_surnames[ind3];
                }
                else
                {
                    names = possible_name_male[ind3];
                    surnames = possible_surnames[ind3];
                }
            }
        }

        PlayerPrefs.SetString("names", names);
        PlayerPrefs.SetString("surnames", surnames);

        PlayerPrefs.SetInt("ssn1", ssn1);
        PlayerPrefs.SetInt("ssn2", ssn2);
        PlayerPrefs.SetInt("ssn3", ssn3);
        PlayerPrefs.SetInt("ssn4", ssn4);
        PlayerPrefs.SetInt("ssn5", ssn5);
        PlayerPrefs.SetInt("ssn6", ssn6);
        PlayerPrefs.SetInt("ssn7", ssn7);
        PlayerPrefs.SetInt("ssn8", ssn8);
        PlayerPrefs.SetInt("ssn9", ssn9);
        PlayerPrefs.SetInt("ssn10", ssn10);
        PlayerPrefs.SetInt("ssn11", ssn11);
        PlayerPrefs.SetInt("ssn12", ssn12);
        PlayerPrefs.SetInt("ssn13", ssn13);
        PlayerPrefs.SetInt("ssn14", ssn14);
        PlayerPrefs.SetInt("ssn15", ssn15);
        PlayerPrefs.SetInt("ssn16", ssn16);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Load Sample Scene
            SceneManager.LoadScene("MasaScene");
        }
    }
}
