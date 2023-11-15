using AlbertCollection.Application.Cache;
using AlbertCollection.Application.Services.Driver.Dto;
using Furion.Logging.Extensions;

namespace AlbertCollection.Application.Services.GatewayConfiguration;

/// <summary>
/// 移动数据
/// </summary>
public class RemoveDataFromOtherCompanyService: ISingleton
{

    private readonly ICacheRedisService _cacheService;

    /// <summary>
    /// 移动数据
    /// </summary>
    /// <param name="cacheService"></param>
    public RemoveDataFromOtherCompanyService(ICacheRedisService cacheService)
    {
        _cacheService = cacheService;
    }

    /// <summary>
    /// 开始搬移数据，会有一个版本号
    /// </summary>
    /// <returns></returns>
    public async Task RemoveData(string otherSqlName)
    {
        // 1. 先拿到缓存的最大自增 id，这些数据是需要全部放入到数据库中的
        var maxId = _cacheService?.Get(otherSqlName);
        int line = 0;

        try
        {
            switch (otherSqlName)
            {
                case CacheConst.OtherSql240:
                    var op240Data = DbContext.Db
                        .GetConnectionScope(otherSqlName)
                        .Queryable<tbl_record_data_240>()
                        .AS("tbl_record_data")
                        .Where(x => x.ID > maxId.ToInt(1))
                        .ToList();

                    // 2. 更新数据到本地数据库
                   line = DbContext.Db.Insertable(op240Data).ExecuteCommand();

                    if (line > 0)
                    {
                        // 3.如果更新成功拿到这边 ID 最大的更新缓存值
                        var maxIdUpdate = op240Data.OrderByDescending(x => x.ID).First()?.ID;
                        _cacheService?.AddObject(otherSqlName, maxIdUpdate);
                    }
                    else
                    {
                        $"{otherSqlName}更新数据库失败".LogError();
                    }
                    break;
                case CacheConst.OtherSql250:
                    var op250Data = DbContext.Db
                        .GetConnectionScope(otherSqlName)
                        .Queryable<tbl_record_data_250>()
                        .AS("tbl_record_data")
                        .Where(x => x.ID > maxId.ToInt(1))
                        .ToList();

                    // 2. 更新数据到本地数据库
                    line = DbContext.Db.Insertable<tbl_record_data_240>(op250Data).ExecuteCommand();

                    if (line > 0)
                    {
                        // 3.如果更新成功拿到这边 ID 最大的更新缓存值
                        var maxIdUpdate = op250Data.OrderByDescending(x => x.ID).First()?.ID;
                        _cacheService?.AddObject(otherSqlName, maxIdUpdate);
                    }
                    else
                    {
                        $"{otherSqlName}更新数据库失败".LogError();
                    }
                    break;
                case CacheConst.OtherSql290:
                    var op290Data = DbContext.Db
                        .GetConnectionScope(otherSqlName)
                        .Queryable<tbl_record_data_290>()
                        .AS("tbl_record_data")
                        .Where(x => x.ID > maxId.ToInt(1))
                        .ToList();

                    // 2. 更新数据到本地数据库
                    line = DbContext.Db.Insertable<tbl_record_data_240>(op290Data).ExecuteCommand();

                    if (line > 0)
                    {
                        // 3.如果更新成功拿到这边 ID 最大的更新缓存值
                        var maxIdUpdate = op290Data.OrderByDescending(x => x.ID).First()?.ID;
                        _cacheService?.AddObject(otherSqlName, maxIdUpdate);
                    }
                    else
                    {
                        $"{otherSqlName}更新数据库失败".LogError();
                    }
                    break;
                case CacheConst.OtherSql300:
                    var op300Data = DbContext.Db
                        .GetConnectionScope(otherSqlName)
                        .Queryable<tbl_record_data_300>()
                        .AS("tbl_record_data")
                        .Where(x => x.ID > maxId.ToInt(1))
                        .ToList();

                    // 2. 更新数据到本地数据库
                    line = DbContext.Db.Insertable<tbl_record_data_240>(op300Data).ExecuteCommand();

                    if (line > 0)
                    {
                        // 3.如果更新成功拿到这边 ID 最大的更新缓存值
                        var maxIdUpdate = op300Data.OrderByDescending(x => x.ID).First()?.ID;
                        _cacheService?.AddObject(otherSqlName, maxIdUpdate);
                    }
                    else
                    {
                        $"{otherSqlName}更新数据库失败".LogError();
                    }
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
           ex.Message.LogError();
        }
    }
}