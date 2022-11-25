using Microsoft.AspNetCore.Mvc;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.Positions;
using Task1_T.PermissionSet;
using Task1_T.Routes;
using Task1_T.Services.Positions;

namespace Task1_T.Controllers
{
    public class PositionController : BaseController
    {
        private readonly IPositionService _positionService;
        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet(ApiRoutes.Position.GetAll)]
        [ClaimRequirementFilter(PermissionNames.Position.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _positionService.GetPositionsAsync());
        }


        [HttpGet(ApiRoutes.Position.Get)]
        [ClaimRequirementFilter(PermissionNames.Position.Get)]
        public async Task<IActionResult> Get(int positionId)
        {
            return Ok(await _positionService.GetPositionByIdAsync(positionId));
        }


        [HttpPost(ApiRoutes.Position.Create)]
        [ClaimRequirementFilter(PermissionNames.Position.Create)]
        public async Task<IActionResult> Create(SavePositionRequest request)
        {
            return Created(string.Empty, await _positionService.CreatePositionAsync(request));
        }


        [HttpPut(ApiRoutes.Position.Update)]
        [ClaimRequirementFilter(PermissionNames.Position.Update)]
        public async Task<IActionResult> Update(int positionId, SavePositionRequest request)
        {
            await _positionService.UpdatePositionAsync(positionId, request);
            return Ok();
        }


        [HttpDelete(ApiRoutes.Position.Delete)]
        [ClaimRequirementFilter(PermissionNames.Position.Delete)]
        public async Task<IActionResult> Delete(int positionId)
        {
            await _positionService.DeletePositionAsync(positionId);
            return NoContent();
        }
    }
}
