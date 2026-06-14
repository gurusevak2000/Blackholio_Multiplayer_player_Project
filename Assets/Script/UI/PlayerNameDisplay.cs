using TMPro;
using UnityEngine;

public class PlayerNameDisplay : MonoBehaviour
{
    public TMP_Text nameText;

    private PlayerMovement playerScriptReference;

    private void Start()
    {
        playerScriptReference = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerScriptReference == null) return;

        // Update displayed name
        nameText.text = playerScriptReference.playerName;

        // Keep text above player as size changes
        transform.localPosition = new Vector3(
            0,
            playerScriptReference.transform.localScale.x * 0.7f,
            0
        );
    }
}