using UnityEngine;
using System.Collections;

public class RedWoman : MonoBehaviour {

    public float d1;
    public float d2;

    public Transform startPosition;
    public Transform finishPosition;

    public GameObject[] items;

    public float speed = 0.001f;
    public float progress = 0f;
    public float dropRate = 0.1f;

    public bool ready = false;
    public float minDistanceToDrop = 5f;
    public bool readyToDrop = false;
    public int waitTime = 10;

    void Start()
    {
        ready = false;
        readyToDrop = false;
        progress = 0f;
        transform.position = startPosition.position;
        StartCoroutine("Wait");
        
    }

    void Update()
    {
        if (ready)
        {
            AdvanceToFinishPosition();
            if (Random.Range(0f,1f) < dropRate * Time.deltaTime)
            {
                DropPresent();
            }

        }
    }


    void AdvanceToFinishPosition()
    {
        progress = Mathf.Clamp(progress + (speed * Time.deltaTime),0f,1f);
        transform.position = Vector3.Lerp(startPosition.position, finishPosition.position, progress);
        d1 = Vector3.Distance(transform.position, startPosition.position);
        d2 = Vector3.Distance(finishPosition.position, transform.position);
        if (d1 < minDistanceToDrop || d2 < minDistanceToDrop)
        {
            readyToDrop = false;
        }
        else
        {
            readyToDrop = true;
        }
        if (!readyToDrop && Vector3.Distance(transform.position,finishPosition.position) < 1f)
        {
            Start();
        }
    }

    void DropPresent()
    {
        if (items != null && items.Length > 0)
        {
            int itemIndex = Random.Range(0, items.Length);
            GameObject gO = GameObject.Instantiate(items[itemIndex]) as GameObject;
            gO.transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);


            Debug.Log("Drop item");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        ready = true;
    }

}
