using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShapeExact : MonoBehaviour
{
    [SerializeField] private List<GameObject> groupShapeCharacterConfirm = new List<GameObject>();
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            foreach (Transform child in other.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    foreach (GameObject item in groupShapeCharacterConfirm)
                    {
                        if (child.gameObject == item)
                        {
                            if (Vector3.Distance(other.transform.position, transform.position) < 0.2f)
                            {
                                var character = other.transform.GetComponent<Factory>();
                                character.ChangeState(CharacterState.Complete);
                                other.transform.gameObject.SetActive(false);
                                transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                            }
                        }
                    }
                }
            }
        }
    }
}
