namespace WarehouseManagament.Models.Mappers
{
    public interface IDtoMapper<TEntity, TDto>
    {
        TDto ToDto(TEntity entity);
        TEntity ToEntity(TDto dto);
    }
}
