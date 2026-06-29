using TMPro;
using UnityEngine;

public class PlayerNameDisplay : MonoBehaviour
{
    public TMP_Text nameText;

    private PlayerMovement playerScriptReference;
    private string lastKnownName = "";

    private void Start()
    {
        playerScriptReference = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerScriptReference == null) return;

        // Update displayed name only if it changed
        if (playerScriptReference.playerName != lastKnownName)
        {
            lastKnownName = playerScriptReference.playerName;
            nameText.text = lastKnownName;
        }

        // Keep text above player as size changes
        transform.localPosition = new Vector3(
            0,
            playerScriptReference.transform.localScale.x * 0.7f,
            0
        );
    }
}