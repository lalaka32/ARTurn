using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsPlayer", menuName = "Managers/SettingsPlayer")]
class SettingsPlayer :ManagerBase
{
    public bool ARCamera;
    public int numberOfRevisionQuestion { get; private set; }
    public void SetNumber(int index)
    {
        numberOfRevisionQuestion = index;
    }
}

