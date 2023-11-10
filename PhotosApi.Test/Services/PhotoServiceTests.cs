using Moq;
using Bogus;
using PhotosApi.Models;
using PhotosApi.Repositories;
using PhotosApi.Services;

namespace PhotosApi.Tests.Services;

public class PhotosServiceTests
{
    private static PhotosApi.Repositories.MockRepository _repo = new PhotosApi.Repositories.MockRepository();

    [Theory]
    [InlineData("Some Name", "Some Description")]
    [InlineData("Some Name", "")]
    [InlineData("", "Some description")]
    [InlineData("", "")]
    [InlineData("Testing", "GetPhoto")]
    public void GetPhoto_ValidCall(string expectedName, string expectedDescription)
    {
        // Arrange
        BuildRepo();
        var expectedId = Guid.NewGuid();
        _repo.Store(new Photo { Id = expectedId, Name = expectedName, Description = expectedDescription });
        var repositoryMock = new Mock<IPhotosRepository>();
        repositoryMock.Setup(x => x.Get(expectedId)).Returns(_repo.Get(expectedId));

        var service = new PhotosService(repositoryMock.Object);

        // Act
        var result = service.GetPhoto(expectedId);

        // Assert
        Assert.Equal(expectedName, result.Name);
        Assert.Equal(expectedId, result.Id);
        Assert.Equal(expectedDescription, result.Description);
    }

    [Theory]
    [InlineData("Pic1", "Some description")]
    public void StorePhoto_ValidCall(string expectedName, string expectedDescription)
    {
        // Arrange
        BuildRepo();
        var expectedId = Guid.NewGuid();
        var repositoryMock = new Mock<IPhotosRepository>();
        var photo = new Photo { Id = expectedId, Name = expectedName, Description = expectedDescription };
        repositoryMock.Setup(x => x.Store(photo));

        var service = new PhotosService(repositoryMock.Object);

        // Act
        service.StorePhoto(photo);

        // Assert
        repositoryMock.Verify(x => x.Store(photo), Times.Exactly(1));
    }

    [Fact]
    public void UpsertPhoto_ValidCall()
    {
        var photoFaker = GetPhotoFaker();

        foreach (var photo in photoFaker.Generate(100))
        {
            // Arrange
            var repositoryMock = new Mock<IPhotosRepository>();
            repositoryMock.Setup(x => x.Upsert(photo));

            var service = new PhotosService(repositoryMock.Object);

            // Act
            service.UpsertPhoto(photo);

            // Assert
            repositoryMock.Verify(r => r.Upsert(photo), Times.Exactly(1));
        }

    }

    [Fact]
    public void DeletePhoto_ValidCall()
    {
        var photoFaker = GetPhotoFaker();

        foreach (var photo in photoFaker.Generate(100))
        {
            // Arrange
            var repositoryMock = new Mock<IPhotosRepository>();
            repositoryMock.Setup(x => x.Delete(photo.Id));

            var service = new PhotosService(repositoryMock.Object);

            // Act
            service.DeletePhoto(photo.Id);

            // Assert
            repositoryMock.Verify(r => r.Delete(photo.Id), Times.Exactly(1));
        }

    }

    [Fact]
    public void GetPhotos_ValidCall()
    {
        for (int i = 0; i < 20; i++)
        {
            // Arrange
            BuildRepo();
            var expected = _repo.GetAll();
            var repositoryMock = new Mock<IPhotosRepository>();
            repositoryMock.Setup(r => r.GetAll()).Returns(() => expected);
            var service = new PhotosService(repositoryMock.Object);

            // Act
            var actual = service.GetPhotos();

            // Assert
            Assert.True(expected.Length == actual.Length);

            for (int j = 0; j < expected.Length; j++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Id, actual[i].Id);
                Assert.Equal(expected[i].Description, actual[i].Description);
            }
        }
    }

    private void BuildRepo()
    {
        var photoModelFaker = GetPhotoFaker();
        foreach (var photo in photoModelFaker.Generate(100))
            _repo.Store(photo);
    }
    private Faker<Photo> GetPhotoFaker()
    {
        var photoModelFaker = new Faker<Photo>("es") // By default it's "en" English
            .RuleFor(p => p.Id, Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Name.FirstName())
            .RuleFor(p => p.Description, f => f.Lorem.Text());
        return photoModelFaker;
    }
}