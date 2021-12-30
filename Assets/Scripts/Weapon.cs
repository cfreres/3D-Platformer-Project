using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isActive = false;
    public Transform playerHandHold;
    public int damage = 100;

    public CharacterAnimator cAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void PickupItem(Collider player)
    {
        isActive = true;
        transform.SetParent(playerHandHold);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("Collision");
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {           
            PickupItem(collision);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && isActive && cAnimator.isSwinging)
        {
            other.GetComponent<EnemyController>().health -= damage;
        }
    }
}
