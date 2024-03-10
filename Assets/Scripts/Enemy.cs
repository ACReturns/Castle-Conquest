using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyRunSpeed = 5f;
	[SerializeField] AudioClip dyingSFX;
    Rigidbody2D enemyRigidBody;
	Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
		enemyRigidBody = GetComponent<Rigidbody2D>();
		enemyAnimator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
	{
		EnemyMovement();
	}

	public void Dying()
	{
		enemyAnimator.SetTrigger("Die");
		// Get game object components and disable them before destroying the enemy
		GetComponent<CapsuleCollider2D>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
		enemyRigidBody.bodyType = RigidbodyType2D.Static;
		EnemyDyingSFX();

		StartCoroutine(DestroyEnemy());
	}

	void EnemyDyingSFX()
	{
		AudioSource.PlayClipAtPoint(dyingSFX, Camera.main.transform.position);
	}

	IEnumerator DestroyEnemy()
	{
		// Stop after 2 seconds and destroy the enemy/ gameObject
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}

	private void EnemyMovement()
	{
		if (IsFacingLeft())
		{
			enemyRigidBody.velocity = new Vector2(-enemyRunSpeed, 0f);
		}
		else
		{
			enemyRigidBody.velocity = new Vector2(enemyRunSpeed, 0f);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        FlipSprite();
	}

	private void FlipSprite()
	{
		transform.localScale = new Vector2(Mathf.Sign(enemyRigidBody.velocity.x), 1f);
	}

	private bool IsFacingLeft()
    {
        return transform.localScale.x > 0f;
    }
}
