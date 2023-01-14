using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float _userFrontBackInput;
    private float _userLeftRightInput;
    public float ScaleMovement = 5;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _userFrontBackInput = Input.GetAxis("Vertical");
        Debug.Log(_userFrontBackInput);

        _userLeftRightInput = Input.GetAxis("Horizontal");
        Debug.Log(_userLeftRightInput);

        gameObject.GetComponent<Transform>().position += transform.forward * _userFrontBackInput * ScaleMovement * Time.deltaTime;
        gameObject.GetComponent<Transform>().position += transform.right * _userLeftRightInput * ScaleMovement * Time.deltaTime;
    }
}
