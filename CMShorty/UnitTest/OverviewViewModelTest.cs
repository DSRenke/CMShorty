namespace UnitTest
{
    using System.Reflection;
    using CMShorty.URLShortModul.Manager;
    using Moq;

    public class OverviewViewModelTest
    {
        /// <summary>
        /// Test OverviewViewModel.IsUrlValide
        /// Es wird getestet ob die Methode IsUrlValide die URL richtig validiert
        /// </summary>
        /// <param name="url"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("https://www.google.de", true)]
        [InlineData("https://www.google.de/", true)]
        [InlineData("https://www.google.de/?q=hallo", true)]
        [InlineData("https://www.google.de/?q=hallo&test=123", true)]
        [InlineData("http://www.google.de/?q=hallo&test=123", false)] // http wird nicht unterstützt
        [InlineData("http://google.de/?q=hallo&test=123", false)] // http wird nicht unterstützt
        [InlineData("google.de/?q=hallo&test=123", true)]
        [InlineData("", false)] // leerstring
        [InlineData(null, false)] // null
        public void TestIsUrlValide(string url, bool expected)
        {
            // Arrange
            var managerMock = new Mock<IShortUrlManager>().Object;
            var viewModel = new CMShorty.URLShortModul.ViewModels.OverviewViewModel(managerMock);

            // Act
            var result = InvokeIsUrlValid(viewModel, url);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Test OverviewViewModel.ShortCommand
        /// Es wird geprüft, ob die Methode IShortUrlManager.RequestShortUrl aufgerufen wird
        /// Oder ob eine NullReferenceException geworfen wird, wenn die URL nicht valide ist
        /// </summary>
        /// <param name="url"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("https://www.google.de", true)]
        [InlineData("https://www.google.de/", true)]
        [InlineData("https://www.google.de/?q=hallo", true)]
        [InlineData("https://www.google.de/?q=hallo&test=123", true)]
        [InlineData("http://www.google.de/?q=hallo&test=123", false)] // http wird nicht unterstützt
        [InlineData("http://google.de/?q=hallo&test=123", false)] // http wird nicht unterstützt
        [InlineData("google.de/?q=hallo&test=123", true)]
        [InlineData("", false)] // leerstring
        [InlineData(null, false)] // null
        public void TestShortCommand(string url, bool expected)
        {
            // Arrange
            var urlManagerMock = new Mock<IShortUrlManager>();
            var viewModel = new CMShorty.URLShortModul.ViewModels.OverviewViewModel(urlManagerMock.Object);

            // Act
            viewModel.EntryURL = url;

            // Assert

            if(expected == true)
            {
                viewModel.ShortCommand.Execute(null);
                urlManagerMock.Verify(m => m.RequestShortUrl(url), expected ? Times.Once : Times.Never);
            }
            else
            {
                Assert.Throws<NullReferenceException>(() => viewModel.ShortCommand.Execute(null));
            }

        }

        /// <summary>
        /// Hilfsmethode um private Method "IsUrlValide" zu testen
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool InvokeIsUrlValid(CMShorty.URLShortModul.ViewModels.OverviewViewModel viewModel, string url)
        {
            var method = typeof(CMShorty.URLShortModul.ViewModels.OverviewViewModel).GetMethod("IsUrlValide", BindingFlags.Instance | BindingFlags.NonPublic);
            return (bool)method.Invoke(viewModel, new object[] { url });
        }
    }
}