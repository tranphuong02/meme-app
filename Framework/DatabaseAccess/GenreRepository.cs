using Framework.Logger.Log4Net;
using System;
using System.Linq;
using System.Net;
using Transverse;
using Transverse.Interfaces.DAL;
using Transverse.Models.Business.Genre;
using Transverse.Models.DAL;
using BaseModel = Transverse.Models.Business.BaseModel;

namespace DatabaseAccess
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public BaseModel GetAll()
        {
            try
            {
                var data = GetAll(x => x.IsDeleted == false, x => x.OrderBy(o => o.Title))
                        .Select(x => new GenreViewModel
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Url = x.Url,
                            Keyword = x.Keyword,
                            Description = x.Description,
                            ViewCount = x.ViewCount,
                            Resource = x.Resource.Url,
                            Categories = x.GenreCategories.Where(y => y.Category.IsDeleted == false).Select(y=>y.Category.Title),
                        }).ToList();

                if (data.Any() == false)
                {
                    return new BaseModel(false, (int) HttpStatusCode.NoContent, string.Format(Constants.Message.NoData, "genre"));
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