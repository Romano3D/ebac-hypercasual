using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public List<LevelPieceBaseSetup> levelPieceBasedSetups;

    public float timeBetweenPieces = .3f;

    [SerializeField] private int _index;
    private GameObject _currentLevel;


    [SerializeField] private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBaseSetup _currSetup;
    

    private void Awake()
    {
       // SpawnNextLevel();
        CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if (_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;

    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region

    private void CreateLevelPieces()
    {
        if (levelPieceBasedSetups == null || levelPieceBasedSetups.Count == 0)
        {
            Debug.LogError("Nenhum LevelPieceBasedSetup atribuído!");
            return;
        }

        CleanSpawnedPieces();

        _currSetup = levelPieceBasedSetups[_index];

        _index++;

        if (_index >= levelPieceBasedSetups.Count)
        {
            _index = 0;
        }

        for (int i = 0; i < _currSetup.piecesStartNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesStart);
        }

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
        }

        for (int i = 0; i < _currSetup.piecesEndNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesEnd);
        }

        ColorManager.Instance.ChangeColorByType(_currSetup.artType);
    }
    /* private void CreateLevelPieces()
     {
         CleanSpawnedPieces();

         // StartCoroutine(CreateLevelPiecesCoroutine());

         if (_currSetup != null)
         {
             _index++;

             if (_index >= levelPieceBasedSetups.Count)
             {
                 ResetLevelIndex();
             }
         }

         _currSetup = levelPieceBasedSetups[_index];

         for (int i = 0; i < _currSetup.piecesStartNumber; i++)
         {
             CreateLevelPiece(_currSetup.levelPiecesStart);
         }
         for (int i = 0; i < _currSetup.piecesNumber; i++)
         {
             CreateLevelPiece(_currSetup.levelPieces);
         }
         for (int i = 0; i < _currSetup.piecesEndNumber; i++)
         {
             CreateLevelPiece(_currSetup.levelPiecesEnd);
         }
     }*/
    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spawnedPiece.transform.localPosition = lastPiece.endPiece.position;
        }
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        _spawnedPieces.Add(spawnedPiece);

        foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currSetup.artType).gameObject);
        }
    }

    private void CleanSpawnedPieces()
    {
        for(int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear();
    }

    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelPieces();
        }
    }
}




























/*public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public List<LevelPieceBaseSetup> levelPieceBaseSetups;

    [SerializeField] private int _levelIndex = 0;

    public float timeBetweenPieces = .3f;

    [SerializeField] private int _leveIndex;
    private LevelPieceBaseSetup _currSetup;
    [SerializeField] private List<LevelPieceBase> _spawnedPieces = new();

    private GameObject _currentLevel;
    private LevelPieceBase spawnedPiece;

    private void Awake()
    {
        SpawnNextLevel();
    }

    private void SpawnNextLevel()
    {
        CleanSpawnedPieces();

        if (_levelIndex >= levelPieceBaseSetups.Count)
            _levelIndex = 0;

        _currSetup = levelPieceBaseSetups[_levelIndex];
        CreateLevelPieces(_currSetup);

        _levelIndex++;
    }
    private void CreateLevelPiecesForCurrentLevel()
    {
        _spawnedPieces.Clear();

        var setup = levelPieceBaseSetups[_leveIndex];

        for (int i = 0; i < setup.piecesStartNumber; i++)
            CreateLevelPiece(setup.levelPiecesStart, _currentLevel.transform);

        for (int i = 0; i < setup.piecesNumber; i++)
            CreateLevelPiece(setup.levelPieces, _currentLevel.transform);

        for (int i = 0; i < setup.piecesEndNumber; i++)
            CreateLevelPiece(setup.levelPiecesEnd, _currentLevel.transform);
    }


    private void ResetLevelIndex()
    {
        _leveIndex = 0;
    }

    #region
    private void CreateLevelPieces(LevelPieceBaseSetup setup)
    {
        _spawnedPieces.Clear();

        for (int i = 0; i < setup.piecesStartNumber; i++)
            CreateLevelPiece(setup.levelPiecesStart, container);

        for (int i = 0; i < setup.piecesNumber; i++)
            CreateLevelPiece(setup.levelPieces, container);

        for (int i = 0; i < setup.piecesEndNumber; i++)
            CreateLevelPiece(setup.levelPiecesEnd, container);
    }

    private void CreateLevelPiece(List<LevelPieceBase> list, Transform parent)
    {
        var prefab = list[Random.Range(0, list.Count)];
        var piece = Instantiate(prefab, parent);

        var renderers = piece.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            var mat = renderers[i].material;

            string prop = mat.HasProperty("_BaseColor") ? "_BaseColor" : "_Color";

            mat.SetColor(
                prop,
                ColorManager.Instance.GetColorByType(_currSetup.artType, 0)
            );
        }

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[^1];
            piece.transform.position = lastPiece.endPiece.position;
        }
        else
        {
            piece.transform.localPosition = Vector3.zero;
        }

        foreach (var p in piece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(
                ArtManager.Instance
                    .GetSetupByType(_currSetup.artType)
                    .gameObject
            );
        }

        _spawnedPieces.Add(piece);
    }

    private void CleanSpawnedPieces()
    {
        foreach (var piece in _spawnedPieces)
            Destroy(piece.gameObject);

        _spawnedPieces.Clear();
    }
    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces, _currentLevel.transform);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
      //  ColorManager.Instance.ChangeColorByType(_currSetup.artType);
    
}

    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnNextLevel();
        }
    }
}
*/