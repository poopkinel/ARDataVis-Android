using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadCSVList : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _dropDown;

    [Inject]
    private ITreeVisualizer _visualizer;

    private List<TextAsset> _allCSVFiles;

    private void Start()
    {
        LoadAllCSVFiles();
        _dropDown.onValueChanged.AddListener(CSVFileSelect);
    }

    private void LoadAllCSVFiles()
    {
        TextAsset[] csvFiles = Resources.LoadAll<TextAsset>("");

        _allCSVFiles = new List<TextAsset>(csvFiles);

        List<TMP_Dropdown.OptionData> options = new();

        foreach (var file in _allCSVFiles)
        {
            options.Add(new TMP_Dropdown.OptionData(file.name));
        }
        
        _dropDown.AddOptions(options);
    }

    private void CSVFileSelect(int fileIndex)
    {
        _visualizer.BuildFromCSV(_allCSVFiles[fileIndex].name);
    }
}
