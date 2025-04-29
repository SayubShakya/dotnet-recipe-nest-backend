namespace RecipeNest.Repository;

public interface IBaseRepository<T>
{
    bool Save(T obj);

    bool Update(T obj);

    T GetActiveById(int id);

    List<T> GetAll();

    bool DeleteById(int id);
}