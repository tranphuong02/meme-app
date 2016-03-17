using Framework.Logger.Log4Net;
using System;
using System.Linq;
using System.Net;
using Transverse;
using Transverse.Interfaces.DAL;
using Transverse.Models.Business.Author;
using Transverse.Models.DAL;
using BaseModel = Transverse.Models.Business.BaseModel;

namespace DatabaseAccess
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public BaseModel GetAll()
        {
            try
            {
                var data = GetAll(x => x.IsDeleted == false, x => x.OrderBy(o => o.Name))
                        .Select(x => new AuthorViewModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Summary = x.Summary,
                            Country = x.Country,
                            Resource = x.Resource.Url
                        }).ToList();

                if (data.Any() == false)
                {
                    return new BaseModel(false, (int) HttpStatusCode.NoContent, string.Format(Constants.Message.NoData, "author"));
                }

                return new BaseModel(true, (int) HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new BaseModel(false, (int)HttpStatusCode.InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}