using Task1_T.Models.Dtos.Positions;

namespace Task1_T.Services.Positions
{
    public interface IPositionService
    {
        Task<PositionGetAllResponse> GetPositionsAsync();
        Task<PositionGetResponse> GetPositionByIdAsync(int positionId);
        Task<PositionGetResponse> CreatePositionAsync(SavePositionRequest request);
        Task UpdatePositionAsync(int positionId, SavePositionRequest request);
        Task DeletePositionAsync(int positionId);
    }
}
