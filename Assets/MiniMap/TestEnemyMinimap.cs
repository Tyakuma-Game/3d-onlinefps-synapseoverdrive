using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMinimap : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float yPositionConstant = 0.0f;


    [SerializeField] GameObject iconObject;
    [SerializeField] Transform targetTransform;
    Vector3 miniMapPos;

    public void OnMinimap()
    {
        iconObject.SetActive(true);
    }
    public void NoMinimap()
    {
        iconObject.SetActive(false);
    }

    private void Update()
    {
        // �~�j�}�b�v��ł̈ʒu���v�Z
        miniMapPos = new Vector3(targetTransform.position.x, yPositionConstant, targetTransform.position.z);
        iconObject.transform.position = miniMapPos;
    }
}