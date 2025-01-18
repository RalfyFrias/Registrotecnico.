using Microsoft.EntityFrameworkCore;
using Registrotecnico.DAL;
using Registrotecnico.Models;
using System.Linq.Expressions;

namespace Registrotecnico.Services
{
    public class TecnicoServices(IDbContextFactory<Contexto> DbFactory)
    {
        //Metodo Existe
        public async Task<bool> Existe(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .AnyAsync(t => t.TecnicoId == id);
        }
        //Metodo el cual Nos Identifica si ese tecnico esta registrado en la base de datos
        public async Task<bool> ExisteNombreTecnico(string nombre, int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .AnyAsync(t => t.Nombres.ToLower().Equals(nombre.ToLower()) && t.TecnicoId != id);
        }
        //Metodo Insertar
        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tecnico.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }
        //Metodo Modificar 
        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }
        //Metodo Guardar
        public async Task<bool> Guardar(Tecnicos tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            if (!await Existe(tecnico.TecnicoId))

                return await Insertar(tecnico);
            else
                return await Modificar(tecnico);
        }
        //Metodo Eliminar 
        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var EliminarTecnico = await contexto.Tecnico
                .Where(t => t.TecnicoId == id)
                .ExecuteDeleteAsync();
            return EliminarTecnico > 0;
        }
        //Metodo Buscar 
        public async Task<Tecnicos?> Buscar(int id)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TecnicoId == id);
        }
        //Metodo Listar
        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<List<Tecnicos>> ListarTecnicos()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .AsNoTracking()
                .ToListAsync();
        }

    }
}

