using Application.Service;
using Application.Service.Interface;
using Entities;
using Moq;

namespace Test;

public class UserServiceTests
{
    [Fact]
    public async Task CreateUserAsync_CallsRepositoryAddUser()
    {
        var repoMock = new Mock<IUserRepository>();
        var service = new UserService(repoMock.Object);
        var user = new User { FirstName = "Test", LastName = "User" };

        await service.CreateUserAsync(user);

        repoMock.Verify(r => r.AddUserAsync(user), Times.Once);
    }

    [Fact]
    public async Task DeleteUserAsync_FetchesUserThenDeletes()
    {
        var user = new User { Id = 1, FirstName = "Test" };
        var repoMock = new Mock<IUserRepository>();
        repoMock.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync(user);
        var service = new UserService(repoMock.Object);

        await service.DeleteUserAsync(1);

        repoMock.Verify(r => r.GetUserByIdAsync(1), Times.Once);
        repoMock.Verify(r => r.DeleteUserAsync(user), Times.Once);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUserFromRepository()
    {
        var user = new User { Id = 42, FirstName = "Alice" };
        var repoMock = new Mock<IUserRepository>();
        repoMock.Setup(r => r.GetUserByIdAsync(42)).ReturnsAsync(user);
        var service = new UserService(repoMock.Object);

        var result = await service.GetUserByIdAsync(42);

        Assert.Equal(user, result);
    }

    [Fact]
    public async Task UpdateUserAsync_DelegatesToRepository()
    {
        var repoMock = new Mock<IUserRepository>();
        var service = new UserService(repoMock.Object);
        var updated = new User { Id = 5, FirstName = "Updated" };

        await service.UpdateUserAsync(5, updated);

        repoMock.Verify(r => r.UpdateUserAsync(5, updated), Times.Once);
    }
}

public class PostServiceTests
{
    [Fact]
    public async Task CreatePostAsync_CallsRepositoryAddPost()
    {
        var repoMock = new Mock<IPostRepository>();
        var service = new PostService(repoMock.Object);
        var post = new Post { Title = "Hello", Content = "World" };

        await service.CreatePostAsync(post);

        repoMock.Verify(r => r.AddPostAsync(post), Times.Once);
    }

    [Fact]
    public async Task DeletePostAsync_FetchesPostThenDeletes()
    {
        var post = new Post { Id = 3, Title = "To delete" };
        var repoMock = new Mock<IPostRepository>();
        repoMock.Setup(r => r.GetPostByIdAsync(3)).ReturnsAsync(post);
        var service = new PostService(repoMock.Object);

        await service.DeletePostAsync(3);

        repoMock.Verify(r => r.GetPostByIdAsync(3), Times.Once);
        repoMock.Verify(r => r.DeletePostAsync(post), Times.Once);
    }

    [Fact]
    public async Task GetPagedPostsAsync_DelegatesToRepository()
    {
        var posts = new List<Post> { new Post { Id = 1 }, new Post { Id = 2 } };
        var repoMock = new Mock<IPostRepository>();
        repoMock.Setup(r => r.GetPagedPostsAsync(1, 5)).ReturnsAsync(posts);
        var service = new PostService(repoMock.Object);

        var result = await service.GetPagedPostsAsync(1, 5);

        Assert.Equal(2, result.Count);
        repoMock.Verify(r => r.GetPagedPostsAsync(1, 5), Times.Once);
    }
}
