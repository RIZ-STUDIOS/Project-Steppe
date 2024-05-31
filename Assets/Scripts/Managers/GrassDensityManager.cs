using RicTools.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Managers
{
    public class GrassDensityManager : GenericManager<GrassDensityManager>
    {
        private Terrain terrain;

        private void Start()
        {
            terrain = GetComponent<Terrain>();
            UpdateGrass();
        }

        public void UpdateGrass()
        {
            var index = PlayerPrefs.GetInt("grassDensity", (int)GrassDensity.High);

            terrain.detailObjectDensity = ((index + 1) / ((float)GrassDensity.High + 1));

            terrain.detailObjectDistance = 50 + (index * 75);
        }
    }

    public enum GrassDensity
    {
        Low,
        Medium,
        High
    }
}
