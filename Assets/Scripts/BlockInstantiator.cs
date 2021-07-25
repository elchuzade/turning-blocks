using UnityEngine;

public class BlockInstantiator : MonoBehaviour
{
    // To choose a prefab from when creating new block
    [SerializeField] GameObject[] allNewBlocks;

    // To access a block for turning when tapped
    GameObject newBlock;

    public void InstantiateBlock()
    {
        // Random block prefab
        GameObject randomBlock = allNewBlocks[Random.Range(0, allNewBlocks.Length)];

        newBlock = Instantiate(randomBlock, transform.position, Quaternion.identity);
        newBlock.transform.SetParent(transform);
        // Initialize a new block
        newBlock.GetComponent<Block>().CreateBlock(gameObject);
    }

    public void TurnBlock()
    {
        newBlock.GetComponent<Block>().TurnBlock();
    }
}
