using LibrarySystemData.Repositories;
using LibrarySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService
{
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository _genresRepository;
        public GenresService(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }

        public List<Genre> GetAll()
        {
            return _genresRepository.TakeAll().Result;
        }

        public void Add(Genre genre)
        {
            _genresRepository.AddData(genre);
        }

        public void Save()
        {
            _genresRepository.SaveData();
        }

        public Genre GetById(int id)
        {
            return _genresRepository.FindById(id).Result;
        }

        public void Delete(Genre genre)
        {
            _genresRepository.DeleteData(genre);
        }

        public void Update(Genre genre)
        {
            _genresRepository.UpdateData(genre);
        }
    }

    public interface IGenresService
    {
        List<Genre> GetAll();
        void Add(Genre genre);
        void Update(Genre genre);
        void Save();
        Genre GetById(int id);
        void Delete(Genre genre);
    }
}
