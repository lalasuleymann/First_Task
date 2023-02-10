using static Task1_T.Extensions.ClaimRequirementFilter;
using Microsoft.AspNetCore.Mvc;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.Positions;
using Task1_T.Routes;
using Task1_T.Services.Positions;
using Task1_T.Constants;

namespace Task1_T.Controllers
{
    [Authorize(PositionPermissions.Position)]
    public class PositionController : BaseController
    {
        private readonly IPositionService _positionService;
        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet(ApiRoutes.Position.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var a = await _positionService.GetPositionsAsync();
            return Ok(a);
        }


        [HttpGet(ApiRoutes.Position.Get)]
        public async Task<IActionResult> Get(int positionId)
        {
            return Ok(await _positionService.GetPositionByIdAsync(positionId));
        }


        [HttpPost(ApiRoutes.Position.Create)]
        [Authorize(PositionPermissions.PositionCreate)]
        public async Task<IActionResult> Create(SavePositionRequest request)
        {
            return Created(string.Empty, await _positionService.CreatePositionAsync(request));
        }


        [HttpPut(ApiRoutes.Position.Update)]
        [Authorize(PositionPermissions.PositionUpdate)]
        public async Task<IActionResult> Update(int positionId, SavePositionRequest request)
        {
            await _positionService.UpdatePositionAsync(positionId, request);
            return Ok();
        }


        [HttpDelete(ApiRoutes.Position.Delete)]
        [Authorize(PositionPermissions.PositionDelete)]
        public async Task<IActionResult> Delete(int positionId)
        {
            await _positionService.DeletePositionAsync(positionId);
            return NoContent();
        }
    }
}
