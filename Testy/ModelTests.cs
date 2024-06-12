using Logika;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testy
{
    [TestClass]
    public class ModelAbstractTests
    {
        private Mock<Logic> _logicApiMock;
        private ModelAbstract _model;

        [TestInitialize]
        public void Setup()
        {
            _logicApiMock = new Mock<Logic>();
            _model = ModelAbstract.createApi(_logicApiMock.Object);
        }

        [TestMethod]
        public void GetSphereModels_ReturnsObservableCollection()
        {
            // Arrange

            // Act
            var result = _model.getSphereModels();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ObservableCollection<CircleModel>));
        }


        [TestMethod]
        public void Stop_CallsStopOnLogic()
        {
            // Arrange

            // Act
            _model.stop();

            // Assert
            _logicApiMock.Verify(x => x.Stop(), Times.Once);
        }
    }
}
