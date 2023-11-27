/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下tm_tool_returnService与Itm_tool_returnService中编写
 */
using VOL.DeviceManager.IRepositories;
using VOL.DeviceManager.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Services
{
    public partial class tm_tool_returnService : ServiceBase<tm_tool_return, Itm_tool_returnRepository>
    , Itm_tool_returnService, IDependency
    {
    public tm_tool_returnService(Itm_tool_returnRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static Itm_tool_returnService Instance
    {
      get { return AutofacContainerModule.GetService<Itm_tool_returnService>(); } }
    }
 }
