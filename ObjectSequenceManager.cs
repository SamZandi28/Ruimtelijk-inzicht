using System.Collections;
using UnityEngine;

public class ObjectSequenceManager : MonoBehaviour
{
    public AudioSource introSound;
    public GameObject[] objectsInSequence;
    private int currentObjectIndex = 0;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject smokePrefab;
    /*void Start()
    {
        StartCoroutine(PlayIntroAndShowFirstObject());
    }*/
    void Update()
    {
        if (AreObjectsColliding(hammer, objectsInSequence[currentObjectIndex]))
        {
                Debug.Log("Hammer hit detected.");
                HandleObjectHit();
            
        }
    }
    public void StartSequence()
    {
        StartCoroutine(PlayIntroAndShowFirstObject());
    }
    bool AreObjectsColliding(GameObject objA, GameObject objB)
    {
        Collider colliderA = objA.GetComponent<Collider>();
        Collider colliderB = objB.GetComponent<Collider>();

        if (colliderA == null || colliderB == null)
        {
            Debug.LogWarning("One or both objects do not have colliders.");
            return false;
        }
        if (objB.tag == "Hitable")
        {
            return colliderA.bounds.Intersects(colliderB.bounds);
        }
        else
        {
            return false;
        }
    }

    private IEnumerator PlayIntroAndShowFirstObject()
    {
        if (introSound != null)
        {
            introSound.Play();
            Debug.Log("Intro sound started...");
            yield return new WaitForSeconds(introSound.clip.length);
            Debug.Log("Intro sound finished.");
        }

        if (objectsInSequence.Length > 0)
        {
            ActivateObject(0);
        }
    }

    private void ActivateObject(int index)
    {
        for (int i = 0; i < objectsInSequence.Length; i++)
        {
            objectsInSequence[i].SetActive(i == index);
        }
        Debug.Log($"Activated object: {objectsInSequence[index].name}");

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Hammer"))
        {
            Debug.Log("Hammer hit detected.");
            HandleObjectHit();
        }
    }*/

    private void HandleObjectHit()
    {
        if (currentObjectIndex < objectsInSequence.Length)
        {
            GameObject currentObject = objectsInSequence[currentObjectIndex];

            // Instantiate the smoke effect at the object's position
            TriggerSmokeEffect(currentObject);

            // Increment to the next object
            currentObjectIndex++;
            if (currentObjectIndex < objectsInSequence.Length)
            {
                ActivateObject(currentObjectIndex);
            }
            else
            {
                Debug.Log("No more objects in sequence.");
            }
        }
    }

    private void TriggerSmokeEffect(GameObject obj)
    {
        if (smokePrefab != null)
        {
            // Instantiate the smoke prefab at the object's position and rotation
            GameObject smokeInstance = Instantiate(smokePrefab, obj.transform.position, Quaternion.identity);
            Debug.Log($"Smoke prefab instantiated at: {obj.name}");

            // Destroy the smoke prefab after 4 seconds
            Destroy(smokeInstance, 3f);
        }
        else
        {
            Debug.LogWarning("Smoke prefab is not assigned in the Inspector!");
        }

        // Optional: Disable the object's collider or mesh if needed
        Collider objCollider = obj.GetComponent<Collider>();
        if (objCollider != null)
        {
            objCollider.enabled = false;
        }
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objRenderer.enabled = false;
        }
    }

}
