using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;
using UnityEngine.UI;

public class Parse_User : MonoBehaviour
{
    public int numVButton;
    public float scale_A;
    public float scale_B;
    public float scale_X;
    public float scale_Y;
    public float lastV; 

    public InputField InputField;
    private AudioSource _audio;
    private string _username = "";
    private bool isFirst = true;

    void Start()
    {   /*
        if (ParseUser.CurrentUser != null)
        {
            ParseUser.LogOut();
        }*/
   
        DontDestroyOnLoad(this);

        _audio = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }
    private IEnumerator SignUp(string _username)
    {
        var user = new ParseUser()
        {
            Username = _username,
            Password = _username,
            Email = _username + "@gmail.com"
        };
        
        var task = user.SignUpAsync();

        while (!task.IsCompleted && !task.IsFaulted)
            yield return null;
        Debug.Log("User registered! Log in...");

        scale_A = 1f;
        scale_B = 1f;
        scale_X = 1f;
        scale_Y = 1f;
        lastV = 1f;
    }

    private IEnumerator Login(string _username)
    {
        isFirst = false;
        var loginTask = ParseUser.LogInAsync(_username, _username);

        while (!loginTask.IsCompleted && !loginTask.IsFaulted)
            yield return null; 

        if (loginTask.IsFaulted || loginTask.IsCanceled)
        {
            // The login failed. 
            Debug.Log("id not found, creating new id");
            StartCoroutine(SignUp(_username));
        }
        else
        {
            // Login was successful.
            ParseUser user = loginTask.Result;
         
            Debug.Log("Successfully logged in as user " + user.Username);

            scale_A = user.Get<float>("scale_A");
            scale_B = user.Get<float>("scale_B");
            scale_X = user.Get<float>("scale_X");
            scale_Y = user.Get<float>("scale_Y");
            lastV = user.Get<float>("lastV");
        }
    }

    public  IEnumerator SaveValue(float newScaleA, float newScaleB, float newScaleX, float newScaleY, int currentV)
    {
        var user = ParseUser.CurrentUser;
        print(user.Username);
        user["scale_A"] = newScaleA;
        user["scale_B"] = newScaleB;
        user["scale_X"] = newScaleX;
        user["scale_X"] = newScaleY;
        user["lastV"] = currentV;

        var userSaveTask = user.SaveAsync();

        while (!userSaveTask.IsCompleted)
            yield return null;

        Debug.Log("New value saved!");

    }

    void OnGUI()
    {
        if (InputField.isFocused && InputField.text != "" && Input.GetKey(KeyCode.Return) && isFirst)
        {
            _username = InputField.text;
            StartCoroutine(Login(_username));
        }
    }

    public void VersionButton(int n)
    {
        _audio.Play();
        numVButton = n;

        if ( numVButton >= lastV )
        {
            //if (ParseUser.CurrentUser != null) //???
            if (_username != "")
            {
                Application.LoadLevel(1);
            }
            else
            {
                Debug.Log("First, Enter your ID");
            }
        }
        else
        {
            Debug.Log("Only new version");
        }

    }
}
