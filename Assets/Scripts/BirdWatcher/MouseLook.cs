using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
  public enum RotationAxes { MouseXAndY, MouseX, MouseY }
  public RotationAxes axes = RotationAxes.MouseXAndY;

  public float sensitivityHorizontal = 3.0f;
  public float sensitivityVertical = 3.0f;
  public float minimumVertical = -45.0f;
  public float maximumVertical = 45.0f;

  float _rotationX = 0;

  const string MouseX = "Mouse X";
  const string MouseY = "Mouse Y";

  void Start() {
    var body = GetComponent<Rigidbody>();
    if (body != null) body.freezeRotation = true;
  }

  void Update () {
    switch (axes) {
      case RotationAxes.MouseX: RotateX(); break;
      case RotationAxes.MouseY: RotateY(); break;
      case RotationAxes.MouseXAndY: RotateXAndY(); break;
    }
  }

  void RotateX() {
    transform.Rotate(0, Input.GetAxis(MouseX) * sensitivityHorizontal, 0);
  }

  void RotateY() {
    _rotationX -= Input.GetAxis(MouseY) * sensitivityVertical;
    _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);
    transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
  }

  void RotateXAndY() {
    _rotationX -= Input.GetAxis(MouseY) * sensitivityVertical;
    _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);

    float deltaY = Input.GetAxis(MouseX) * sensitivityHorizontal;
    float rotationY = transform.localEulerAngles.y + deltaY;
    transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
  }
}
