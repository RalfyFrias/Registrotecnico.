using Microsoft.EntityFrameworkCore;
using Registrotecnico.DAL;
using Registrotecnico.Models;
using System.Linq.Expressions;

namespace Registrotecnico.Services
{
    public class CiudadServices
    {
        
            private readonly Contexto _contexto;

            public CiudadServices(Contexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<bool> Existe(int CiudadId)
            {
                return await _contexto.Ciudad.AnyAsync(t => t.CiudadId == CiudadId);
            }

            private async Task<bool> Insertar(Ciudades ciudad)
            {
                _contexto.Ciudad.Add(ciudad);
                return await _contexto.SaveChangesAsync() > 0;
            }

            private async Task<bool> Modificar(Ciudades ciudad)
            {
                _contexto.Ciudad.Update(ciudad);
                var modificado = await _contexto.SaveChangesAsync() > 0;
                _contexto.Entry(ciudad).State = EntityState.Detached;
                return modificado;
            }

            public async Task<bool> Guardar(Ciudades ciudades)
            {
                if (!await Existe(ciudades.CiudadId))
                {
                    return await Insertar(ciudades);
                }
                else
                {
                    return await Modificar(ciudades);
                }
            }

            public async Task<bool> Eliminar(int id)
            {
                var eliminado = await _contexto.Ciudad.Where(t => t.CiudadId == id).ExecuteDeleteAsync();
                return eliminado > 0;
            }

            public async Task<Ciudades?> Buscar(int id)
            {
                return await _contexto.Ciudad.AsNoTracking().FirstOrDefaultAsync(t => t.CiudadId == id);
            }

            public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> Criterio)
            {
                return await _contexto.Ciudad
                .Where(Criterio)
                .ToListAsync();
            }
        }

    }

