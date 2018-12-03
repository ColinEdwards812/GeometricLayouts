using System;
using System.Collections.Generic;
using System.Drawing;
using GeometricLayouts.Controllers;
using GeometricLayouts.Interfaces;
using GeometricLayouts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeometricLayoutsTests
{
    [TestClass]
    public class ValidationTests
    {
        private TriangleController _controller;
        private TriangleMapCalculator _triangleMapCalculator;

#region SetUpTests
        public ValidationTests()
        {
            _triangleMapCalculator = new TriangleMapCalculator();
           _controller = new TriangleController(_triangleMapCalculator);
        }

        public List<MapReference> ValidMapValuesForLeftTriangle()
        {
            return new List<MapReference>
            {
                new MapReference('A',1),
                new MapReference('B',1),
                new MapReference('B',3),
                new MapReference('E',5),
                new MapReference('E',7),
                new MapReference('E',11)
            };
        }

        public List<MapReference> ValidMapValuesForRightTriangle()
        {
            return new List<MapReference>
            {
                new MapReference('A',2),
                new MapReference('B',2),
                new MapReference('B',6),
                new MapReference('D',8),
                new MapReference('F',8),
                new MapReference('E',10)
            };
        }

#endregion SetUpTests

        [TestMethod]
        public void When_MapReferenceInputIsInvalid_ReturnsBadRequest()
        {
            ////Arrange
            string[] invalidMapValues 
                = new string[]{"E1111111","A0","G1","Z","Z11","T6","A13","F13"};

            foreach (var invalidMapValue in invalidMapValues)
            {
                ////Act
                ActionResult<string> response = _controller.Get(invalidMapValue);
                /// Assert
                Assert.IsInstanceOfType(response.Result, typeof(BadRequestObjectResult));
            }
        }

        [TestMethod]
        public void When_MapReferenceInputIsValid_ReturnsRequestOk()
        {
            ////Arrange
            string[] validMapValues
                = new string[] { "A1", "A12", "F1", "F12", "C3", "C6"};

            foreach (var validMapValue in validMapValues)
            {
                ////Act
                ActionResult<string> response = _controller.Get(validMapValue);
                /// Assert
                Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));
            }
        }

        [TestMethod]
        public void When_LeftTriangleInputWidthIsInvalid_ReturnsBadRequest()
        {
            ////Arrange
            const int WIDTH = 12;

            Point a = new Point(0, WIDTH);
            Point b = new Point(0, 0);
            Point c = new Point(WIDTH, WIDTH);

            //Check left facing
            Triangle triangleLeft = new Triangle(a,b,c);
            ////Act
            ActionResult<string> response = _controller.Post(triangleLeft);
            /// Assert
            Assert.IsInstanceOfType(response.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void When_RightTriangleInputWidthIsInvalid_ReturnsBadRequest()
        {
            ////Arrange
            const int WIDTH = 9;

            Point a = new Point(WIDTH, 0);
            Point b = new Point(WIDTH, WIDTH);
            Point c = new Point(0, 0);
            
            //Check right facing
            Triangle triangleRight = new Triangle(a,b,c);
            ////Act
            ActionResult<string> response = _controller.Post(triangleRight);
            /// Assert
            Assert.IsInstanceOfType(response.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void When_LeftFacingTriangle_IsIsoscelesRightTriangle()
        {
            ////Arrange
            List<MapReference> validMapValues = ValidMapValuesForLeftTriangle();
            
            foreach (var leftFacingReference in validMapValues)
            {
                ////Act
                //Check left facing
                Triangle triangle = _triangleMapCalculator
                    .CalculateCoordinatesFromMapReference(leftFacingReference);
                /// Assert
                Assert.IsTrue(triangle.IsIsoscelesRightTriangle());
            }
        }

        [TestMethod]
        public void When_RightFacingTriangle_IsIsoscelesRightTriangle()
        {
            ////Arrange
            List<MapReference> validMapValues = ValidMapValuesForRightTriangle();

            foreach (var rightFacingReference in validMapValues)
            {
                ////Act
                //Check right facing
                Triangle triangle = _triangleMapCalculator
                    .CalculateCoordinatesFromMapReference(rightFacingReference);
                /// Assert
                Assert.IsTrue(triangle.IsIsoscelesRightTriangle());
            }
        }
    }
}
