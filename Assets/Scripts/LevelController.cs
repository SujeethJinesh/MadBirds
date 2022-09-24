using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 2;
    private int _sceneCount;
    private Enemy[] _enemies;
    private Bird[] _birds;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _birds = FindObjectsOfType<Bird>();
        _sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Enemy enemy in _enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }

        string nextLevelName = "Level" + _nextLevelIndex;
        if (SceneUtility.GetBuildIndexByScenePath("path or name of the scene") > -1) {
            _nextLevelIndex++;
            SceneManager.LoadScene(nextLevelName);
        } else
        {
            SceneManager.LoadScene("GameOver");
        }

    }
}
