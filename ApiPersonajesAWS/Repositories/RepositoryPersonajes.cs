using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesAWS.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }

        private async Task<int> GetMaxIdPersonajesAsync()
        {
            return await this.context.Personajes.MaxAsync(x => x.IdPersonaje) + 1;
        }

        public async Task CreatePersonajeAsync(string nombre, string imagen)
        {
            Personaje personaje = new Personaje();
            personaje.IdPersonaje = await this.GetMaxIdPersonajesAsync();
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;

            await this.context.Personajes.AddAsync(personaje);
            await this.context.SaveChangesAsync();
        }

        public async Task<Personaje> FindPersonajeAsync(int idPersonaje)
        {
            return await this.context.Personajes
                .FirstOrDefaultAsync(x => x.IdPersonaje == idPersonaje);
        }


        public async Task UpdatePersonajeAsync(int idPersonaje, string nombre, string imagen)
        {
            Personaje personaje = await this.context.Personajes
                .FirstOrDefaultAsync(x => x.IdPersonaje == idPersonaje);
                        
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            await this.context.SaveChangesAsync();
            
        }
    }
}
