using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCSVList : MonoBehaviour
{
    [SerializeField]
    private Dropdown _dropDown;

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

        List<Dropdown.OptionData> options = new();

        foreach (var file in _allCSVFiles)
        {
            options.Add(new Dropdown.OptionData(file.name));
        }
        
        _dropDown.AddOptions(options);
    }

    private void CSVFileSelect(int fileIndex)
    {

    }
}
