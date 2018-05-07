using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BeeFly
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
        //обобщаем и накладываем ограничение чтобы Singleton наследовался
        //от движка
    {
        private static T _instance;//Глобальный доступ к этой переменной
        public static T Instance//Свойсво для поиска или создания
        {
            get
            {
                if(_instance == null)//Поиск
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)//Создание
                    {
                        var singleton = new GameObject("[SINGLETON]"+typeof(T));
                        _instance = singleton.AddComponent<T>();//Можем навешивать только из-за ограничения

                        DontDestroyOnLoad(singleton);// Если перейдём на новую сцену не произайдёт удаление Обьектов
                        //В Unity даже сцена делиться на 2 части: обычную и DontDestroyOnLoad
                    }
                }
                return _instance;// эти скрипты глобальные и одиночные, их будем использовать для менеджеров

            }

        }
    }
}
