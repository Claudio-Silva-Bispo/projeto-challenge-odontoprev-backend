using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserApi.Models;

namespace UserApi.Services
{
    public class FormularioDetalhadoService : IFormularioDetalhadoService
    {
        private readonly IMongoCollection<FormularioDetalhado> _formularioDetalhadoCollection;

        public FormularioDetalhadoService(IMongoClient mongoClient, IOptions<UserDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _formularioDetalhadoCollection = database.GetCollection<FormularioDetalhado>(settings.Value.FormularioDetalhadoCollectionName);
        }

        public async Task<List<FormularioDetalhado>> GetAll()
        {
            return await _formularioDetalhadoCollection.Find(f => true).ToListAsync();
        }

        public async Task<FormularioDetalhado?> GetById(string id)
        {
            return await _formularioDetalhadoCollection.Find(f => f.id_formulario == id).FirstOrDefaultAsync();
        }

        public async Task<FormularioDetalhado> Create(FormularioDetalhado formularioDetalhado)
        {
            await _formularioDetalhadoCollection.InsertOneAsync(formularioDetalhado);
            return formularioDetalhado;
        }

        public async Task Update(string id, FormularioDetalhado formularioDetalhado)
        {
            await _formularioDetalhadoCollection.ReplaceOneAsync(f => f.id_formulario == id, formularioDetalhado);
        }

        public async Task Delete(string id)
        {
            await _formularioDetalhadoCollection.DeleteOneAsync(f => f.id_formulario == id);
        }
    }
}
