//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class DeathCountText : MonoBehaviour, IDataPersistence
//{
    //private int deathCount = 0;

   // private TextMeshProUGUI deathCountText;

   // private void Awake()
  //  {
    //    deathCountText = this.GetComponent<TextMeshProUGUI>();
   // }

   // public void LoadData(GameData data)
  //  {
   //     this.deathCount = data.deathCount;
  //  }

   // public void SaveData(ref GameData data)
  //  {
  //      data.deathCount = this.deathcount;
  //  }

  //  private void Start()
   // {
   //     GameEventsManager.instance.onPlayerDeath += onPlayerDeath;
   // }

   // private void OnDestroy()
   // {
   //     GameEventsManager.instance.onPlayerDeath;
  //  }

   // private void onPlayerDeath()
   // {
  //      deathcount++;
  //  }

   // private void Update()
   // {
  //      deathCountText.text = "" + deathcount;
   // }

   // public void SaveData(ref GameData data)
   // {
   //     throw new System.NotImplementedException();
   // }
//}