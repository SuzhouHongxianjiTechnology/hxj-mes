#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

namespace AlbertCollection.Core
{
    /// <summary>
    /// Cache常量
    /// </summary>
    public class CacheConst
    {
        /// <summary>
        /// 登录验证码缓存Key
        /// </summary>
        public const string Cache_Captcha = Cache_Prefix_Web + "Captcha";

        /// <summary>
        /// 系统配置表缓存Key
        /// </summary>
        public const string Cache_DevConfig = Cache_Prefix_Web + "DevConfig";

        /// <summary>
        /// Cache Key前缀(可删除)
        /// </summary>
        public const string Cache_Prefix_Web = "AlbertCollection:";

        /// <summary>
        /// 关系表缓存Key
        /// </summary>
        public const string Cache_SysRelation = Cache_Prefix_Web + "SysRelation:";

        /// <summary>
        /// 资源表缓存Key
        /// </summary>
        public const string Cache_SysResource = Cache_Prefix_Web + "SysResource:";

        /// <summary>
        /// 角色表缓存Key
        /// </summary>
        public const string Cache_SysRole = Cache_Prefix_Web + "SysRole";

        /// <summary>
        /// 用户表缓存Key
        /// </summary>
        public const string Cache_SysUser = Cache_Prefix_Web + "SysUser";

        /// <summary>
        /// 用户账户关系缓存Key
        /// </summary>
        public const string Cache_SysUserAccount = Cache_Prefix_Web + "SysUserAccount";

        /// <summary>
        /// UserId缓存Key
        /// </summary>
        public const string Cache_UserId = Cache_Prefix_Web + "UserId";

        public const string Cache_DriverPlugin = Cache_Prefix_Web + "DriverPlugin";

        #region OpenApi

        /// <summary>
        /// OpenApi用户表缓存Key
        /// </summary>
        public const string Cache_OpenApiUser = Cache_Prefix_Web + "OpenApiUser";

        /// <summary>
        /// OpenApi关系缓存Key
        /// </summary>
        public const string Cache_OpenApiUserAccount = Cache_Prefix_Web + "OpenApiUserAccount";

        /// <summary>
        /// OpenApiUserId缓存Key
        /// </summary>
        public const string Cache_OpenApiUserId = Cache_Prefix_Web + "OpenApiUserId";

        /// <summary>
        /// UserVerificat缓存Key
        /// </summary>
        public const string Cache_OpenApiUserVerificat = Cache_Prefix_Web + "OpenApiUserVerificat";

        /// <summary>
        /// UserVerificat缓存Key
        /// </summary>
        public const string Cache_UserVerificat = Cache_Prefix_Web + "UserVerificat";

        /// <summary>
        /// Swagger登录缓存Key
        /// </summary>
        public const string SwaggerLogin = Cache_Prefix_Web + "SwaggerLogin";

        #endregion OpenApi

        #region DeviceCache
        public const string PdmProduct = "PdmProduct";
        public const string PdmProductType = "产品型号";
        public const string PdmProductUpdate = "产品切型-更新在制产品";
        public const string PdmProductUpdateCraft = "产品切型-更新在制工艺";
        public const string PdmProductUpdateCraftStationList = "产品切型-更新在制工艺工站列表";
        public const string DeviceList = "DeviceList";
        public const string Craft = "Craft";
        public const string CraftStationList = "CraftStationList";
        public const string PlcMes = "MES-PLC 交互";
        public const string RfidUp = "收到来自[Rfid]的上升沿";
        public const string RfidDown = "置位来自[Rfid]的上升沿";
        public const string RfidResponseUp = "发出[Rfid]的响应上升沿";
        public const string RfidReadError = "Rfid 未读取到";
        public const string RfidIsUse = "Rfid 被占用，如需重试请点击恢复占用";
        public const string SaveDataUp = "收到来自[保存数据]的上升沿";
        public const string SaveDataDown = "置位来自[保存数据]的上升沿";
        public const string SaveDataResponseUp = "发出[保存数据]的响应上升沿";
        public const string FirstUpdateStationListStatusY = "初次通讯-更新工站状态为 Y";
        public const string FirstUpdateStationListStatusN = "更新工站状态为 N";
        public const string OtherSql240 = "Op240";
        public const string OtherSql250 = "Op250";
        public const string OtherSql290 = "Op290";
        public const string OtherSql300 = "Op300";

        #endregion
    }
}