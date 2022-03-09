using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject modelHolder = null;
    [SerializeField]
    private ModelController modelController = null;

    private Object[] models;
    private int currentModelChosen = 0;
    private GameObject model;

    private void Start()
    {
        models = Resources.LoadAll("Input", typeof(GameObject));
        LoadModel(currentModelChosen);
    }

    public void LoadNextModel()
    {
        currentModelChosen++;
        if (currentModelChosen == models.Length)
        {
            currentModelChosen = 0;
        }
        LoadModel(currentModelChosen);
    }

    public void LoadPreviousModel()
    {
        currentModelChosen--;
        if (currentModelChosen == -1)
        {
            currentModelChosen = models.Length - 1;
        }
        LoadModel(currentModelChosen);
    }

    public void LoadModel(int index)
    {
        if(modelHolder.transform.childCount != 0)
        {
            Destroy(model);
        }
        model = Instantiate(models[index] as GameObject, modelHolder.transform);
        modelController.SetCurrentModel(model);
        modelController.ResetTransform();
    }
}
