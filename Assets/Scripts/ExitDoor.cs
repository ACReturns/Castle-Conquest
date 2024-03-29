using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
	[SerializeField] float secondsToLoad = 2f;
	[SerializeField] AudioClip openingDoorSFX, closingDoorSFX;

	private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Animator>().SetTrigger("Open");
    }

	public void StartLoadingNextLevel()
	{
		GetComponent<Animator>().SetTrigger("Close");
		AudioSource.PlayClipAtPoint(closingDoorSFX, Camera.main.transform.position);
		StartCoroutine(LoadNextLevel());
	}

	IEnumerator LoadNextLevel()
	{
		yield return new WaitForSeconds(secondsToLoad);

		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex + 1);
	}

	void PlayOpeningDoorSFX()
	{
		AudioSource.PlayClipAtPoint(openingDoorSFX, Camera.main.transform.position);
	}
}
