

namespace Repository
{

    public interface BaseRepository<T>
    {

        bool Save(T obj);

        bool Update(T obj);

        T GetById(int id);

        List<T> GetAll();

        bool DeleteById(int id);
    }
}