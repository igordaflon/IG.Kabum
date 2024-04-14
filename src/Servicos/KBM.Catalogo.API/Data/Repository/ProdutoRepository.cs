using KBM.Catalogo.API.Models;
using KBM.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace KBM.Catalogo.API.Data.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly CatalogoContext _context;

    public ProdutoRepository(CatalogoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Produto> ObterPorId(Guid id)
    {
        return await _context.Produto.FindAsync(id);
    }

    public async Task<IEnumerable<Produto>> ObterTodos()
    {
        return await _context.Produto.AsNoTracking().ToListAsync(); 
    }

    public void Adicionar(Produto produto)
    {
        _context.Produto.Add(produto);
    }

    public void Atualizar(Produto produto)
    {
        _context.Produto.Update(produto);
    }

    public void Dispose() => _context?.Dispose();
}
