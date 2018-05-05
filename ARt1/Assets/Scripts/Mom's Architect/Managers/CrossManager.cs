using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    [CreateAssetMenu(fileName = "CrossManager", menuName = "Managers/CrossManager")]
    class CrossManager:ManagerBase
    {
        //Тута нада Создац керекрёсток
        //ВОзможно лучши сделать 1 манагер по созданию всего
        //но я не уверен...
        public GameObject prefabOfCross;
        public void SetCrossGO(Vector3 pos)
        {
            Instantiate(prefabOfCross, pos, Quaternion.identity);
        }
        public void OnAwake()
        {
            
        }
    }

