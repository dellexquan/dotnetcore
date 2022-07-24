using Microsoft.EntityFrameworkCore;

namespace jwt.service;

public interface ICompanyService : IBaseService
{

}
public class CompanyService : BaseService, ICompanyService
{
    public CompanyService(DbContext context) : base(context)
    {
    }
}
