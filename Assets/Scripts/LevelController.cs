using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _levelIndex = 2;
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

        string nextLevelName = "Level" + _levelIndex;
        if (SceneUtility.GetBuildIndexByScenePath(nextLevelName) > -1) {
            _levelIndex++;
            SceneManager.LoadScene(nextLevelName);
        } else
        {
            SceneManager.LoadScene("GameOver");
        }

    }
}
