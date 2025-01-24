using Models;

namespace RestauranteAPI.Repositories
{
    public interface ILineaDePedidoRepository
    {
        Task<List<LineaPedido>> GetAllAsync();
        Task<LineaPedido?> GetByIdAsync(int id);
        Task AddAsync(LineaPedido lineaPedido);
        Task UpdateAsync(LineaPedido Compra);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
