 using System;
 using UnityEngine;
 using UnityEngine.UI;

 public class UIIntro : UIControllerBase
 {
     [SerializeField] private Button buttonTest;

     public void SetButtron(Action OnEvent)
     {
         buttonTest.onClick.AddListener(() => OnEvent?.Invoke());
     }
 }
