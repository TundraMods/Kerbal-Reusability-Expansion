using System;
using UnityEngine;
using System.Collections.Generic;

namespace DeployableAeroSurfaces
{
    class ModuleDeployableAero:PartModule
    {
        //Index of ModuleAnimateGeneric (Or other module implementing IScalarModule)
        [KSPField]
        public int DeployModuleIndex;

        //Position of deploy module
        [KSPField]
        public int DeployModulePos;

        private IScalarModule deployModule;
        private ModuleLiftingSurface[] surfaces;
        private List<float> lifts = new List<float>();
        private List<float> ranges = new List<float>();

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            deployModule = part.Modules[DeployModuleIndex] as IScalarModule;
            surfaces = part.FindModulesImplementing<ModuleLiftingSurface>().ToArray();
            
            for (int i = 0; i < surfaces.Length; i++)
            {
                lifts.Add(surfaces[i].deflectionLiftCoeff);
                if (surfaces[i].GetType() == typeof(ModuleControlSurface) | surfaces[i].GetType() == typeof(ModuleAeroSurface))
                {
                    ModuleControlSurface srf = surfaces[i] as ModuleControlSurface;
                    ranges.Add(srf.ctrlSurfaceRange);
                }
            }

            deployModule.OnStop.Add(UpdateSurfaces);
            UpdateSurfaces(deployModule.GetScalar);
        }

        public void UpdateSurfaces(float pos)
        {
            if (pos == DeployModulePos)
            {
                for (int i = 0; i < surfaces.Length; i++)
                {
                    surfaces[i].deflectionLiftCoeff = lifts[i];
                    if (surfaces[i].GetType() == typeof(ModuleControlSurface) | surfaces[i].GetType() == typeof(ModuleAeroSurface))
                    {
                        ModuleControlSurface srf = surfaces[i] as ModuleControlSurface;
                        srf.ctrlSurfaceRange = ranges[0];
                    }
                }
            }
            else
            {
                for (int i = 0; i < surfaces.Length; i++)
                {
                    surfaces[i].deflectionLiftCoeff = 0.0001f;
                    if (surfaces[i].GetType() == typeof(ModuleControlSurface) | surfaces[i].GetType() == typeof(ModuleAeroSurface))
                    {
                        ModuleControlSurface srf = surfaces[i] as ModuleControlSurface;
                        srf.ctrlSurfaceRange = 0.0001f;
                    }
                }
            }
        }
    }
}
