using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private TrackedPrefab[] prefabToInstantiate;

    private Dictionary<string, GameObject> instantiatedPrefabs;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        instantiatedPrefabs = new Dictionary<string, GameObject>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage addedImage in eventArgs.added)
        {
            InstantiateGameObject(addedImage);
        }

        foreach (ARTrackedImage updatedImage in eventArgs.updated)
        {
            if (updatedImage.trackingState == TrackingState.Tracking)
            {
                UpdateTrackingGameObject(updatedImage);
            }
            else if (updatedImage.trackingState == TrackingState.Limited)
            {
                UpdateLimitedGameObject(updatedImage);
            }
            else
            {
                UpdateNoneGameObject(updatedImage);
            }
        }

        foreach (ARTrackedImage removedImage in eventArgs.removed)
        {
            DestroyGameObject(removedImage);
        }
    }

    private void InstantiateGameObject(ARTrackedImage addedImage)
    {
        Debug.Log(".......... InstantiateGameObject");
        for (int i = 0; i < prefabToInstantiate.Length; i++)
        {
            if (addedImage.referenceImage.name == prefabToInstantiate[i].name)
            {
              //why is it transform.parent?
                GameObject prefab = Instantiate(prefabToInstantiate[i].prefab, transform.parent);
                prefab.transform.position = addedImage.transform.position;
                prefab.transform.rotation = addedImage.transform.rotation;

                instantiatedPrefabs.Add(addedImage.referenceImage.name, prefab);
            }
        }
    }

    private void UpdateTrackingGameObject(ARTrackedImage updatedImage)
    {
        Debug.Log(".......... UpdateTrackingGameObject"); //*
        for (int i = 0; i < instantiatedPrefabs.Count; i++)
        {
            if (instantiatedPrefabs.TryGetValue(updatedImage.referenceImage.name, out GameObject prefab))
            {
                prefab.transform.position = updatedImage.transform.position;
                prefab.transform.rotation = updatedImage.transform.rotation;
                prefab.SetActive(true);
            }
        }
    }

    private void UpdateLimitedGameObject(ARTrackedImage updatedImage)
    {
        Debug.Log(".......... UpdateLimitedGameObject"); //*
        for (int i = 0; i < instantiatedPrefabs.Count; i++)
        {
            if (instantiatedPrefabs.TryGetValue(updatedImage.referenceImage.name, out GameObject prefab))
            {
                if (!prefab.GetComponent<ARTrackedImage>().destroyOnRemoval)
                {
                    prefab.transform.position = updatedImage.transform.position;
                    prefab.transform.rotation = updatedImage.transform.rotation;
                    prefab.SetActive(true);
                }
                else
                {
                    prefab.SetActive(false);
                }

            }
        }
    }

    private void UpdateNoneGameObject(ARTrackedImage updateImage)
    {
        Debug.Log(".......... UpdateNoneGameObject");
        for (int i = 0; i < instantiatedPrefabs.Count; i++)
        {
            if (instantiatedPrefabs.TryGetValue(updateImage.referenceImage.name, out GameObject prefab))
            {
                prefab.SetActive(false);
            }
        }
    }

    private void DestroyGameObject(ARTrackedImage removedImage)
    {
        Debug.Log(".......... DestroyGameObject");
        for (int i = 0; i < instantiatedPrefabs.Count; i++)
        {
            if (instantiatedPrefabs.TryGetValue(removedImage.referenceImage.name, out GameObject prefab))
            {
                instantiatedPrefabs.Remove(removedImage.referenceImage.name);
                Destroy(prefab);
            }
        }
    }
}

[Serializable]
public struct TrackedPrefab
{
    public string name;
    public GameObject prefab;
}
