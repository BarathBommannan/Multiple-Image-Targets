using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combain : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Sphere;
    public GameObject CapsulePrefab; // Prefab of the Capsule to spawn

    private bool targetsCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ImageTarget"))
        {
            targetsCollided = true;
            CombineObjects();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ImageTarget"))
        {
            targetsCollided = false;
            ResetObjects();
        }
    }

    private void CombineObjects()
    {
        if (targetsCollided)
        {
            // Find the center point between the two image targets
            Vector3 centerPoint = (Cube.transform.position + Sphere.transform.position) / 2f;

            // Spawn the Capsule at the center point
            GameObject newCapsule = Instantiate(CapsulePrefab, centerPoint, Quaternion.identity);
            // Ensure it's a child of this GameObject
            newCapsule.transform.parent = transform;

            // Deactivate Cube and Sphere
            Cube.SetActive(false);
            Sphere.SetActive(false);
        }
    }

    private void ResetObjects()
    {
        Cube.SetActive(true);
        Sphere.SetActive(true);

        // Destroy any spawned Capsules when the targets are no longer colliding
        GameObject[] capsules = GameObject.FindGameObjectsWithTag("Capsule");
        foreach (GameObject capsule in capsules)
        {
            Destroy(capsule);
        }
    }
}
