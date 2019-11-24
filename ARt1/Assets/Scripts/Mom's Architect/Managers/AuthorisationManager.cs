using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AuthorisationManager", menuName = "Managers/AuthorisationManager")]
class AuthorisationManager : ManagerBase
{
    //string token = Toolbox.Get<DataGameSession>().Token;

    private string BASE_URL = "http://localhost:2054/api/Auth/";
    WebRequesting webRequesting;

    public void Register(InputField fieldLogin, InputField fieldPassword, InputField fieldConfirmedPassword)
    {
        webRequesting = GameObject.Find("WebRequesting").GetComponent<WebRequesting>();
        string userName = fieldLogin.text;
        string password = fieldPassword.text;
        string confirmPassword = fieldConfirmedPassword.text;

        UserRegistryModel registryModel = new UserRegistryModel(userName, password, confirmPassword);

        string jsonRegistryModel = JsonUtility.ToJson(registryModel);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonRegistryModel.ToCharArray());

        webRequesting.StartRegistration(BASE_URL + "Register", byteData);
    }

    public void Login (InputField fieldLogin, InputField fieldPassword)
    {
        webRequesting = GameObject.Find("WebRequesting").GetComponent<WebRequesting>();
        string userName = fieldLogin.text;
        string password = fieldPassword.text;

        UserLoginModel loginModel = new UserLoginModel(userName, password);

        string jsonLoginModel = JsonUtility.ToJson(loginModel);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonLoginModel.ToCharArray());

        webRequesting.StartLogin(BASE_URL + "Login", byteData);
    }

    internal void PutToken(string token)
    {
        token = JsonUtility.FromJson<TokenModel>(token).token;
        if (token != "")
        {

        }
        ToolBox.Get<DataGameSession>().Token = token;
        Debug.Log("token now is: " + ToolBox.Get<DataGameSession>().Token);
        ToolBox.Get<UIManager>().HideAuthorisation();
    }

    public void CheckAuth()
    {
        webRequesting = GameObject.Find("WebRequesting").GetComponent<WebRequesting>();

        //TokenModel tokenModel = new TokenModel(ToolBox.Get<DataGameSession>().Token);
        //string jsonTokenModel = JsonUtility.ToJson(tokenModel);
        //byte[] byteData = System.Text.Encoding.ASCII.GetBytes(jsonTokenModel.ToCharArray());

        //webRequesting.StartSendTest("http://localhost:2054/api/TestResult/Post", byteData);
    }
}

