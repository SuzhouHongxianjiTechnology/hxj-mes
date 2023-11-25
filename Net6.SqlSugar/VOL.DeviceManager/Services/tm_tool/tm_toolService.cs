/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下tm_toolService与Itm_toolService中编写
 */
using VOL.DeviceManager.IRepositories;
using VOL.DeviceManager.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Services
{
    public partial class tm_toolService : ServiceBase<tm_tool, Itm_toolRepository>
    , Itm_toolService, IDependency
    {
    public tm_toolService(Itm_toolRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static Itm_toolService Instance
    {
      get { return AutofacContainerModule.GetService<Itm_toolService>(); } }
    }
 }
