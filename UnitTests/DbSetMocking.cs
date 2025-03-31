using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests;

internal static class DbSetMocking
{
    public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> source) where T : class
    {
        var mock = new Mock<DbSet<T>>();
        mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
        mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
        mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
        mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
        return mock;
    }
}
