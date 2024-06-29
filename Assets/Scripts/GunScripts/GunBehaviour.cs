using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // ����� ������ ����
    public float projectileSpeed; // �������� ����
    public Animator anim; // �������� ������
    public float fireRate=1f; // �������� �������� (��������� � �������)
    private float nextFireTime; // ����� ���������� ��������

    // ����� ��� ��������
    public void Shoot()
    {
        // ���������, ����� �� ��������
        if (Time.time > nextFireTime)
        {
            // ������� ����
            firePoint.rotation = Quaternion.Euler(firePoint.rotation.eulerAngles + new Vector3(0, 0, 270));
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // �������� ����������� � �������
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
            direction.z = 0; // ������� z-����������, ����� ���� ������ � 2D ���������

            // ����������� ���� ��������
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction.normalized * projectileSpeed;
            }


            // ��������� ����� ���������� ��������
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    // �����, ���������� ��� ���������� �������� ��������
    private void AnimationEvent_ShootFinished()
    {
        // ������� ����
        GameObject bullet = transform.GetChild(0).gameObject;
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }

    private void RotateGunTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // ������������� z-���������� ���� �� z-���������� ������� "gun"


        // ��������� ������ ����������� �� ������� "gun" � ����
        Vector3 direction = mousePosition - transform.position;

        // ��������� ���� �������� � ��������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ����������� ����, ���� ��� ��������� ����� ��� �����
        if (direction.x < 0.01f)
        {
            angle += 180f;
        }

        // ������������ ������ "gun"
        transform.rotation = Quaternion.Euler(0f, 0f, angle); // ������������ ������ �� ��� Z
    }


    // ����������
    void Update()
    {
        RotateGunTowardsMouse();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}