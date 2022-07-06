// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [System.Serializable]
// public class BombItem
// {
//     public int amount;
// 	public  GameObject prefab;
// }

// public class ExistingBombItem {
//     public GameObject gameObject;

//     // Constructor
//     public ExistingBombItem(GameObject gameObject){
//         //reference input
//         this.gameObject = gameObject;
//     }
// }

// public class BombPooler : MonoBehaviour {
//     public static BombPooler SharedInstance;
//     public List<BombItem> bombsToPool;
//     public List<ExistingBombItem> pooledBombs; 

//     void Awake()
//     {
//         // Access the ObjectPooler instance fast and since there should only be one instance of this per scene,
//         // Create one instance of this per scene: 
//         SharedInstance = this;
//         pooledBombs = new List<ExistingBombItem>();
//         Debug.Log("ObjectPooler is Awake");

//         foreach (ExistingBombItem bomb in pooledBombs)
//         {
//             for (int i = 0; i < pooledBombs.length ; i++)
//             {
//                 // this 'pickup' a local variable, but Unity will not remove it since it exists in the scene
//                 GameObject pickup = (GameObject)Instantiate(bomb.prefab);
//                 pickup.SetActive(false);
//                 pickup.transform.parent = this.transform;

//                 // e contains a reference to a newly instantiated ExistingPoolItem object 
//                 ExistingBombItem e = new ExistingBombItem(pickup);
//                 pooledBombs.Add(e);
//             }
//         }
//     }

//     public GameObject GetPooledObject(){
//         for (int i = 0; i < pooledBombs.Count; i++){
//             if (!pooledBombs[i].gameObject.activeInHierarchy){
//                 return pooledBombs[i].gameObject;
//             }
//         }

//         foreach (BombItem bomb in bombsToPool)
//             {
//                 GameObject pickup = (GameObject)Instantiate(bomb.prefab);
//                 pickup.SetActive(false);
//                 pickup.transform.parent = this.transform;
//                 pooledBombs.Add(new ExistingBombItem(pickup));
//                 return pickup;
//             }
//         return null;
//     }
// }

