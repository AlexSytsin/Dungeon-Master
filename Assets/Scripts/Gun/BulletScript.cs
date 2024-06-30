using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public int damage; // ���� ������

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        // ���������, ����������� �� �� � ������
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // �������� ��������� Enemy �� �������, � ������� �����������
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                // ������� ���� �����
                enemy.TakeDamage(damage);
            }

            
        }
        Destroy(gameObject);
    }

}
