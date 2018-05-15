using UnityEngine;
using Enums;

class PositionRotation
{
    public RuntimeAnimatorController Controller { get; set; }
    Vector3 Position { get; set; }
    Vector3 Rotation { get; set; }
    Position NumberOfPosition { get; set; }
    Vector3 Scale { get; set; }//Нада что-то и со скейлом тут сделать
    public PositionRotation(Vector3 position, Vector3 rotation,Position numberpos, RuntimeAnimatorController controller): this(position,rotation)
    {
        Controller = controller;
        NumberOfPosition = numberpos;
    }
    public PositionRotation(Vector3 position, Vector3 rotation)
    {
        Position = position;
        Rotation = rotation;
    }
    public void SetPR(GameObject GO)
    {
        GO.transform.localPosition = Position;
        GO.transform.eulerAngles = Rotation;
    }
    public void SetPRAatCar(GameObject GO)
    {
        GO.name = NumberOfPosition.ToString();
        SetPR(GO);
        GO.GetComponent<Car>().Position = NumberOfPosition;
        GO.GetComponentInChildren<Animator>().runtimeAnimatorController = Controller;
    }
    
}
