using UnityEngine;

public class AreaCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            MakeDamage();
        }
    }

    private void MakeDamage()
    {
        Debug.Log("Ich hitte den Player");
    }
}
