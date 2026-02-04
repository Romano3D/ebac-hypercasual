using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public List<LevelPieceBaseSetup> levelPieceBaseSetups;

    public float timeBetweenPieces = .3f;

    [SerializeField] private int _index;
    private GameObject _currentLevel;

    [SerializeField] private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBaseSetup _currSetup; 

    private void Awake()
    {
        //SpawnNextLevel();
        //CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if (_currentLevel != null)
            Destroy(_currentLevel);

        if (_index >= levels.Count)
            _index = 0;

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;

        // 🔥 GERA AS PIECES PARA ESSE LEVEL
        CreateLevelPiecesForCurrentLevel();

        _index++;
    }
    private void CreateLevelPiecesForCurrentLevel()
    {
        _spawnedPieces.Clear();

        var setup = levelPieceBaseSetups[_index];

        for (int i = 0; i < setup.piecesStartNumber; i++)
            CreateLevelPiece(setup.levelPiecesStart, _currentLevel.transform);

        for (int i = 0; i < setup.piecesNumber; i++)
            CreateLevelPiece(setup.levelPieces, _currentLevel.transform);

        for (int i = 0; i < setup.piecesEndNumber; i++)
            CreateLevelPiece(setup.levelPiecesEnd, _currentLevel.transform);
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region
    private void CreateLevelPieces()
    {
        cleanSpawnedPieces();

        if (_currSetup != null)
        {
            _index++;

            if (_index >= levelPieceBaseSetups.Count)
            {
                ResetLevelIndex();
            }
        }

        _currSetup = levelPieceBaseSetups[_index];

        for (int i = 0; i < _currSetup.piecesStartNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesStart, _currentLevel.transform);
        }

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces, _currentLevel.transform);
        }

        for (int i = 0; i < _currSetup.piecesEndNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesEnd, _currentLevel.transform);
        }
    }

    private void CreateLevelPiece(List<LevelPieceBase> list, Transform parent)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, parent);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void cleanSpawnedPieces()
    {
        for (int i = _spawnedPieces.Count - 1; i >= 0; i--)
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
            CreateLevelPiece(_currSetup.levelPieces, _currentLevel.transform);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
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
