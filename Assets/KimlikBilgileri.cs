using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimlikBilgileri : MonoBehaviour
{
    public TMPro.TMP_Text name_text;
    public TMPro.TMP_Text ssn_text;
    public GameObject KimlikKart;

    // Start is called before the first frame update
    void Start()
    {
        // Get the name and ssn from playerprefs
        string name = UnityEngine.PlayerPrefs.GetString("current_voter_name");
        int ssn = UnityEngine.PlayerPrefs.GetInt("current_voter_ssn");
        int istherevoter = UnityEngine.PlayerPrefs.GetInt("istherevoter");

        if (istherevoter == 1)
        {
            KimlikKart.SetActive(true);
        }
        else
        {
            KimlikKart.SetActive(false);
        }

        // Set the text of the text objects
        name_text.text = name;
        ssn_text.text = ssn.ToString();
    }
}
