using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AddressablesSceneLoader : ISceneLoader
{
    // Храним ID текущей сцены для рестарта
    private string _currentSceneId;

    // Флаг для предотвращения одновременной загрузки нескольких сцен
    private bool _isLoading;

    // Ссылка на handle последней загруженной сцены (опционально, для сложного управления памятью)
    private AsyncOperationHandle<SceneInstance> _currentSceneHandle;

    public AddressablesSceneLoader()
    {
        // Если игра запускается не с пустой сцены, можно попробовать инициализировать _currentSceneId
        // именем активной сцены, но для Addressables лучше использовать явные ключи.
        _currentSceneId = SceneManager.GetActiveScene().name;
    }

    public void SwitchTo(string sceneName)
    {
        if (_isLoading)
        {
            Debug.LogWarning($"[SceneLoader] Загрузка уже идет. Пропуск запроса на: {sceneName}");
            return;
        }

        // Запускаем асинхронный процесс без ожидания (Fire-and-forget), 
        // так как интерфейс возвращает void.
        LoadSceneProcess(sceneName).Forget();
    }

    public void Restart()
    {
        if (string.IsNullOrEmpty(_currentSceneId))
        {
            Debug.LogError("[SceneLoader] Невозможно перезагрузить: неизвестен ID текущей сцены.");
            return;
        }

        SwitchTo(_currentSceneId);
    }

    private async UniTaskVoid LoadSceneProcess(string key)
    {
        _isLoading = true;

        try
        {
            // 1. Загрузка сцены.
            // LoadSceneMode.Single автоматически выгружает предыдущую сцену.
            // activateOnLoad: true — сцена активируется сразу после загрузки.
            var loadHandle = Addressables.LoadSceneAsync(key, LoadSceneMode.Single, activateOnLoad: true);

            // Ожидаем завершения с помощью UniTask.
            // ToUniTask(Progress) можно использовать, если нужно передавать прогресс в UI загрузки.
            SceneInstance sceneInstance = await loadHandle.ToUniTask();

            // Если мы здесь, значит await прошел успешно (в UniTask исключения пробрасываются автоматически).

            // Сохраняем handle и ID
            _currentSceneHandle = loadHandle;
            _currentSceneId = key;

            // 3. Выполняем логику успешной загрузки
            SceneLoadingLogic();
        }
        catch (Exception ex)
        {
            // 4. Обработка ошибок
            // В WebGL и на телефонах важно видеть полный стек ошибки
            Debug.LogError($"[SceneLoader] Ошибка при загрузке сцены '{key}': {ex.Message}\n{ex.StackTrace}");
        }
        finally
        {
            _isLoading = false;
        }
    }

    /// <summary>
    /// Метод, вызываемый после успешной загрузки.
    /// </summary>
    private void SceneLoadingLogic()
    {
        Debug.Log($"[SceneLoader] Сцена '{_currentSceneId}' успешно загружена и инициализирована.");

        // Здесь можно вызвать сборщик мусора, если это критично для памяти на слабых телефонах,
        // хотя Addressables обычно хорошо управляют своей памятью.
        // Resources.UnloadUnusedAssets();
    }
}