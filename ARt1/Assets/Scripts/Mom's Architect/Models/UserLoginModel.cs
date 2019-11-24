using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UserLoginModel
{
    public string login;

    public string password;

    public UserLoginModel(string login, string password)
    {
        this.login = login;
        this.password = password;
    }
}

