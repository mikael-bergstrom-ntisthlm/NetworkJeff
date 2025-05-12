using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

  [SerializeField]
  float movementSpeed = 3f; // Unity-enheter per sekund

  [SerializeField]
  float rotationSpeed = 150f; // Grader per sekund

  [SerializeField]
  GameObject bulletPrefab;

  [SerializeField]
  Transform bulletSpawnPoint;

  [SerializeField]
  float timeBetweenShots = 0.5f;
  float timeSinceLastShot = 0f;

  Vector2 moveInput = new();

  void Update()
  {
    // VROOM VROOM

    float yRotation = moveInput.x * rotationSpeed * Time.deltaTime;
    float zMovement = moveInput.y * movementSpeed * Time.deltaTime;

    Vector3 movementVector = transform.forward * zMovement;

    GetComponent<CharacterController>().Move(movementVector);
    transform.Rotate(Vector3.up, yRotation);

    // BANG BANG DELAY
    if (timeSinceLastShot < timeBetweenShots) timeSinceLastShot += Time.deltaTime;
  }

  void OnMove(InputValue value)
  {
    moveInput = value.Get<Vector2>();
  }

  void OnFire()
  {
    // BANG BANG
    if (timeSinceLastShot > timeBetweenShots)
    {
      GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
      timeSinceLastShot = 0;
    }
  }
}
