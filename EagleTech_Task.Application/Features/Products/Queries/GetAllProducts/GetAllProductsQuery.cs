using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using EagleTeck_Task.Shared;
using Mapster;
using MediatR;

namespace EagleTech_Task.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<Result<List<GetAllProductsQueryDto>>>;

    internal class GetlAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<GetAllProductsQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetlAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAllProductsQueryDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Repository<Product>().GetAllAsync();

            var products = entities.Adapt<List<GetAllProductsQueryDto>>();

            return Result<List<GetAllProductsQueryDto>>.Success(products);
        }
    }
}
