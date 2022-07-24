using System.Linq.Expressions;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace jwt.service;

public class PageResult<T> where T : class
{
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public List<T>? DataList { get; set; }
}

public interface IBaseService : IDisposable
{
    T Find<T>(int id) where T : class;
    IQueryable<T> Set<T>() where T : class;
    IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class;
    PageResult<T> QueryPage<T, S>(Expression<Func<T, bool>> funcWhere,
    int pageSize, int pageIndex,
    Expression<Func<T, S>> funcOrderBy, bool isAsc = true) where T : class;
    T Insert<T>(T t) where T : class;
    IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class;
    void Update<T>(T t) where T : class;
    void Update<T>(IEnumerable<T> tList) where T : class;
    void Delete<T>(int id) where T : class;
    void Delete<T>(T t) where T : class;
    void Delete<T>(IEnumerable<T> tList) where T : class;
    void Commit();
    IQueryable<T> ExecuteQuery<T>(string sql, SqlParameter[] parameters) where T : class;
    void Excute<T>(string sql, SqlParameter[] parameters) where T : class;
}

public abstract class BaseService : IBaseService
{
    protected DbContext Context { get; private set; } = null!;
    public BaseService(DbContext context)
    {
        this.Context = context;
    }
    public T Find<T>(int id) where T : class
    {
        return Context.Set<T>().Find(id)!;
    }

    public IQueryable<T> Set<T>() where T : class
    {
        return Context.Set<T>();
    }
    public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
    {
        if (funcWhere == null)
            return Context.Set<T>();
        else
            return Context.Set<T>().Where<T>(funcWhere);
    }

    public PageResult<T> QueryPage<T, S>(Expression<Func<T, bool>> funcWhere,
    int pageSize, int pageIndex,
    Expression<Func<T, S>> funcOrderBy, bool isAsc = true) where T : class
    {
        var list = Set<T>();
        if (funcWhere != null)
        {
            list = list.Where(funcWhere);
        }
        if (isAsc)
        {
            list = list.OrderBy(funcOrderBy);
        }
        else
        {
            list = list.OrderByDescending(funcOrderBy);
        }
        var result = new PageResult<T>
        {
            DataList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalCount = list.Count()
        };

        return result;
    }

    public T Insert<T>(T t) where T : class
    {
        Context.Set<T>().Add(t);
        Commit();
        return t;
    }

    public IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class
    {
        Context.Set<T>().AddRange(tList);
        Commit();
        return tList;
    }

    public void Update<T>(T t) where T : class
    {
        if (t == null) throw new Exception("t is null");
        Context.Set<T>().Attach(t);
        Context.Entry<T>(t).State = EntityState.Modified;
        Commit();
    }

    public void Update<T>(IEnumerable<T> tList) where T : class
    {
        foreach (var t in tList)
        {
            Context.Set<T>().Attach(t);
            Context.Entry<T>(t).State = EntityState.Modified;
        }
        Commit();
    }

    public void Delete<T>(int id) where T : class
    {
        var t = Context.Set<T>().Find(id);
        if (t == null) throw new Exception("id is not existed.");
        Context.Set<T>().Remove(t);
        Commit();
    }

    public void Delete<T>(T t) where T : class
    {
        if (t == null) throw new Exception("t is null");
        Context.Set<T>().Attach(t);
        Context.Set<T>().Remove(t);
        Commit();
    }

    public void Delete<T>(IEnumerable<T> tList) where T : class
    {
        foreach (var t in tList)
        {
            Context.Set<T>().Attach(t);
            Context.Set<T>().Remove(t);
        }
        Commit();
    }
    public IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class
    {
        throw new NotImplementedException();
    }

    public void Excute<T>(string sql, SqlParameter[] parameters) where T : class
    {
        throw new NotImplementedException();
    }
    public void Commit()
    {
        Context.SaveChanges();
    }

    public IQueryable<T> ExecuteQuery<T>(string sql, SqlParameter[] parameters) where T : class
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}