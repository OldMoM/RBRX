using Entitas;
using UnityEngine;

public class VelocityCompnent:IComponent
{
    public Vector2 velocity;
    public float speed = 4;
    public float jumpHeight = 3;
    public Vector2 face = Vector2.right;
    public float sprintSpeed = 18;
    public float sprintAcc = -45;
    public float gravity = -9.81f;
}
