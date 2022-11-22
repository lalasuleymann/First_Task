using AutoMapper;
using Task1_T.Models.Dtos.Positions;
using Task1_T.Models.Entities;
using Task1_T.Repositories;
using Task1_T.UnitOfWork;

namespace Task1_T.Services.Positions
{
    public class PositionManager : IPositionService
    {
        private readonly IUnitOfWorkService _unitOfWork;

        private readonly IMapper _mapper;
        public PositionManager(IUnitOfWorkService unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PositionGetResponse> GetPositionByIdAsync(int positionId)
        {
            var response = new PositionGetResponse();
            var item = await _unitOfWork.Positions.GetFirstOrDefaultAsync(x => x.Id == positionId);
            response.PositionDto = _mapper.Map<PositionDto>(item);
            return response;
        }
        
        public async Task<PositionGetAllResponse> GetPositionsAsync()
        {
            var response = new PositionGetAllResponse();
            var entities = await _unitOfWork.Positions.GetAllAsync();
            response.PositionDtos = _mapper.Map<List<PositionDto>>(entities);
            return response;
        }

        public async Task<PositionGetResponse> CreatePositionAsync(SavePositionRequest request)
        {
            PositionGetResponse response = new();

            Position entity = new Position
            {
                Name = request.Name
            };

            var addedEntity = await _unitOfWork.Positions.AddAsync(entity);
            if (addedEntity == null)
            {
                throw new Exception("Data could not be added!");
            }
            _unitOfWork.Complete();

            var dto = _mapper.Map<PositionDto>(addedEntity);
            response.PositionDto = dto;
            return response;
        }

        public async Task UpdatePositionAsync(int positionId, SavePositionRequest request)
        {
            var item = await _unitOfWork.Positions.GetFirstOrDefaultAsync(x => x.Id == positionId);

            item.Name = request.Name;
            await _unitOfWork.Positions.UpdateAsync(item);
            _unitOfWork.Complete();
        }

        public async Task DeletePositionAsync(int positionId)
        {
            var entity = await _unitOfWork.Positions.GetFirstOrDefaultAsync(x => x.Id == positionId);
            await _unitOfWork.Positions.DeleteAsync(entity);
            _unitOfWork.Complete();
        }
    }
}
