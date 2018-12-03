using GeometricLayouts.Interfaces;
using GeometricLayouts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GeometricLayouts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriangleController : ControllerBase
    {
        private ITriangleMapCalculator _triangleMapCalculator;

        public TriangleController(ITriangleMapCalculator triangleMapCalculator)
        {
            _triangleMapCalculator = triangleMapCalculator;
        }

        // GET api/triangle
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Geometric Layouts Technical Test";
        }

        // GET api/triangle/{mapReference}
        [HttpGet("{mapReference}")]
        public ActionResult<string> Get(string mapReference)
        {
            // Only letters A-F followed by 1-12 are valid
            Regex regex = new Regex(@"(^[A-F][1-9]$)|(^[A-F]1[0-2]$)");
            bool valid = regex.IsMatch(mapReference);

            if (valid)
            {
                char letter = mapReference[0];
                int number = int.Parse(mapReference.Remove(0, 1));
                MapReference mapRef = new MapReference(letter, number);
                Triangle triangle = 
                    _triangleMapCalculator.CalculateCoordinatesFromMapReference(mapRef);
                return Ok(triangle);
            }
            else
            {
                return BadRequest("Bad Request: Input should be character then number A-F/1-12");
            }
        }

        // POST api/triangle
        [HttpPost]
        public ActionResult<string> Post([FromBody] Triangle t)
        {
            //Validate Triangle
            if(t.IsIsoscelesRightTriangle())
            { 
                MapReference mapReference = 
                    _triangleMapCalculator.CalculateMapReferenceFromCoordinates(t);
                return Ok(mapReference.ToString());
            }
            else
            {
                return BadRequest("Bad Request: The input vertexes do not create a 10px right triangle");
            }
        }
    }
}
