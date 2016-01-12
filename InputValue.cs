using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputValue : MonoBehaviour
{
    private InputField inputField_A;
    private InputField inputField_B;
    private Slider slider_X;
    private Slider slider_Y;

    private Text[] text_A;
    private Text[] text_B;

    public float scale_A;
    public float scale_B;
    public float scale_X;
    public float scale_Y;
    public float lastV;

    private Parse_User _parse_User;

    void Start()
    {
        _parse_User = GameObject.Find("Parse_User").GetComponent<Parse_User>();
        scale_A = _parse_User.scale_A;
        scale_B = _parse_User.scale_B;
        scale_X = _parse_User.scale_X;
        scale_Y = _parse_User.scale_Y;
        lastV = _parse_User.numVButton;

        if (GameObject.Find("InputField_A"))
        {
            inputField_A = GameObject.Find("InputField_A").GetComponent<InputField>();
            text_A = inputField_A.GetComponentsInChildren<Text>(); 
        }

        if (GameObject.Find("InputField_B"))
        {
            inputField_B = GameObject.Find("InputField_B").GetComponent<InputField>();
            text_B = inputField_B.GetComponentsInChildren<Text>(); 
        }

        if (GameObject.Find("Slider_X"))
        {
            slider_X = GameObject.Find("Slider_X").GetComponent<Slider>();
            slider_X.value = scale_X;
        }

        if (GameObject.Find("Slider_Y"))
        {
           slider_Y = GameObject.Find("Slider_Y").GetComponent<Slider>();
           slider_Y.value = scale_Y;
        }

    }

    public void Input_X()
    {
        scale_X = slider_X.value;
    }

    public void Input_Y()
    {
        scale_Y = slider_Y.value;
    }
    
    void OnGUI()
    {
        if (inputField_A != null)
        {
            string Field_A = inputField_A.text;

            float newLength_A;
            if (float.TryParse(Field_A, out newLength_A))
            {

                text_A[1].color = Color.gray;

                if (inputField_A.isFocused && inputField_A.text != "" && Input.GetKey(KeyCode.Return))
                {
                    scale_A = newLength_A;
                }
            }
            else
            {
                text_A[1].color = Color.red;
            }
        }

        if (inputField_B != null)
        {
            string Field_B = inputField_B.text;

            float newLength_B;
            if (float.TryParse(Field_B, out newLength_B))
            {
                text_B[1].color = Color.gray;

                if (inputField_B.isFocused && inputField_B.text != "" && Input.GetKey(KeyCode.Return))
                {
                    scale_B = newLength_B;
                }
            }
            else
            {
                text_B[1].color = Color.red;
            }
        }

        //StartCoroutine(Parse_User.SaveValue(scale_A)); //???????? + lastV
    }

}
