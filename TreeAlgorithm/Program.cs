namespace TreeAlgorithm;

public static class Program
{
    public static void Main()
    {
        var treeNode = new TreeNode(1, new TreeNode(2,
            new TreeNode(4),
            new TreeNode(5)),
            new TreeNode(5,
            new TreeNode(4),
            new TreeNode(8)));
        var arr = new int[] {4, 5};
        var result = Calculate(treeNode, arr);
        for (var i = 0; i < arr.Length; i++)
            Console.WriteLine($"The number of {arr[i]} is {result[i]}");
    }
    public static int[] Calculate(TreeNode treeNode, int[] arr)
    {
        var resultArr = new int[arr.Length];
        AddArrayValue(arr, resultArr, treeNode.Value, (a, b) => a == b);
        if (treeNode.Right is not null)
            SumArrays(resultArr, Calculate(treeNode.Right, arr));
        if (treeNode.Left is not null)
            SumArrays(resultArr, Calculate(treeNode.Left, arr));

        return resultArr;
    }

    public static void AddArrayValue(int[] arr, int[] resultArr,
        int value,
        Func<int, int, bool> func) =>
        AddArrayValueHelper(0,
            arr,
            resultArr,
            value,
            func);
    public static void AddArrayValueHelper(int index, int[] arr, int[] resultArr, int value, Func<int, int, bool> func)
    {
        if (index >= arr.Length)
            return;
        if (func(arr[index], value))
            resultArr[index]++;
        AddArrayValueHelper(index + 1, arr, resultArr, value, func);
    }
    
    public static void SumArrays(int[] main, int[] second) => SumHelper(0, main, second);

    private static void SumHelper(int index, int[] main, int[] second)
    {
        if (index >= main.Length)
            return;
        main[index] += second[index];
        SumHelper(index + 1, main, second);
    }
}



public record TreeNode(int Value, TreeNode? Left = null, TreeNode? Right = null);