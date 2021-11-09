// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.XR.ARSubsystems;
//
// public class MultiImageTracker : MonoBehaviour
// {
//     ARTrackedImageManager arTrackedImageManager;
//
//     //mapping of image to prefab
//     //[Serializable] - DOUBLE CHECK THIS
//     public class trackedPrefab{
//       public string name;
//       public GameObject prefab;
//     }
//
//     public trackedPrefab[] prefabsToInstantiate;
//
//     //tracked objects
//     Dictionary<string, GameObject> instantiatedPrefabs;
//
//     // Start is called before the first frame update
//     void Start()
//     {
//         instantiatedPrefabs = new Dictionary<string, GameObject>();
//
//         Screen.sleepTimeout = SleepTimeout.NeverSleep;
//         arTrackedImageManager = GetComponent<ARTrackedImageManager>();
//         arTrackedImageManager.trackedImagesChanged += OnTrackedImage;
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//
//     }
//
//     private void OnTrackedImage(ARTrackedImagesChangedEventArgs obj){
//         //obj.added - list of newly found images
//         //obj.updated - list of prefabs already generated that should be updated
//         //obj.removed - list of prefabs with images not found anymore (lost images)
//
//         //new images
//         foreach ( ARTrackedImage image in obj.added){
// //REST OF CODE ONLINE
//         }
//
//         //updated images
//         foreach ( ARTrackedImage image in obj.updated){
//
//         }
//
//         //DOING REMOVED IN A DIFFERENT WAY
//     }
// }
