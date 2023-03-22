using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadManager : MonoBehaviour
{
    [SerializeField] float vel;
    [SerializeField] Renderer quad;

    private void Update()
    {
        Vector2 offset = new Vector2(vel * Time.deltaTime, 0);
        quad.material.mainTextureOffset += offset;
    }
}
