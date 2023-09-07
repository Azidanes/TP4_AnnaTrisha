using Microsoft.Extensions.Logging;
using TP4_AnnTrisha.Controllers;
using TP4_AnnTrisha;
using Moq;

namespace TestProjectTP4
{
    public class UnitTest1
    {


        public class WeatherForecastControllerTests
        {
            private WeatherForecastController CreateController()
            {
                var loggerMock = new Mock<ILogger<WeatherForecastController>>();
                return new WeatherForecastController(loggerMock.Object);
            }

            [Fact]
            public void AvoirChansonsAleatoires_ReturnsListOfSongs()
            {
                // Arrange
                var controller = CreateController();
                const int numberOfSongs = 5;

                // Act
                var result = controller.AvoirChansonsAleatoires(numberOfSongs);

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<List<Song>>(result);
                Assert.Equal(numberOfSongs, result.Count);
                // You can add more specific assertions based on your requirements.
            }

            [Fact]
            public void AvoirChansonDuGenre_ReturnsSongByGenre()
            {
                // Arrange
                var controller = CreateController();
                const string genre = "Chill"; // Change to other genres for additional tests

                // Act
                var result = controller.AvoirChansonDuGenre(genre);

                // Assert
                Assert.NotNull(result);
                Assert.Single(result); // Expecting one song
                Assert.Equal(genre, result.First().Style);
                // You can add more specific assertions based on your requirements.
            }

            [Fact]
            public void AjouterChanson_AddsSongToList()
            {
                // Arrange
                var controller = CreateController();
                const string titre = "NewSong";
                const string artiste = "NewArtist";
                const string genre = "NewGenre";

                // Act
                var result = controller.AjouterChanson(titre, artiste, genre);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(artiste, result.Artist);
                Assert.Equal(titre, result.Name);
                Assert.Equal(genre, result.Style);

                // You can add more specific assertions based on your requirements for adding songs.
            }

            [Fact]
            public void AvoirChansonsAjoutees_ReturnsListOfAddedSongs()
            {
                // Arrange
                var controller = CreateController();
            }
}
    }
}