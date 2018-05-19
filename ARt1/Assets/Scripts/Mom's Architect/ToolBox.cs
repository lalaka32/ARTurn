using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeeFly;
using System;

public class ToolBox :Singleton<ToolBox> {

    //Инкапсулированная структура данных для хранения систем
    private Dictionary<Type, object> data = new Dictionary<Type, object>();

    //Добавление элементов
    public static void Add(object obj)
    {
        var add = obj;
        ManagerBase managerTemp = obj as ManagerBase;

        if (managerTemp != null)
            add = Instantiate(managerTemp);//копируем скриптабл обжект со всеми настройками
        //У этих скриптабл ести такая харк-ка они сохраняют данные после того как
        // сцена закончилась.

        else
            return;
        //Объект не менеджер то 

        Instance.data.Add(obj.GetType(), managerTemp);
        //Типо this только метод то статический

        if (add is IAwake)//Возможность подрубать какую-то
                          //логику на стадии добавления в наш список
            (add as IAwake).OnAwake();

    }
    
    //Считывание
    public static T Get<T>()
    {
        object resolve;
        Instance.data.TryGetValue(typeof(T),out resolve);
        return (T)resolve;
    }
    public void ClearScene()//Проблема в том что т.к. Тулбокс это
        //не уничтожаемый объект то он сохранит всю дату с
        //менеджеров и будет куча ошибок нужно удалитб весь список
        //менеджеров
    {

    }
}
