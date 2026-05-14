using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private ObjectPool bulletPool;

    private Rigidbody _rb;
    private Camera _cam;
    private IWeapon _weapon;
    private HUDController _hud;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
        _weapon = new BaseGun(bulletPool);
        _hud = FindObjectOfType<HUDController>();
    }

    private void Update()
    {
        HandleRotation();

        if (Mouse.current.leftButton.wasPressedThisFrame)
            Shoot();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float h = Keyboard.current.dKey.isPressed ? 1f :
                  Keyboard.current.aKey.isPressed ? -1f : 0f;
        float v = Keyboard.current.wKey.isPressed ? 1f :
                  Keyboard.current.sKey.isPressed ? -1f : 0f;

        Vector3 dir = new Vector3(h, 0f, v).normalized;
        _rb.MovePosition(_rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f;
            if (lookDir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    private void Shoot()
    {
        if (firePoint == null) return;
        _weapon.Shoot(firePoint);
    }

    public void UpgradeToRapidFire()
    {
        _weapon = new RapidFireDecorator(_weapon);
        _hud?.ShowUpgradeAlert("RAPID FIRE x3 Faster Bullets!");
    }
}