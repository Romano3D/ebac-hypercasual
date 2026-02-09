using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materials;
    public List<ColorSetup> colorSetups;

    public Color GetColorByType(ArtManager.ArtType artType, int index)
    {
        var setup = colorSetups.Find(i => i.artType == artType);

        if (setup == null)
        {
            Debug.LogError($"ColorSetup não encontrado: {artType}");
            return Color.white;
        }

        if (index >= setup.colors.Count)
        {
            Debug.LogWarning("Índice de cor fora do range");
            return setup.colors[0];
        }

        return setup.colors[index];
    }

    [System.Serializable]
    public class ColorSetup
    {
        public ArtManager.ArtType artType;
        public List<Color> colors;
    }

}
