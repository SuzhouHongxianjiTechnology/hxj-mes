#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

namespace AlbertCollection.Application
{
    /// <inheritdoc cref="IRelationService"/>
    public class RelationService : DbRepository<SysRelationAc>, IRelationService
    {
        private readonly IResourceService _resourceService;
        private readonly SysCacheService _sysCacheService;

        /// <inheritdoc cref="IRelationService"/>
        public RelationService(SysCacheService sysCacheService, IResourceService resourceService)
        {
            _sysCacheService = sysCacheService;
            this._resourceService = resourceService;
        }

        /// <inheritdoc/>
        public async Task<List<SysRelationAc>> GetRelationByCategory(string category)
        {
            //先从Cache拿
            var sysRelations = _sysCacheService.Get<List<SysRelationAc>>(CacheConst.Cache_SysRelation, category);
            if (sysRelations == null)
            {
                //cache没有就去数据库拿
                sysRelations = await GetListAsync(it => it.Category == category);
                if (sysRelations.Count > 0)
                {
                    //插入Cache
                    _sysCacheService.Set(CacheConst.Cache_SysRelation, category, sysRelations);
                }
            }
            return sysRelations;
        }

        /// <inheritdoc/>
        public async Task<List<SysRelationAc>> GetRelationListByObjectIdAndCategory(long objectId, string category)
        {
            var sysRelations = await GetRelationByCategory(category);
            var result = sysRelations.Where(it => it.ObjectId == objectId).ToList();//获取关系集合
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<SysRelationAc>> GetRelationListByObjectIdListAndCategory(List<long> objectIds, string category)
        {
            var sysRelations = await GetRelationByCategory(category);

            var result = sysRelations.Where(it => objectIds.Contains(it.ObjectId)).ToList();//获取关系集合
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<SysRelationAc>> GetRelationListByTargetIdAndCategory(string targetId, string category)
        {
            var sysRelations = await GetRelationByCategory(category);
            var result = sysRelations.Where(it => it.TargetId == targetId).ToList();//获取关系集合
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<SysRelationAc>> GetRelationListByTargetIdListAndCategory(List<string> targetIds, string category)
        {
            var sysRelations = await GetRelationByCategory(category);
            var result = sysRelations.Where(it => targetIds.Contains(it.TargetId)).ToList();//获取关系集合
            return result;
        }

        /// <inheritdoc/>
        public async Task RefreshCache(string category)
        {
            _sysCacheService.Remove(CacheConst.Cache_SysRelation, category);//删除cache
            await GetRelationByCategory(category);//更新缓存
        }

        /// <inheritdoc/>
        public async Task SaveRelation(string category, long objectId, string targetId, string extJson, bool clear, bool refreshCache = true)
        {
            var sysRelation = new SysRelationAc
            {
                ObjectId = objectId,
                TargetId = targetId,
                Category = category,
                ExtJson = extJson
            };
            //事务
            var result = await itenant.UseTranAsync(async () =>
            {
                if (clear)
                    await DeleteAsync(it => it.ObjectId == objectId && it.Category == category);//删除老的
                await InsertAsync(sysRelation);//添加新的
            });
            if (result.IsSuccess)//如果成功了
            {
                if (refreshCache)
                    await RefreshCache(category);
            }
            else
            {
                //写日志
                throw Oops.Oh(ErrorCodeEnum.A0003);
            }
            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task SaveRelationBatch(string category, long objectId, List<string> targetIds, List<string> extJsons, bool clear)
        {
            var sysRelations = new List<SysRelationAc>();//要添加的列表
            for (int i = 0; i < targetIds.Count; i++)
            {
                sysRelations.Add(new SysRelationAc
                {
                    ObjectId = objectId,
                    TargetId = targetIds[i],
                    Category = category,
                    ExtJson = extJsons == null ? null : extJsons[i]
                });
            }

            var result = await itenant.UseTranAsync(async () =>
            {
                if (clear)
                    await DeleteAsync(it => it.ObjectId == objectId && it.Category == category);//删除老的
                await InsertRangeAsync(sysRelations);//添加新的
            });
            if (result.IsSuccess)//如果成功了
            {
                await RefreshCache(category);
            }
            else
            {
                throw Oops.Oh(ErrorCodeEnum.A0003);
            }
            await Task.CompletedTask;
        }


        /// <inheritdoc/>
        public async Task<SysRelationAc> GetWorkbench(long userId)
        {
            var sysRelations = await GetRelationByCategory(CateGoryConst.Relation_SYS_USER_WORKBENCH_DATA);
            var result = sysRelations.Where(it => it.ObjectId == userId).FirstOrDefault();//获取个人工作台
            return result;

        }

    }
}