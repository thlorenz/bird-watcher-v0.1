using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMove : MonoBehaviour {
  public float speed = 6.0f;
  public float gravity = -9.8f;
  const string Horizontal = "Horizontal";
  const string Vertical = "Vertical";

  CharacterController _characterController;

  void Start() {
    _characterController = GetComponent<CharacterController>();
  }

  void Update () {
    float deltaX = Input.GetAxis(Horizontal) * speed;
    float deltaZ = Input.GetAxis(Vertical) * speed;
    var movement = new Vector3(deltaX, 0, deltaZ);

    // limit diagonal movement to same speed as movement along access
    movement = Vector3.ClampMagnitude(movement, speed);
    movement.y = gravity;

    // independent of computer speed by multiplying with time passed
    // since last frame
    movement *= Time.deltaTime;

    // tranform movement vector from local to world space
    movement = transform.TransformDirection(movement);
    _characterController.Move(movement);
  }
}