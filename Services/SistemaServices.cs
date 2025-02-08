using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Registrotecnico.DAL;
using Registrotecnico.Models;

public class SistemaServices
{
    private readonly IDbContextFactory<Contexto> DbFactory;

    public SistemaServices(IDbContextFactory<Contexto> dbFactory)
    {
        DbFactory = dbFactory;
    }

    // Método Existe
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.sistemas
            .AnyAsync(s => s.SistemaId == id);
    }

    // Método para verificar si un sistema con la misma descripción ya está registrado
    public async Task<bool> ExisteDescripcionSistema(string descripcion, int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.sistemas
            .AnyAsync(s => s.Descripcion.ToLower().Equals(descripcion.ToLower()) && s.SistemaId != id);
    }

    // Método Insertar
    private async Task<bool> Insertar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.sistemas.Add(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    // Método Modificar
    private async Task<bool> Modificar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    // Método Guardar
    public async Task<bool> Guardar(Sistemas sistema)
    {
        if (!await Existe(sistema.SistemaId))
            return await Insertar(sistema);
        else
            return await Modificar(sistema);
    }
    public async Task<int> ObtenerSiguienteId()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        int maxId = await contexto.sistemas.MaxAsync(s => (int?)s.SistemaId) ?? 0;
        return maxId + 1;
    }

    // Método Eliminar
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var eliminado = await contexto.sistemas
            .Where(s => s.SistemaId == id)
            .ExecuteDeleteAsync();
        return eliminado > 0;
    }

    // Método Buscar
    public async Task<Sistemas?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.sistemas
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SistemaId == id);
    }

    // Método Listar con filtro
    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.sistemas
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    // Método Listar todos los sistemas
    public async Task<List<Sistemas>> ListarSistemas()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.sistemas
            .AsNoTracking()
            .ToListAsync();
    }
}
