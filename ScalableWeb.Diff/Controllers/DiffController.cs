using Microsoft.AspNetCore.Mvc;
using ScalableWeb.Diff.Application.Services;
using ScalableWeb.Diff.Model.Enums;
using System;

namespace ScalableWeb.Diff.Api.Controllers
{
    [Produces("application/json")]
    [Route("v1/diff")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]

    public class DiffController : Controller
    {
        private readonly IDiffService _diffService;

        public DiffController(IDiffService diffService)
        {
            _diffService = diffService;
        }

        /// <summary>
        /// Compare left-side and right-side content to return if they are equal, different in size or provide insight into where diffs are.
        /// </summary>
        /// <param name="id">Id of the comparer</param>
        /// <remarks>
        /// Sample request:
        ///
        /// GET diff/1        
        ///
        /// </remarks>
        /// <response code="200">Returns the diff comparer result</response>
        /// <response code="404">Content not found</response>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return Ok(_diffService.Get(id));
            }
            catch (ArgumentNullException aex)
            {
                return NotFound(aex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Sets the left-side content of a comparer.
        /// </summary>
        /// <param name="id">Id of the comparer</param>
        /// <param name="content">A base64 encoded binary data content that must be send in the request's body.</param>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT diff/1/left        
        ///
        /// </remarks>
        /// <response code="200">Content set</response>
        /// <response code="400">Content is null, empty or wrong type</response>
        [HttpPut("{id}/left")]
        public IActionResult Left(int id, [FromBody] string content)
        {
            try
            {
                return Ok(_diffService.SetContent(id, content, Side.Left));
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Sets the right-side content of a comparer.
        /// </summary>
        /// <param name="id">Id of the comparer</param>
        /// <param name="content">A base64 encoded binary data content that must be send in the request's body.</param>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT diff/1/right        
        ///
        /// </remarks>
        /// <response code="200">Content set</response>
        /// <response code="400">Content is null, empty or wrong type</response>
        [HttpPut("{id}/right")]
        public IActionResult Right(int id, [FromBody] string content)
        {
            try
            {
                return Ok(_diffService.SetContent(id, content, Side.Right));
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}