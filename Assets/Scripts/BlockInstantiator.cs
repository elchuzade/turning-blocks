using UnityEngine;

public class BlockInstantiator : MonoBehaviour
{
    [SerializeField] GameObject[] allNewBlocks;

    GameObject newBlock;

    public void InstantiateBlock()
    {
        GameObject randomBlock = allNewBlocks[Random.Range(0, allNewBlocks.Length)];

        newBlock = Instantiate(randomBlock, transform.position, Quaternion.identity);
        newBlock.transform.SetParent(transform);
        newBlock.GetComponent<Block>().CreateBlock(gameObject);
    }

    public void TurnBlock()
    {
        newBlock.GetComponent<Block>().TurnBlock();
    }
}
