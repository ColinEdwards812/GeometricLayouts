using System;
using System.Drawing;
using GeometricLayouts.Controllers;
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

        public ValidationTests()
        {
           _controller = new TriangleController(new TriangleMapCalculator());
        }

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

            //Check Left facing
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
            const int WIDTH = 12;

            Point a = new Point(WIDTH, 0);
            Point b = new Point(WIDTH, WIDTH);
            Point c = new Point(0, 0);
            
            //Check Right facing
            Triangle triangleRight = new Triangle(a,b,c);
            ////Act
            ActionResult<string> response = _controller.Post(triangleRight);
            /// Assert
            Assert.IsInstanceOfType(response.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void When_LeftTriangleInputWidthIsValid_ReturnsRequestOk()
        {
            ////Arrange
            const int WIDTH = 10;

            Point a = new Point(0, WIDTH);
            Point b = new Point(0, 0);
            Point c = new Point(WIDTH, WIDTH);

            //Check Left facing
            Triangle triangleLeft = new Triangle(a, b, c);
            ////Act
            ActionResult<string> response = _controller.Post(triangleLeft);
            /// Assert
            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void When_RightTriangleInputWidthIsValid_ReturnsRequestOk()
        {
            ////Arrange
            const int WIDTH = 10;

            Point a = new Point(WIDTH, 0);
            Point b = new Point(WIDTH, WIDTH);
            Point c = new Point(0, 0);

            //Check Right facing
            Triangle triangleRight = new Triangle(a, b, c);
            ////Act
            ActionResult<string> response = _controller.Post(triangleRight);
            /// Assert
            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));
        }
    }
}
