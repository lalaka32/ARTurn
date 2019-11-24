using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "DataGameSession", menuName = "Data/DataGameSession")]
class DataGameSession : ScriptableObject, IDisposable
    {
        public string Token { get; set; }
        public void CleanSession()
        {
            Token = null;
        }


        public void Dispose()
        {
            CleanSession();
        }
    
}

