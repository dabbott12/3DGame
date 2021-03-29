using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;
    public int value;

    private void Start()
    {
        if (CompareTag("YellowCoin"))
        {
            value = 1;
        }

        else if (CompareTag("RedCoin"))
        {
            value = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddScore(value);
            Destroy(gameObject);
        }
    }
}
