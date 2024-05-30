using RicTools.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Managers
{
    public class GrassDensityManager : GenericManager<GrassDensityManager>
    {
        private List<GameObject> grassChildren = new List<GameObject>();

        private void Start()
        {
            foreach(Transform child in transform)
            {
                grassChildren.Add(child.gameObject);
            }
            UpdateGrass();
        }

        public void UpdateGrass()
        {
            var index = PlayerPrefs.GetInt("grassDensity", (int)GrassDensity.High);

            for (int i = 0; i < grassChildren.Count; i++)
            {
                var grass = grassChildren[i];
                grass.SetActive(i % (((int)GrassDensity.High - index) + 1) == 0);
            }
        }
    }

    public enum GrassDensity
    {
        Low,
        Medium,
        High
    }
}
