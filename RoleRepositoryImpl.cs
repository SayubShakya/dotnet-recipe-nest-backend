
using Mapper.Implementation;
using Model;
using RecipeNest.Util.Database;

namespace Repository.Implementation
{

    public class RoleRepositoryImpl : RoleRepository
    {

        private DatabaseUtil databaseUtil;

        public RoleRepositoryImpl(DatabaseUtil databaseUtil)
        {
            this.databaseUtil = databaseUtil;
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAll()
        {
            return databaseUtil.Query("SELECT * FROM roles", new RoleRowMapper());
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Role role)
        {
            throw new NotImplementedException();
        }

        public bool Update(Role role)
        {
            throw new NotImplementedException();
        }

        
    }
}