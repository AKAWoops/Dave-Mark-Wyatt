using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Searchable : MonoBehaviour
{
    public float TimeToSearch;
    public GameObject SearchUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SearchUI.GetComponentInChildren<Slider>().maxValue = TimeToSearch;
            SearchUI.GetComponentInChildren<Slider>().value = TimeToSearch;
            float Dot = Vector3.Dot(transform.forward, (Camera.main.transform.position - transform.position).normalized);
            if (Dot > 0.7f)
            {
                Debug.Log("Not Facing");
            }
            else
            {
                Debug.Log("Facing");
                TimeToSearch--;
            }
        }
    }
}
