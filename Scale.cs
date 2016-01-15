using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scale : MonoBehaviour {

    public GameObject[] Cubes;

    private Parse_User parseUser;
    private InputValue inputValue;

    private GameObject inputField_A;
    private GameObject inputField_B;
    private GameObject slider_X;
    private GameObject slider_Y;

    void Awake () {
        parseUser = GameObject.Find("Parse_User").GetComponent<Parse_User>();
        inputValue = GameObject.Find("Canvas").GetComponent<InputValue>();

        inputField_A = GameObject.Find("InputField_A");
        inputField_B = GameObject.Find("InputField_B");
        slider_X = GameObject.Find("Slider_X");
        slider_Y = GameObject.Find("Slider_Y");

        switch (parseUser.numVButton)
        {
            case 1:
                Version_1();
                break;
            case 2:
                Version_2();
                break;
            case 3:
                Version_3();
                break;
        }
    }

    void Update()
    {
        if (parseUser.numVButton != 3)
        {
            Cubes[0].transform.localScale = new Vector3(inputValue.scale_A, inputValue.scale_A, 1f);
        }     
        Cubes[1].transform.localScale = new Vector3(inputValue.scale_B, inputValue.scale_B, 1f);
        Cubes[2].transform.localScale = new Vector3(inputValue.scale_X, inputValue.scale_X, 1f);
        Cubes[3].transform.localScale = new Vector3(inputValue.scale_Y, inputValue.scale_Y, 1f);
    }

    void Version_1()
    {
        Cubes[2].SetActive(false);
        Cubes[3].SetActive(false);
        slider_X.SetActive(false);
        slider_Y.SetActive(false);
    }

    void Version_2()
    {
        Cubes[3].SetActive(false);
        slider_Y.SetActive(false);
    }

    void Version_3()
    {
        Cubes[0].transform.localScale = new Vector3(parseUser.scale_A + parseUser.scale_B, parseUser.scale_A + parseUser.scale_B, 1f);
        Cubes[1].SetActive(false);
        inputField_A.SetActive(false);
        inputField_B.SetActive(false);
    }
    
}
