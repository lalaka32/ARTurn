using UnityEngine;
using Enums;

class PositionRotationAnimation
{
    public RuntimeAnimatorController Controller { get; set; }
    Vector3 Position { get; set; }
    Vector3 Rotation { get; set; }
    Position NumberOfPosition { get; set; }

    public PositionRotationAnimation(Vector3 position, Vector3 rotation,Position numberpos, RuntimeAnimatorController controller)
    {
        Controller = controller;
        Position = position;
        Rotation = rotation;
        NumberOfPosition = numberpos;
        
    }
    public void Apropriation(GameObject game)
    {
        game.name = NumberOfPosition.ToString();
        game.transform.localPosition = Position;
        game.transform.eulerAngles = Rotation;
        game.GetComponent<Car>().Position = NumberOfPosition;
        game.AddComponent<Animator>().runtimeAnimatorController = Controller;
    }
    
}
