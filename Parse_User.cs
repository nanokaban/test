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
    private string username = "";
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

    private void SignUp(string username)
    {
        ParseUser user = new ParseUser()
        {
            Username = username,
            Password = username,
        };

        user.SignUpAsync().ContinueWith(signUpTask =>
        {
            if (signUpTask.IsFaulted || signUpTask.IsCanceled)
            {
                // The signUp failed. 
                Debug.Log("Sign up failed. This might be because: " + signUpTask.Exception.Message);
            }
            else
            {
                // signUp was successful.
                Debug.Log("New user registered!");

                user["scale_A"] = 1;
                user["scale_B"] = 1;
                user["scale_X"] = 1;
                user["scale_Y"] = 1;
                user["lastV"] = 1;
                user.SaveAsync();
            }
        });
    }

    private void Login(string username)
    {
        isFirst = false;
        ParseUser.LogInAsync(username, username).ContinueWith(loginTask =>
        {
            if (loginTask.IsFaulted || loginTask.IsCanceled)
            {
                // The login failed. 
                Debug.Log("ID not found, creating new ID");
                SignUp(username);
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
        });
    }

    public void SaveValue(string s, float newValue)
    {
        var user = ParseUser.CurrentUser;
        user[s] = newValue;

        var SaveTask = user.SaveAsync();

        if (SaveTask.IsFaulted || SaveTask.IsCanceled)
        {
            Debug.Log("Save failed. This might be because: " + SaveTask.Exception.Message);
        }
        else
        {
            Debug.Log("New value saved!");
        }
    }

    void OnGUI()
    {
        if (InputField.isFocused && InputField.text != "" && Input.GetKey(KeyCode.Return) && isFirst)
        {
            username = InputField.text;
            Login(username);
        }
    }

    public void VersionButton(int n)
    {
        _audio.Play();
        numVButton = n;

        if ( numVButton >= lastV )
        {
            //if (ParseUser.CurrentUser != null) ???
            if (username != "")
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
            Debug.Log("Error: you can not use an older version");
        }

    }
}
