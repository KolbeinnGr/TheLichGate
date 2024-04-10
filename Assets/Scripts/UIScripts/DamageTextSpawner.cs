using UnityEngine;
using TMPro;

public class DamageTextSpawner : MonoBehaviour
{
   public GameObject damageTextPrefab;
   
   public void SpawnDamageText(float damage, Vector3 position, Color textColor = default)
   {
       GameObject textObj = Instantiate(damageTextPrefab, position, Quaternion.identity, transform);
       var textMesh = textObj.GetComponent<TextMeshPro>();
       textMesh.text = damage.ToString();
       textMesh.color = textColor;
   }
}
