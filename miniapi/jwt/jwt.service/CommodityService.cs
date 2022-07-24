using Microsoft.EntityFrameworkCore;

namespace jwt.service;

public interface ICommodityService : IBaseService
{
}
public class CommodityService : BaseService, ICommodityService
{
    public CommodityService(DbContext context) : base(context)
    {
    }
}
