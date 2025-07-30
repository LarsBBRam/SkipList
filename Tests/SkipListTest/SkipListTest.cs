using System;
using SkipList.Node;
using SkipList.SkipList;
using Xunit.Abstractions;

namespace SkipListTests;

public class SkipListTest(ITestOutputHelper helper)
{
    private readonly ITestOutputHelper _helper = helper;
    [Fact]
    public void TestInitializeSkipListWithCollectionExpression()
    {
        // Given
        SkipList<int> skipListofInts = [];
        // When

        // Then
        Assert.NotNull(skipListofInts);
    }

    [Fact]
    public void TestInsertNumberToSkipListofInts()
    {
        // Given
        SkipList<int> ints = [];
        // When
        ints.Insert(10);
        // Then
        var value = ints.Search(10);
        Assert.NotNull(value);
        Assert.IsType<SkipListNode<int>>(value);
        Assert.Equal(10, value.Value);
    }

    [Fact]
    public void TestInsertAndDeleteShouldReturnNullThroughSearch()
    {
        // Given
        SkipList<int> ints = [];
        // When
        ints.Delete(10);
        // Then
        var result = ints.Search(10);
        Assert.Null(result);
    }

    [Fact]
    public void TestName()
    {
        // Given

        // When

        // Then
    }


    [Fact]
    public void TestEnumeration()
    {
        // Given
        List<int> list = [1, 2, 3, 4, 5];

        var query = list.Select(n => n * 2);

        list.Add(8);

        foreach (var n in query) _helper.WriteLine(n.ToString());

        // When

        // Then
    }
}
