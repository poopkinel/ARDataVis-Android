using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadCSVList : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _dropDown;

    [SerializeField]
    private TreeVisualizer _treeVisualizer;

    private List<TextAsset> _allCSVFiles;

    private void Start()
    {
        LoadAllCSVFiles();
        _dropDown.onValueChanged.AddListener(CSVFileSelect);
    }

    private void LoadAllCSVFiles()
    {
        TextAsset[] csvFiles = Resources.LoadAll<TextAsset>("");
        _allCSVFiles = new List<TextAsset>(csvFiles.Reverse().Skip(2)); // Patch: Skip two irrelevant TextAssets

        List<TMP_Dropdown.OptionData> options = new();

        foreach (var file in _allCSVFiles)
        {
            if (string.IsNullOrWhiteSpace(file.name))
            {
                continue;
            }
            Debug.Log(file.name);
            options.Add(new TMP_Dropdown.OptionData(file.name));
        }
        
        _dropDown.AddOptions(options);
    }

    private void CSVFileSelect(int fileIndex)
    {
        Debug.Log($"in CSVFileSelect with {fileIndex}");
        _treeVisualizer.BuildFromCSV(_allCSVFiles[fileIndex - 1].name); // 0 Is the placeholder option
    }
}
