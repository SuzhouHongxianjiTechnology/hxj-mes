/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下bs_coderuleService与Ibs_coderuleService中编写
 */
using VOL.BasicConfig.IRepositories;
using VOL.BasicConfig.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.BasicConfig.Services
{
    public partial class bs_coderuleService : ServiceBase<bs_coderule, Ibs_coderuleRepository>
    , Ibs_coderuleService, IDependency
    {
    public bs_coderuleService(Ibs_coderuleRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static Ibs_coderuleService Instance
    {
      get { return AutofacContainerModule.GetService<Ibs_coderuleService>(); } }
    }
 }
