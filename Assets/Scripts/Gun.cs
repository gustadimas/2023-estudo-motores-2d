using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet, gun;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            gun.SetActive(true);
            Instantiate(bullet, new Vector2(gun.transform.position.x, gun.transform.position.y), gun.transform.rotation);
        }
    }
}